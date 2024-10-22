using BilleterieParis2024.Models;
using EllipticCurve.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace BilleterieParis2024.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class StatisticsController : Controller
    {
        public IActionResult Index()
        { 
            return View();
        }

        //Retourne le nombre de ventes par type d'offres et par mois
        public JsonResult StatByMonth()
        {
            List <Orders> listOrders = new List<Orders>();
            List<OrderDetails> listOrderDetails = new List<OrderDetails>();
            List<TicketsOffers> listTicketsOffers = new List<TicketsOffers>();

            GetLists(ref listOrders, ref listOrderDetails, ref listTicketsOffers);

                var statResult = from order in listOrders
                    join orderDetail in listOrderDetails on order.Id equals orderDetail.OrderId
                    join ticketOffer in listTicketsOffers on orderDetail.TicketsOfferID equals ticketOffer.Id
                    group new { order, ticketOffer } by new { order.OrderDate.Month, order.OrderDate.Year } into g
                    orderby g.Key.Year, g.Key.Month
                    select new 
                    {
                        Month = g.Key.Month + "/" + g.Key.Year,
                        Solo = g.Count(a => a.ticketOffer.OfferName == "Solo"),
                        Duo = g.Count(a => a.ticketOffer.OfferName == "Duo"),
                        Groupe = g.Count(a => a.ticketOffer.OfferName == "Groupe")
                    };

            // Passer les données à la vue
            //return View(statResult);
            //return Json(new { data = statResult }, new System.Text.Json.JsonSerializerOptions());
            return Json(statResult, new System.Text.Json.JsonSerializerOptions());
        }

        //Retourne le nombre de ventes par type d'offre
        public JsonResult StatByOffer()
        {
            List<Orders> listOrders = new List<Orders>();
            List<OrderDetails> listOrderDetails = new List<OrderDetails>();
            List<TicketsOffers> listTicketsOffers = new List<TicketsOffers>();

            GetLists(ref listOrders, ref listOrderDetails, ref listTicketsOffers);

            var statResult = from order in listOrders
                join orderDetail in listOrderDetails on order.Id equals orderDetail.OrderId
                join ticketOffer in listTicketsOffers on orderDetail.TicketsOfferID equals ticketOffer.Id
                group ticketOffer by ticketOffer.OfferName into g
                orderby g.Key
                select new 
                {
                    Name = g.Key,
                    Total = g.Count()
                };

            // Passer les données à la vue
            //return View(statResult);
            //return Json(new { data = statResult }, new System.Text.Json.JsonSerializerOptions());
            return Json(statResult, new System.Text.Json.JsonSerializerOptions());
        }

       //Alimentation dynamique et aléatoire des lists Orders et OrderDetails
        private void  GetLists(ref List<Orders> listOrders, ref List<OrderDetails> listOrderDetails, ref List<TicketsOffers> listTicketsOffers)
        {
            listTicketsOffers = new List<TicketsOffers> {
                new TicketsOffers {Id=1,OfferName="Solo" },
                new TicketsOffers {Id=2,OfferName="Duo" },
                new TicketsOffers {Id=3,OfferName="Groupe" }
            };


            var rand = new Random();
            for (int i = 1; i <= 10000; i++)
            {
                Orders anOrder = new Orders()
                {
                    Id = i,
                    UserId = "22efbeeb-f22f-460f-8dd6-7421618acd7e",
                    OrderDate = GenerateRandomDate(),
                    OrderNo = i.ToString("000")
                };

                listOrders.Add(anOrder);

                for (int j = 1; j <= rand.Next(1, 4); j++)
                {
                    OrderDetails orderDetails = new OrderDetails()
                    {
                        Id = j + 4,
                        OrderId = i,
                        TicketsOfferID = rand.Next(1, 4)
                    };
                    listOrderDetails.Add(orderDetails);
                }
            };
        }

        //Génère une date aléatoire entre le 01/07/2023 et 31/07/2024 
        private DateTime GenerateRandomDate()
        {
            // Définir la plage de dates
            DateTime start = new DateTime(2023, 7, 1);
            DateTime end = new DateTime(2024, 7, 31);

            // Générer un TimeSpan aléatoire dans la plage spécifiée
            Random random = new Random();
            int range = (end - start).Days;
            DateTime randomDate = start.AddDays(random.Next(range)).AddHours(random.Next(0, 24)).AddMinutes(random.Next(0, 60)).AddSeconds(random.Next(0, 60));

            // retourne la date et heure générées
            return randomDate;
        }
    }
}

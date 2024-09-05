using BilleterieParis2024.Data;
using BilleterieParis2024.Models;
using BilleterieParis2024.Utilities;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text;

namespace BilleterieParis2024.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class OrderController : Controller
    {
        private ApplicationDbContext _db;

        public OrderController(ApplicationDbContext db)
        {
            _db = db;
        }

        //GET Checkout action method

        public IActionResult Checkout()
        {
            return View();
        }

        //POST Checkout action method

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Checkout(Orders anOrder)
        {

                List<TicketsOffers> ticketsOffers = HttpContext.Session.Get<List<TicketsOffers>>("TicketsOffers");
                if (ticketsOffers != null)
                {
                    foreach (var ticketsOffer in ticketsOffers)
                    {
                        OrderDetails orderDetails = new OrderDetails();
                        orderDetails.TicketsOfferID = ticketsOffer.Id;
                        anOrder.OrderDetails.Add(orderDetails);
                    }
                    //Récupération de l'id de l'utilisateur connecté
                    var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                    var OrderNo = GetOrderNo();
                    anOrder.OrderNo = OrderNo;
                    anOrder.OrderKey = GetOrderKey(16);
                    anOrder.UserId = userId;
                    anOrder.OrderDate = DateTime.Now;

                    _db.Orders.Add(anOrder);
                    await _db.SaveChangesAsync();
                    
                }
          

            //var orderResult = from order in _db.Orders
            //                  join orderDetail in _db.OrderDetails on order.Id equals orderDetail.OrderId
            //                  where order.OrderNo == anOrder.OrderNo
            //                  select new
            //                  {
            //                      OrderNo = order.OrderNo,
            //                      orderDate=order.OrderDate,
            //                      orderDetails = order.OrderDetails.ToList()
            //                  };
                         //select $"Commande n° {order.OrderNo}";
           // return RedirectToAction("CheckoutConfirm", anOrder);
            return View("CheckoutConfirm", anOrder);

            // On réinitialise la liste des produits à 0
            HttpContext.Session.Set("TicketsOffers", new List<TicketsOffers>());

        }

        [Route("checkout-confirmation")]
        public async Task<IActionResult> CheckoutConfirm()
        {
            return View();
        }

        public string GetOrderNo()
        {
            int rowCount = _db.Orders.ToList().Count() + 1;
            return rowCount.ToString("000");
        }

        //Génération de la clé associée à la commande
        public static string GetOrderKey(int length)
        {
            const string chars = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

            StringBuilder sb = new StringBuilder();
            Random rnd = new Random();

            for (int i = 0; i < length; i++)
            {
                int index = rnd.Next(chars.Length);
                sb.Append(chars[index]);
            }

            return sb.ToString();
        }
    }
}

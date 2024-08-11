using BilleterieParis2024.Data;
using BilleterieParis2024.Models;
using BilleterieParis2024.Utilities;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BilleterieParis2024.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class CartController : Controller
    {

        private readonly ApplicationDbContext _context;

        public CartController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            List<TicketsOffers> ticketsOffers = HttpContext.Session.Get<List<TicketsOffers>>("TicketsOffers");
            if (ticketsOffers == null)
            {
                ticketsOffers = new List<TicketsOffers>();
            }

            //var result = from t in ticketsOffers
            //             group t by t.OfferName into t
            //             select new { OfferName = g.OfferName  , Image = g.Image, Number = g.Count(), Total = g.Sum(x => x.Price) };
            
            return View(ticketsOffers);
        }

        //POST product detail acation method
        [HttpPost]
        public ActionResult AddToCart(int? id)
        {
            List<TicketsOffers> ticketsOffers = new List<TicketsOffers>();
            if (id == null)
            {
                return NotFound();
            }

            var ticketsOffer = _context.TicketsOffers.FirstOrDefault(c => c.Id == id);
            if (ticketsOffer == null)
            {
                return NotFound();
            }

            ticketsOffers = HttpContext.Session.Get<List<TicketsOffers>>("TicketsOffers");
            if (ticketsOffers == null)
            {
                ticketsOffers = new List<TicketsOffers>();
            }
            ticketsOffers.Add(ticketsOffer);
            HttpContext.Session.Set("TicketsOffers", ticketsOffers);
            return RedirectToAction("Offers", "Home");
        }

        [HttpPost]
        [ActionName("Remove")]
        public IActionResult RemoveFromOffers(int? id)
        {
            List<TicketsOffers> ticketsOffers = HttpContext.Session.Get<List<TicketsOffers>>("TicketsOffers");
            if (ticketsOffers != null)
            {
                var ticketsOffer = ticketsOffers.FirstOrDefault(c => c.Id == id);
                if (ticketsOffer != null)
                {
                    ticketsOffers.Remove(ticketsOffer);
                    HttpContext.Session.Set("TicketsOffers", ticketsOffers);
                }
            }
            return RedirectToAction("Offers","Home");
        }

        //GET Remove action methode from
        //Paramétre cart pour éventuellement fusionner avec l'action RemoveFromOffers. Code strictement identique 
        public IActionResult RemoveFromCart(int? id, bool cart)
        {
            List<TicketsOffers> ticketsOffers = HttpContext.Session.Get<List<TicketsOffers>>("TicketsOffers");
            if (ticketsOffers != null)
            {
                var ticketsOffer = ticketsOffers.FirstOrDefault(c => c.Id == id);
                if (ticketsOffer != null)
                {
                    ticketsOffers.Remove(ticketsOffer);
                    HttpContext.Session.Set("TicketsOffers", ticketsOffers);
                }
            }
            return RedirectToAction(nameof(Index));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

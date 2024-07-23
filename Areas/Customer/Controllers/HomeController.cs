using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using BilleterieParis2024.Data;
using BilleterieParis2024.Models;
using Microsoft.EntityFrameworkCore;
using BilleterieParis2024.Utilities;

namespace BilleterieParis2024.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
            
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Route("")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("offers")]
        public IActionResult Offers()
        {
            return _context.TicketsOffers != null ?
                          View(_context.TicketsOffers.ToList()) :
                          Problem("Entity set 'ApplicationDbContext.TicketsOffers'  is null.");
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
            return RedirectToAction(nameof(Offers));
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}


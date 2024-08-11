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
                          View(_context.TicketsOffers.Where(x => x.IsAvailable == true).ToList()) :
                          Problem("Entity set 'ApplicationDbContext.TicketsOffers'  is null.");
        }

    }
}


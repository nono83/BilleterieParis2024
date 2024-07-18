using BilleterieParis2024.Data;
using BilleterieParis2024.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BilleterieParis2024.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Offers()
        {
            return _context.TicketsOffers != null ?
                          View(_context.TicketsOffers.ToList()) :
                          Problem("Entity set 'ApplicationDbContext.TicketsOffers'  is null.");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BilleterieParis2024.Data;
using BilleterieParis2024.Models;
using Microsoft.AspNetCore.Authorization;

namespace BilleterieParis2024.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TicketsOffersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TicketsOffersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/TicketsOffers
        public IActionResult Index()
        {
              return _context.TicketsOffers != null ? 
                          View(_context.TicketsOffers.ToList()) :
                          Problem("Entity set 'ApplicationDbContext.TicketsOffers'  is null.");
        }

        // GET: Admin/TicketsOffers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TicketsOffers == null)
            {
                return NotFound();
            }

            var ticketsOffers = await _context.TicketsOffers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ticketsOffers == null)
            {
                return NotFound();
            }

            return View(ticketsOffers);
        }

        // GET: Admin/TicketsOffers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/TicketsOffers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,OfferName,Price")] TicketsOffers ticketsOffers)
        public async Task<IActionResult> Create(TicketsOffers ticketsOffers)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ticketsOffers);
                await _context.SaveChangesAsync();
                TempData["save"] = "L’offre a bien été créée";
                return RedirectToAction(nameof(Index));
            }
            return View(ticketsOffers);
        }

        // GET: Admin/TicketsOffers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TicketsOffers == null)
            {
                return NotFound();
            }

            var ticketsOffers = await _context.TicketsOffers.FindAsync(id);
            if (ticketsOffers == null)
            {
                return NotFound();
            }
            return View(ticketsOffers);
        }

        // POST: Admin/TicketsOffers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Id,OfferName,Price")] TicketsOffers ticketsOffers)
        public async Task<IActionResult> Edit(TicketsOffers ticketsOffers)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ticketsOffers);
                    await _context.SaveChangesAsync();
                    TempData["edit"] = "L'offre a bien été mise à jour";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TicketsOffersExists(ticketsOffers.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(ticketsOffers);
        }

        // GET: Admin/TicketsOffers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TicketsOffers == null)
            {
                return NotFound();
            }

            var ticketsOffers = await _context.TicketsOffers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ticketsOffers == null)
            {
                return NotFound();
            }

            return View(ticketsOffers);
        }

        // POST: Admin/TicketsOffers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TicketsOffers == null)
            {
                return Problem("Entity set 'ApplicationDbContext.TicketsOffers'  is null.");
            }
            var ticketsOffers = await _context.TicketsOffers.FindAsync(id);
            if (ticketsOffers != null)
            {
                _context.TicketsOffers.Remove(ticketsOffers);
                TempData["delete"] = "L'offre a bien été supprimée";
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TicketsOffersExists(int id)
        {
          return (_context.TicketsOffers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

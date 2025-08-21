using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Traylo.Data;
using Traylo.Models;

namespace Traylo.Controllers
{
    [Authorize]
    public class DeliveryPersonController : Controller
    {
        private readonly AppDbContext _context;
        public DeliveryPersonController(AppDbContext context) => _context = context;

        [Authorize(Roles = "Manager,Admin")]
        public async Task<IActionResult> Index()
        {
            var deliveryPersons = await _context.DeliveryPeople
                .Include(d => d.City)
                .ToListAsync();

            return View(deliveryPersons);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create()
        {
            ViewBag.Cities = await _context.Cities.ToListAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(DeliveryPerson dp)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Cities = await _context.Cities.ToListAsync();
                return View(dp);
            }
            _context.DeliveryPeople.Add(dp);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int DeliveryPersonsId)
        {
            var deliveryPerson = await _context.DeliveryPeople.FindAsync(DeliveryPersonsId);
            if (deliveryPerson == null) return NotFound();
            ViewBag.Cities = await _context.Cities.ToListAsync();
            return View(deliveryPerson);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(DeliveryPerson dp)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Cities = await _context.Cities.ToListAsync();
                return View(dp);
            }
            _context.DeliveryPeople.Update(dp);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int DeliveryPersonsId)
        {
            var dp = await _context.DeliveryPeople.FindAsync(DeliveryPersonsId);
            if (dp == null) return NotFound();
            return View(dp);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int DeliveryPersonsId)
        {
            var dp = await _context.DeliveryPeople.FindAsync(DeliveryPersonsId);
            if (dp != null) _context.DeliveryPeople.Remove(dp);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}

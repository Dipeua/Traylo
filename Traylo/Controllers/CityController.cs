using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Traylo.Data;
using Traylo.Models;

namespace Traylo.Controllers
{
    [Authorize]
    public class CityController : Controller
    {
        private readonly AppDbContext _context;
        public CityController(AppDbContext context) => _context = context;

        [Authorize(Roles = "Manager,Admin")]
        public async Task<IActionResult> Details(int CityId)
        {
            var city = await _context.Cities
                .Include(c => c.DeliveryPeople)
                    .ThenInclude(d => d.StockHistories)  // inclure les mouvements pour chaque livreur
                .ThenInclude(sh => sh.Product)       // inclure les produits
                .Include(c => c.StockHistories)      // inclure aussi les mouvements directs vers la ville
                    .ThenInclude(sh => sh.Product)
                .FirstOrDefaultAsync(c => c.CityId == CityId);

            if (city == null)
            {
                return NotFound();
            }

            return View(city);
        }


        [Authorize(Roles = "Manager,Admin")]
        public async Task<IActionResult> Index()
        {
            var cities = await _context.Cities
                .Include(c => c.DeliveryPeople)
                .ToListAsync();
            return View(cities);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create() => View();


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(City city)
        {
            if (!ModelState.IsValid) return View(city);
            _context.Cities.Add(city);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int CityId)
        {
            var city = await _context.Cities.FindAsync(CityId);
            if (city == null) return NotFound();
            return View(city);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(City city)
        {
            if (!ModelState.IsValid) return View(city);
            _context.Cities.Update(city);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int CityId)
        {
            var city = await _context.Cities
                .Include(x => x.DeliveryPeople)
                .FirstOrDefaultAsync(x => x.CityId == CityId);

            if (city == null) return NotFound();
            return View(city);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int CityId)
        {
            var city = await _context.Cities.FindAsync(CityId);
            if (city != null) _context.Cities.Remove(city);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Traylo.Data;

namespace Traylo.Controllers
{
    [Authorize]
    public class RapportsController : Controller
    {
        private readonly AppDbContext _context;

        public RapportsController(AppDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Manager,Admin")]
        public async Task<IActionResult> Index()
        {
            var livreurs = await _context.DeliveryPeople
                .Include(d => d.City)
                .Include(d => d.StockHistories)
                    .ThenInclude(sh => sh.Product)
                .ToListAsync();

            return View(livreurs);
        }
    }
}

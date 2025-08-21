using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Threading.Tasks;
using Traylo.Data;
using Traylo.Models;

namespace Traylo.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        [Authorize(Roles = "Manager,Admin")]
        public async Task<IActionResult> Index()
        {
            var cites = await _context.Cities.ToListAsync();
            var sumTotalProducts = await _context.Products.SumAsync(p => p.QuantityReste);
            var deliveryPersons = await _context.DeliveryPeople.ToListAsync();
            var stockhistory = await _context.StockHistories.ToListAsync();

            var dv = new DashboardViewModel()
            {
                Cites = cites,
                TotalProducts = (int)sumTotalProducts,
                DeliveryPersons = deliveryPersons,
                StockHistorys = stockhistory
            };
            return View(dv);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

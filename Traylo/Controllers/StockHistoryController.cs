using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Traylo.Data;
using Traylo.Models;

namespace Traylo.Controllers
{
    [Authorize]
    public class StockHistoryController : Controller
    {
        private readonly AppDbContext _context;
        public StockHistoryController(AppDbContext context) => _context = context;

        [Authorize(Roles = "Manager,Admin")]
        public async Task<IActionResult> Index()
        {
            var model = await _context.StockHistories
                .Include(s => s.Product)
                .Include(s => s.City)
                .Include(s => s.DeliveryPerson)
                .OrderByDescending(s => s.Date)
                .Take(200)
                .ToListAsync();
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddCentralStock()
        {
            ViewBag.Products = await _context.Products.ToListAsync();
            return View();
        }

        public class CentralStockDto
        {
            public int ProductId { get; set; }
            public int Quantity { get; set; }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddCentralStockMultiple(List<CentralStockDto> products, string? note)
        {
            if (products == null || !products.Any())
            {
                ModelState.AddModelError("", "Aucun produit fourni.");
                ViewBag.Products = await _context.Products.ToListAsync();
                return View();
            }

            foreach (var item in products)
            {
                if (item.Quantity <= 0) continue;

                var product = await _context.Products.FindAsync(item.ProductId);
                if (product == null) continue;

                // Mise à jour du stock central
                product.InitQuantity += item.Quantity;
                product.QuantityReste = (product.QuantityReste ?? 0) + item.Quantity;

                // Historique
                _context.StockHistories.Add(new StockHistory
                {
                    ProductId = item.ProductId,
                    CityId = null,
                    DeliveryPersonId = null,
                    QuantityChange = item.Quantity,
                    Date = DateTime.UtcNow,
                    Note = note ?? $"{item.Quantity} {product.Name} ajoutés au stock central"
                });
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DistributeToCity()
        {
            ViewBag.Products = await _context.Products.ToListAsync();
            ViewBag.Cities = await _context.Cities.ToListAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DistributeMultipleToCity(
            int cityId,
            List<ProductDistributionDto> products,
            string? note)
        {
            var city = await _context.Cities.FindAsync(cityId);
            if (city == null) return NotFound();

            foreach (var item in products)
            {
                var product = await _context.Products.FindAsync(item.ProductId);
                if (product == null || product.QuantityReste < item.Quantity)
                    continue; // ou return erreur

                // Mise à jour du stock
                product.QuantityDelivery = (product.QuantityDelivery ?? 0) + item.Quantity;
                product.QuantityReste = product.InitQuantity - product.QuantityDelivery;

                // Historique
                _context.StockHistories.Add(new StockHistory
                {
                    ProductId = item.ProductId,
                    CityId = cityId,
                    DeliveryPersonId = null,
                    QuantityChange = item.Quantity,
                    Date = DateTime.UtcNow,
                    Note = note ?? $"Distribution de {item.Quantity} {product.Name} vers la ville {city.Name}"
                });
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Authorize(Roles = "Manager,Admin")]
        public IActionResult DistributeToDeliveryPerson()
        {
            ViewBag.Products = _context.Products.ToList();
            ViewBag.DeliveryPeople = _context.DeliveryPeople.Include(d => d.City).ToList();
            return View();
        }

        public class ProductDistributionDto
        {
            public int ProductId { get; set; }
            public int Quantity { get; set; }
        }

        [HttpPost]
        [Authorize(Roles = "Manager,Admin")]
        public async Task<IActionResult> DistributeMultipleToDeliveryPerson(
            int deliveryPersonId,
            List<ProductDistributionDto> products,
            string? note)
        {
            var deliveryPerson = await _context.DeliveryPeople.FindAsync(deliveryPersonId);
            if (deliveryPerson == null) return NotFound();

            foreach (var item in products)
            {
                var product = await _context.Products.FindAsync(item.ProductId);
                if (product == null || product.QuantityReste < item.Quantity)
                    continue; // ou return erreur

                product.QuantityDelivery = (product.QuantityDelivery ?? 0) + item.Quantity;
                product.QuantityReste = product.InitQuantity - product.QuantityDelivery;

                _context.StockHistories.Add(new StockHistory
                {
                    ProductId = item.ProductId,
                    DeliveryPersonId = deliveryPersonId,
                    QuantityChange = item.Quantity,
                    Date = DateTime.UtcNow,
                    Note = note ?? $"Distribution de {item.Quantity} {product.Name} vers {deliveryPerson.FullName}"
                });
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


    }
}

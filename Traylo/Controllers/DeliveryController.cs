using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Traylo.Data;
using Traylo.Models;

namespace Traylo.Controllers
{
    public class DeliveryController : Controller
    {
        private readonly AppDbContext _context;
        public DeliveryController(AppDbContext context) => _context = context;

        //public IActionResult Index()
        //{
        //    var products = _context.Products.ToList();

        //    var cities = _context.Cities
        //                         .Include(c => c.DeliveryPeople)
        //                         .Where(c => c.DeliveryPeople.Any())
        //                         .ToList();

        //    var stockHistories = _context.StockHistories
        //                                 .Where(s => s.DeliveryPersonId != null)
        //                                 .ToList();

        //    var viewModel = new List<DeliveryTableViewModel>();

        //    foreach (var product in products)
        //    {
        //        var productVM = new DeliveryTableViewModel
        //        {
        //            ProductName = product.Name,
        //            QuantitiesByCityAndDeliveryPerson = new Dictionary<string, Dictionary<string, int>>()
        //        };

        //        foreach (var city in cities)
        //        {
        //            var deliveries = new Dictionary<string, int>();

        //            foreach (var dp in city.DeliveryPeople)
        //            {
        //                var qty = stockHistories
        //                    .Where(s => s.ProductId == product.ProductId && s.DeliveryPersonId == dp.DeliveryPersonId)
        //                    .Sum(s => s.QuantityChange);

        //                if (qty > 0)
        //                    deliveries[dp.FullName] = qty;
        //            }

        //            if (deliveries.Any())
        //                productVM.QuantitiesByCityAndDeliveryPerson[city.Name] = deliveries;
        //        }

        //        if (productVM.QuantitiesByCityAndDeliveryPerson.Any())
        //            viewModel.Add(productVM);
        //    }

        //    return View(viewModel);
        //}

        public IActionResult Index()
        {
            var products = _context.Products.ToList();

            var cities = _context.Cities
                                 .Include(c => c.DeliveryPeople)
                                 .Where(c => c.DeliveryPeople.Any())
                                 .ToList();

            var stockHistories = _context.StockHistories
                                         .Where(s => s.DeliveryPersonId != null)
                                         .ToList();

            var viewModel = new List<DeliveryTableViewModel>();

            foreach (var product in products)
            {
                var productVM = new DeliveryTableViewModel
                {
                    ProductName = product.Name,
                    QuantitiesByCityAndDeliveryPerson = new Dictionary<string, Dictionary<string, int>>()
                };

                foreach (var city in cities)
                {
                    var deliveries = new Dictionary<string, int>();

                    foreach (var dp in city.DeliveryPeople)
                    {
                        var qty = stockHistories
                            .Where(s => s.ProductId == product.ProductId && s.DeliveryPersonId == dp.DeliveryPersonId)
                            .Sum(s => s.QuantityChange);

                        deliveries[dp.FullName] = qty; // si rien reçu, qty = 0
                    }

                    if (deliveries.Any())
                        productVM.QuantitiesByCityAndDeliveryPerson[city.Name] = deliveries;
                }

                if (productVM.QuantitiesByCityAndDeliveryPerson.Any())
                    viewModel.Add(productVM);
            }

            return View(viewModel);
        }
    }
}

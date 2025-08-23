using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Traylo.Data;
using Traylo.Models;
using ClosedXML.Excel;
using System.IO;

namespace Traylo.Controllers
{
    public class DeliveryController : Controller
    {
        private readonly AppDbContext _context;
        public DeliveryController(AppDbContext context) => _context = context;

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


        public IActionResult DownloadExcel()
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

                        deliveries[dp.FullName] = qty; // 0 si pas de produit
                    }

                    if (deliveries.Any())
                        productVM.QuantitiesByCityAndDeliveryPerson[city.Name] = deliveries;
                }

                if (productVM.QuantitiesByCityAndDeliveryPerson.Any())
                    viewModel.Add(productVM);
            }

            // ===============================
            // Génération du fichier Excel
            // ===============================
            using (var workbook = new XLWorkbook())
            {
                var ws = workbook.Worksheets.Add("Livraisons");

                int row = 1;
                int col = 2;

                // 1) Première ligne = noms des villes avec fusion des colonnes par ville
                foreach (var city in viewModel.First().QuantitiesByCityAndDeliveryPerson.Keys)
                {
                    var livreurs = viewModel.First().QuantitiesByCityAndDeliveryPerson[city].Keys.ToList();

                    // Écrire le nom de la ville et fusionner seulement les colonnes des livreurs
                    ws.Cell(row, col).Value = city;
                    ws.Range(row, col, row, col + livreurs.Count - 1).Merge();
                    col += livreurs.Count;

                    // Ici, on ne met pas TOTAL1 !
                    // On avance juste une colonne pour accueillir TOTAL2 plus bas
                    ws.Range(row, col, row + 1, col).Merge(); // Fusion verticale pour TOTAL2
                    ws.Cell(row, col).Value = "TOTAL";
                    col++;
                }

                // 2) Deuxième ligne = livreurs
                row++;
                col = 2;
                foreach (var city in viewModel.First().QuantitiesByCityAndDeliveryPerson.Keys)
                {
                    var livreurs = viewModel.First().QuantitiesByCityAndDeliveryPerson[city].Keys.ToList();

                    foreach (var dp in livreurs)
                    {
                        ws.Cell(row, col).Value = dp;
                        col++;
                    }

                    // pas besoin d’écrire TOTAL2 ici → il est déjà écrit plus haut
                    col++;
                }


                // 3) Produits et quantités
                row++;
                foreach (var product in viewModel)
                {
                    ws.Cell(row, 1).Value = product.ProductName;

                    col = 2;
                    foreach (var city in product.QuantitiesByCityAndDeliveryPerson.Keys)
                    {
                        var deliveries = product.QuantitiesByCityAndDeliveryPerson[city];
                        foreach (var qty in deliveries.Values)
                        {
                            ws.Cell(row, col).Value = qty;
                            col++;
                        }
                        ws.Cell(row, col).Value = deliveries.Values.Sum();
                        col++;
                    }
                    row++;
                }

                // ---------------------------
                // Mise en forme (bordures)
                // ---------------------------
                var usedRange = ws.RangeUsed();
                usedRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                usedRange.Style.Border.InsideBorder = XLBorderStyleValues.Thin;
                usedRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                usedRange.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                usedRange.Style.Font.Bold = true;


                // Auto-ajuster les colonnes
                ws.Columns().AdjustToContents();

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"Fiche-de-livraisons-du-{DateTime.Now.ToString("dd-MM-yyyy")}.xlsx");
                }

                
            }
        }

}
}

namespace Traylo.Models
{
    public class ProductFilterViewModel
    {
        public int? Year { get; set; }
        public int? Month { get; set; }
        public int? Day { get; set; }

        public List<ProductStockViewModel> Products { get; set; } = new();
    }

}

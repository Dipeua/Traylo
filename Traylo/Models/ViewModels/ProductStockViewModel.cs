namespace Traylo.Models
{
    public class ProductStockViewModel
    {
        public int ProductId { get; set; }
        public string Name { get; set; }

        public int InitQuantity { get; set; }
        public int QuantityDelivered { get; set; }
        public int QuantityRemaining { get; set; }
    }

}

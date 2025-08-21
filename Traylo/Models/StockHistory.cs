using System.ComponentModel.DataAnnotations;

namespace Traylo.Models
{
    public class StockHistory
    {
        [Key]
        public int StockHistoryId { get; set; }

        public int ProductId { get; set; }
        public Product? Product { get; set; }

        public int? CityId { get; set; }
        public City? City { get; set; }

        public int? DeliveryPersonId { get; set; }
        public DeliveryPerson? DeliveryPerson { get; set; }

        [Required]
        public int QuantityChange { get; set; }

        public DateTime Date { get; set; } = DateTime.UtcNow;

        public string? Note { get; set; }
    }
}

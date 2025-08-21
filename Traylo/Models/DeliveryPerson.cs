using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Traylo.Models
{
    public class DeliveryPerson
    {
        [Key]
        public int DeliveryPersonId { get; set; }

        [Required]
        [DisplayName("Nom du livreur")]
        public string FullName { get; set; }

        [Required]
        [DisplayName("Numero de telephone")]
        public string PhoneNumber { get; set; }

        public int CityId { get; set; }
        public City? City { get; set; }
        public List<Product> Products { get; set; } = new();
        public List<StockHistory> StockHistories { get; set; } = new();
    }
}

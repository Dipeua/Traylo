using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Traylo.Models
{
    public class City
    {
        [Key]
        public int CityId { get; set; }

        [Required]
        [DisplayName("Nom de la ville ")]
        public string Name { get; set; }

        public ICollection<DeliveryPerson>? DeliveryPeople { get; set; }
        public ICollection<StockHistory>? StockHistories { get; set; }
    }
}

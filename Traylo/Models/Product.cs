using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Traylo.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required]
        [DisplayName("Nom du produit")]
        public string Name { get; set; }

        public int InitQuantity { get; set; }
        public int? QuantityDelivery { get; set; }
        public int? QuantityReste { get; set; }
    }
}

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Traylo.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        [DisplayName("Nom du gerant")]
        public string Username { get; set; }

        [Required]
        [DisplayName("Mot de passe")]
        public string PasswordHash { get; set; }

        [Required]
        public UserRole Role { get; set; }

        public int? CityId { get; set; }
        public City? City { get; set; }
    }
}

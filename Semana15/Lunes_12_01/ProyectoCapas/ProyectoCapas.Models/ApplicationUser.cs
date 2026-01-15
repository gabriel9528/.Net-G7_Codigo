using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ProyectoCapas.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Name { get; set; }

        [Required(ErrorMessage = "La dirreccion es obligatorio")]
        public string Address { get; set; }

        [Required(ErrorMessage = "La ciudad es obligatorio")]
        public string City { get; set; }

        [Required(ErrorMessage = "El pais es obligatorio")]
        public string Country { get; set; }
    }
}

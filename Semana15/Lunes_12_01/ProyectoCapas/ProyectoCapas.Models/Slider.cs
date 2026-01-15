using System.ComponentModel.DataAnnotations;

namespace ProyectoCapas.Models
{
    public class Slider
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Ingrese un nombre para el Slider")]
        [Display(Name = "Nombre del Slider")]
        public string Name { get; set; }
        [Required]
        public bool State { get; set; }
        [DataType(DataType.ImageUrl)]
        [Display(Name = "Imagen")]
        public string UrlImage { get; set; }
        public bool IsActive { get; set; } = true;
    }
}

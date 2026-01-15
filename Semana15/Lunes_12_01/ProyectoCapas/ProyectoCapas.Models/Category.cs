using System.ComponentModel.DataAnnotations;

namespace ProyectoCapas.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Ingrese un nombre para la categoria")]
        [Display(Name = "Nombre de la Categoria")]
        public string Name { get; set; }

        [Display(Name = "Orden de la visualizacion")]
        [Range(1, 100, ErrorMessage = "El valor debe estar entre 1 y 100")]
        public int? Order { get; set; }
        public bool IsActive { get; set; } = true;
    }
}

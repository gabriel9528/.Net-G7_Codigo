using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoCapas.Models
{
    public class Article
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre del articulo es obligatorio")]
        [Display(Name = "Nombre del Articulo")]
        public string Name { get; set; }

        [Required(ErrorMessage = "La descripcion del articulo es obligatoria")]
        public string Description { get; set; }

        [Display(Name = "Fecha de creacion")]
        public string CreatedDate { get; set; } = string.Empty;

        [DataType(DataType.ImageUrl)]
        public string UrlImage { get; set; } = string.Empty;

        [Required(ErrorMessage = "La categoria es obligatoria")]
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public Category Category { get; set; }
        public bool IsActive { get; set; } = true;
    }
}

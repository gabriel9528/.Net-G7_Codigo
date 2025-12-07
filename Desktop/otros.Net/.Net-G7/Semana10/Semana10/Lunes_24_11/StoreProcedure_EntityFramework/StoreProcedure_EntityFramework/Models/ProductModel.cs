using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace StoreProcedure_EntityFramework.Models
{
    public class ProductModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DisplayName("Product Name")]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int Quantity { get; set; }

        public string Remark { get; set; }
    }
}
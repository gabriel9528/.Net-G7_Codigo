using G7_Microservices.Backend.ShoppingCartAPI.Models.Dto;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace G7_Microservices.Backend.ShoppingCartAPI.Models
{
    public class CartDetails
    {
        [Key]
        public int Id { get; set; }
        public int CartHeaderId { get; set; }

        [ForeignKey("CartHeaderId")]
        public virtual CartHeader CartHeader { get; set; }
        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        [NotMapped]
        public virtual ProductDto Product { get; set; }
        public int Count { get; set; }
        public bool IsDeleted { get; set; }
    }
}

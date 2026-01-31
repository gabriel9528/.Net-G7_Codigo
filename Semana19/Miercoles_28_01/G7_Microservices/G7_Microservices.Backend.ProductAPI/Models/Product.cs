using System.ComponentModel.DataAnnotations;

namespace G7_Microservices.Backend.ProductAPI.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        [Range(0, int.MaxValue)]
        public double Price { get; set; }
        public string Description { get; set; }
        public string CategoryName { get; set; }
        public string ImageUrl { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}

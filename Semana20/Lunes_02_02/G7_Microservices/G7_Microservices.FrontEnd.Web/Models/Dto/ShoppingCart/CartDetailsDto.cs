using G7_Microservices.FrontEnd.Web.Models.Dto.Product;

namespace G7_Microservices.FrontEnd.Web.Models.Dto.ShoppingCart
{
    public class CartDetailsDto
    {
        public int Id { get; set; }
        public int CartHeaderId { get; set; }
        public virtual CartHeaderDto? CartHeaderDto { get; set; }
        public int ProductId { get; set; }
        public virtual ProductDto? ProductDto { get; set; }
        public int Count { get; set; }
    }
}

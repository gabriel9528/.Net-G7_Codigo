namespace G7_Microservices.FrontEnd.Web.Models.Dto.ShoppingCart
{
    public class CartDto
    {
        public CartHeaderDto CartHeaderDto { get; set; }
        public IEnumerable<CartDetailsDto>? CartDetailsDtos { get; set; }
    }
}

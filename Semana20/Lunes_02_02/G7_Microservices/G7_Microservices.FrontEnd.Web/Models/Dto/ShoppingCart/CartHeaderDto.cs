namespace G7_Microservices.FrontEnd.Web.Models.Dto.ShoppingCart
{
    public class CartHeaderDto
    {
        public int Id { get; set; }
        public string? UserId { get; set; }
        public string? CouponCode { get; set; }
        public double? Discount { get; set; }
        public double? CartTotal { get; set; }
    }
}

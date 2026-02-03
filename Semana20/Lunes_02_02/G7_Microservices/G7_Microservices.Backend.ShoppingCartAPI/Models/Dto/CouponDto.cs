namespace G7_Microservices.Backend.ShoppingCartAPI.Models.Dto
{
    public class CouponDto
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public double DiscountAmount { get; set; }
        public int MinimunAmount { get; set; }
        public bool IsDeleted { get; set; }
    }
}

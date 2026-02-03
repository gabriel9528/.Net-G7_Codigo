using G7_Microservices.FrontEnd.Web.Models.Dto;
using G7_Microservices.FrontEnd.Web.Models.Dto.ShoppingCart;

namespace G7_Microservices.FrontEnd.Web.Services.IServices
{
    public interface IShoppingCartService
    {
        Task<ResponseDto?> GetCartByUserIdAsync(string userId);
        Task<ResponseDto?> UpsertCartAsync(CartDto cartDto);
        Task<ResponseDto?> RemoveCartAsync(int cartDetailsId);
        Task<ResponseDto?> ApplyCouponAsync(CartDto applyCouponDto);
    }
}

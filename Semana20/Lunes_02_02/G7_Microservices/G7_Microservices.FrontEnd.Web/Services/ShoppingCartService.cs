using G7_Microservices.FrontEnd.Web.Models.Dto;
using G7_Microservices.FrontEnd.Web.Models.Dto.ShoppingCart;
using G7_Microservices.FrontEnd.Web.Services.IServices;
using static G7_Microservices.FrontEnd.Web.Utility.SD;

namespace G7_Microservices.FrontEnd.Web.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IBaseService _baseService;
        public ShoppingCartService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<ResponseDto?> GetCartByUserIdAsync(string userId)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                API_TYPE = API_TYPE.GET,
                Url = ShoppingCartAPIBase + $"/api/ShoppingCartAPI/GetCart/{userId}"
            });
        }

        public async Task<ResponseDto?> UpsertCartAsync(CartDto cartDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                API_TYPE = API_TYPE.POST,
                Data = cartDto,
                Url = ShoppingCartAPIBase + $"/api/ShoppingCartAPI/UpSert"
            });
        }

        public async Task<ResponseDto?> ApplyCouponAsync(CartDto applyCouponDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                API_TYPE = API_TYPE.POST,
                Data = applyCouponDto,
                Url = ShoppingCartAPIBase + $"/api/ShoppingCartAPI/ApplyCoupon"
            });
        }

        

        public async Task<ResponseDto?> RemoveCartAsync(int cartDetailsId)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                API_TYPE = API_TYPE.POST,
                Data = cartDetailsId,
                Url = ShoppingCartAPIBase + $"/api/ShoppingCartAPI/RemoveCart"
            });
        }

        public async Task<ResponseDto?> EmailCart(CartDto cartDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                API_TYPE = API_TYPE.POST,
                Data = cartDto,
                Url = ShoppingCartAPIBase + $"/api/ShoppingCartAPI/EmailCartRequest"
            });
        }
    }
}

using G7_Microservices.Backend.ShoppingCartAPI.Models.Dto;
using G7_Microservices.Backend.ShoppingCartAPI.Service.IService;
using Newtonsoft.Json;

namespace G7_Microservices.Backend.ShoppingCartAPI.Service
{
    public class CouponService : ICouponService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public CouponService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<CouponDto> GetCouponByCodeAsync(string couponCode)
        {
            var client = _httpClientFactory.CreateClient("Coupon");
            var response = await client.GetAsync($"/api/CouponsAPI/getByCode/{couponCode}");
            var apiContent = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<ResponseDto>(apiContent);
            if (result != null && result.IsSucess)
            {
                return JsonConvert.DeserializeObject<CouponDto>(Convert.ToString(result.Result));
            }

            return new CouponDto();

        }
    }
}

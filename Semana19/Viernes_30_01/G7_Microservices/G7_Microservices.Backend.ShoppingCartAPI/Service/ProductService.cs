using G7_Microservices.Backend.ShoppingCartAPI.Models.Dto;
using G7_Microservices.Backend.ShoppingCartAPI.Service.IService;
using Newtonsoft.Json;

namespace G7_Microservices.Backend.ShoppingCartAPI.Service
{
    public class ProductService : IProductService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public ProductService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IEnumerable<ProductDto>> GetProductsAsync()
        {
            var client = _httpClientFactory.CreateClient("Product");
            var response = await client.GetAsync($"/api/ProductsAPI");
            var apiContent = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<ResponseDto>(apiContent);
            if (result != null && result.IsSucess)
            {
                return JsonConvert.DeserializeObject<List<ProductDto>>(Convert.ToString(result.Result));
            }

            return new List<ProductDto>();

        }
    }
}

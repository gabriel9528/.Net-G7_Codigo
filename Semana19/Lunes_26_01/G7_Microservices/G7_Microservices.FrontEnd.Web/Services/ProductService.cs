using G7_Microservices.FrontEnd.Web.Models.Dto;
using G7_Microservices.FrontEnd.Web.Models.Dto.Product;
using G7_Microservices.FrontEnd.Web.Services.IServices;
using static G7_Microservices.FrontEnd.Web.Utility.SD;

namespace G7_Microservices.FrontEnd.Web.Services
{
    public class ProductService : IProductService
    {
        private readonly IBaseService _baseService;
        public ProductService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        //Post
        public async Task<ResponseDto?> CreateProductsAsync(ProductDto productDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                API_TYPE = API_TYPE.POST,
                Data = productDto,
                Url = ProductAPIBase + "/api/ProductsAPI"
            });
        }

        //Put
        public async Task<ResponseDto?> UpdateProductsAsync(ProductDto productDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                API_TYPE = API_TYPE.PUT,
                Data = productDto,
                Url = ProductAPIBase + "/api/ProductsAPI"
            });
        }

        //Delete
        public async Task<ResponseDto?> DeleteProductsAsync(int id)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                API_TYPE = API_TYPE.DELETE,
                Url = ProductAPIBase + "/api/ProductsAPI/" + id
            });
        }

        //GetALL
        public async Task<ResponseDto?> GetAllProductsAsync()
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                API_TYPE = API_TYPE.GET,
                Url = ProductAPIBase + "/api/ProductsAPI"
            });
        }

        //GetById
        public async Task<ResponseDto?> GetProductByIdAsync(int id)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                API_TYPE = API_TYPE.GET,
                Url = ProductAPIBase + "/api/ProductsAPI/" + id
            });
        }

        
    }
}

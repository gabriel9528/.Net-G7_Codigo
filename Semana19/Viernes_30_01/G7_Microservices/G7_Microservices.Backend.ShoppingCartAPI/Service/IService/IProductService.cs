using G7_Microservices.Backend.ShoppingCartAPI.Models.Dto;

namespace G7_Microservices.Backend.ShoppingCartAPI.Service.IService
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetProductsAsync();
    }
}

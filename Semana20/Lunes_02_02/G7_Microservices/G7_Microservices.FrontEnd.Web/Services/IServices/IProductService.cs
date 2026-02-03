using G7_Microservices.FrontEnd.Web.Models.Dto;
using G7_Microservices.FrontEnd.Web.Models.Dto.Product;

namespace G7_Microservices.FrontEnd.Web.Services.IServices
{
    public interface IProductService
    {
        Task<ResponseDto?> GetProductByIdAsync(int id);
        Task<ResponseDto?> GetAllProductsAsync();
        Task<ResponseDto?> CreateProductsAsync(ProductDto productDto);
        Task<ResponseDto?> UpdateProductsAsync(ProductDto productDto);
        Task<ResponseDto?> DeleteProductsAsync(int id);

    }
}

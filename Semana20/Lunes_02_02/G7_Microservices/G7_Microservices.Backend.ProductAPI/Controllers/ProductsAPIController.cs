using G7_Microservices.Backend.ProductAPI.Data;
using G7_Microservices.Backend.ProductAPI.Models;
using G7_Microservices.Backend.ProductAPI.Models.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace G7_Microservices.Backend.ProductAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsAPIController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private ResponseDto _responseDto;

        public ProductsAPIController(ApplicationDbContext db)
        {
            _db = db;
            _responseDto = new ResponseDto();
        }

        [HttpGet]
        //[Authorize(Roles = "Admin")]
        public ResponseDto GetAll()
        {
            try
            {
                IEnumerable<Product> listProducts = _db.Products.Where(x => !x.IsDeleted).ToList();
                List<ProductDto> productDtoList = new List<ProductDto>();

                foreach (Product product in listProducts)
                {
                    ProductDto newProductDto = new ProductDto();
                    newProductDto.Id = product.Id;
                    newProductDto.Name = product.Name;
                    newProductDto.Price = product.Price;
                    newProductDto.Description = product.Description;
                    newProductDto.CategoryName = product.CategoryName;
                    newProductDto.ImageUrl = product.ImageUrl;

                    productDtoList.Add(newProductDto);
                }

                _responseDto.Result = productDtoList;
                _responseDto.Message = "Productos recuperados con exito";
            }
            catch (Exception ex)
            {
                _responseDto.Message = "Error: " + ex.Message;
                _responseDto.IsSucess = false;
                _responseDto.Result = null;
            }

            return _responseDto;
        }

        [HttpGet]
        //[Authorize(Roles = "Admin")]
        [Route("{id:int}")]
        public ResponseDto GetById(int id)
        {
            ProductDto productDto = new ProductDto();
            try
            {
                Product? product = _db.Products.FirstOrDefault(x => x.Id == id && !x.IsDeleted);

                if (product != null)
                {
                    productDto.Id = product.Id;
                    productDto.Name = product.Name;
                    productDto.Price = product.Price;
                    productDto.Description = product.Description;
                    productDto.CategoryName = product.CategoryName;
                    productDto.ImageUrl = product.ImageUrl;

                    _responseDto.Result = productDto;
                    _responseDto.Message = $"Producto {productDto.Name} recuperado con exito";
                }
                else
                {
                    _responseDto.Message = "Producto no encontrado";
                    _responseDto.IsSucess = false;
                    _responseDto.Result = null;
                }
            }
            catch (Exception ex)
            {
                _responseDto.Message = "Error: " + ex.Message;
                _responseDto.IsSucess = false;
                _responseDto.Result = null;
            }

            return _responseDto;
        }

        [HttpPost]
        //[Authorize(Roles = "Admin")]
        public ResponseDto Post([FromBody] ProductDto productDto)
        {
            Product newProduct = new Product();

            newProduct.Id = productDto.Id;
            newProduct.Name = productDto.Name;
            newProduct.Price = productDto.Price;
            newProduct.Description = productDto.Description;
            newProduct.CategoryName = productDto.CategoryName;
            newProduct.ImageUrl = productDto.ImageUrl;

            try
            {
                if (newProduct != null)
                {
                    _db.Products.Add(newProduct);
                    _db.SaveChanges();

                    _responseDto.Result = productDto.Name;
                    _responseDto.Message = $"Producto con id {productDto.Id} REGISTRADO con exito";
                }
                else
                {
                    _responseDto.Result = null;
                    _responseDto.Message = "El producto ingresado no es valido";
                    _responseDto.IsSucess = false;
                }
            }
            catch (Exception ex)
            {
                _responseDto.Message = "Error: " + ex.Message;
                _responseDto.IsSucess = false;
                _responseDto.Result = null;
            }

            return _responseDto;
        }

        [HttpPut]
        //[Authorize(Roles = "Admin")]
        public ResponseDto Put([FromBody] ProductDto productDto)
        {
            try
            {
                if (productDto != null)
                {
                    Product? productFromDb = _db.Products.FirstOrDefault(x => x.Id == productDto.Id && !x.IsDeleted);
                    if (productFromDb != null)
                    {
                        productFromDb.Name = productDto.Name;
                        productFromDb.Price = productDto.Price;
                        productFromDb.Description = productDto.Description;
                        productFromDb.CategoryName = productDto.CategoryName;
                        productFromDb.ImageUrl = productDto.ImageUrl;

                        _db.Products.Update(productFromDb);
                        _db.SaveChanges();

                        _responseDto.Result = productDto.Id;
                        _responseDto.Message = $"Producto con id {productDto.Id} ACTUALIZADO con exito";
                    }
                    else
                    {
                        _responseDto.Result = null;
                        _responseDto.Message = "Producto no encontrado";
                        _responseDto.IsSucess = false;
                    }
                }
                else
                {
                    _responseDto.Result = null;
                    _responseDto.Message = "El producto ingresado no es valido";
                    _responseDto.IsSucess = false;
                }
            }
            catch (Exception ex)
            {
                _responseDto.Message = "Error: " + ex.Message;
                _responseDto.IsSucess = false;
                _responseDto.Result = null;
            }
            return _responseDto;
        }

        [HttpDelete]
        [Route("{id:int}")]
        //[Authorize(Roles = "Admin")]
        public ResponseDto Delete(int id)
        {
            try
            {
                if (id <= 0)
                {
                    _responseDto.Message = "El id del producto ingresado no es valido";
                    _responseDto.IsSucess = false;
                    _responseDto.Result = null;
                }
                else
                {
                    Product? productFromDb = _db.Products.FirstOrDefault(x => x.Id == id && !x.IsDeleted);
                    if (productFromDb != null)
                    {
                        productFromDb.IsDeleted = true;

                        _db.Products.Update(productFromDb);
                        _db.SaveChanges();

                        _responseDto.Result = id;
                        _responseDto.Message = $"Producto {productFromDb.Name} ELIMINADO con exito";
                    }
                    else
                    {
                        _responseDto.Result = null;
                        _responseDto.Message = "No se encontro ningun registro con ese Id";
                        _responseDto.IsSucess = false;
                    }

                }
            }
            catch (Exception ex)
            {
                _responseDto.Message = "Error: " + ex.Message;
                _responseDto.IsSucess = false;
                _responseDto.Result = null;
            }
            return _responseDto;
        }
    }
}

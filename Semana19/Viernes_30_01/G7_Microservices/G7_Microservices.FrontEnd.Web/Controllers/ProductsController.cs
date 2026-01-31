using G7_Microservices.FrontEnd.Web.Models.Dto;
using G7_Microservices.FrontEnd.Web.Models.Dto.Product;
using G7_Microservices.FrontEnd.Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace G7_Microservices.FrontEnd.Web.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> ProductIndex()
        {
            List<ProductDto>? listProducts = new();
            ResponseDto? responseDto = await _productService.GetAllProductsAsync();
            if (responseDto != null && responseDto.IsSucess)
            {
                listProducts = JsonConvert.DeserializeObject<List<ProductDto>>(Convert.ToString(responseDto.Result));
            }
            else
            {
                TempData["error"] = responseDto?.Message;
            }

            return View(listProducts);
        }

        #region Create
        [HttpGet]
        public async Task<IActionResult> ProductCreate()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ProductCreate(ProductDto productDto)
        {
            if (ModelState.IsValid)
            {
                ResponseDto? responseDto = await _productService.CreateProductsAsync(productDto);
                if (responseDto != null && responseDto.IsSucess)
                {
                    TempData["success"] = "Producto creado con exito";
                    return RedirectToAction(nameof(ProductIndex));
                }
                else
                {
                    TempData["error"] = responseDto?.Message;
                    return View(productDto);
                }
            }
            return View(productDto);
        }
        #endregion

        #region Update
        [HttpGet]
        public async Task<IActionResult> ProductEdit(int productId)
        {
            ProductDto? productDto = new ProductDto();

            ResponseDto? responseDto = await _productService.GetProductByIdAsync(productId);
            if (responseDto != null && responseDto.IsSucess)
            {
                productDto = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(responseDto?.Result));

                return View(productDto);
            }
            else
            {
                TempData["error"] = responseDto?.Message;
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> ProductEdit(ProductDto productDto)
        {
            if (ModelState.IsValid)
            {
                ResponseDto? responseDto = await _productService.UpdateProductsAsync(productDto);
                if (responseDto != null && responseDto.IsSucess)
                {
                    TempData["success"] = "Producto actualizado con exito";
                    return RedirectToAction(nameof(ProductIndex));
                }
                else
                {
                    TempData["error"] = responseDto?.Message;
                    return View(productDto);
                }
            }
            return View(productDto);
        }
        #endregion

        #region Delete
        [HttpGet]
        public async Task<IActionResult> ProductDelete(int productId)
        {
            ProductDto? productDto = new ProductDto();

            ResponseDto? responseDto = await _productService.GetProductByIdAsync(productId);
            if (responseDto != null && responseDto.IsSucess)
            {
                productDto = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(responseDto?.Result));

                return View(productDto);
            }
            else
            {
                TempData["error"] = responseDto?.Message;
                return View(productDto);
            }
        }

        [HttpPost]
        public async Task<IActionResult> ProductDelete(ProductDto productDto)
        {
            ResponseDto? responseDto = await _productService.DeleteProductsAsync(productDto.Id);
            if (responseDto != null && responseDto.IsSucess)
            {
                TempData["success"] = "Producto eliminado con exito";
                return RedirectToAction(nameof(ProductIndex));
            }
            else
            {
                TempData["error"] = responseDto?.Message;
                return View();
            }
        }
        #endregion
    }
}

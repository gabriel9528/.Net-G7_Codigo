using G7_Microservices.FrontEnd.Web.Models;
using G7_Microservices.FrontEnd.Web.Models.Dto;
using G7_Microservices.FrontEnd.Web.Models.Dto.Product;
using G7_Microservices.FrontEnd.Web.Models.Dto.ShoppingCart;
using G7_Microservices.FrontEnd.Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;

namespace G7_Microservices.FrontEnd.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductService _productService;
        private readonly IShoppingCartService _shoppingCartService;
        private readonly ILogger<HomeController> _logger;

        public HomeController(IProductService productService, IShoppingCartService shoppingCartService, ILogger<HomeController> logger)
        {
            _productService = productService;
            _shoppingCartService = shoppingCartService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
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

        [HttpGet]
        public async Task<IActionResult> ProductDetails(int productId)
        {
            ProductDto? productDto = new ProductDto();
            ResponseDto? responseDto = await _productService.GetProductByIdAsync(productId);
            if (responseDto != null && responseDto.IsSucess)
            {
                productDto = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(responseDto.Result));
            }
            else
            {
                TempData["error"] = responseDto?.Message;
            }
            return View(productDto);
        }

        [HttpPost]
        public async Task<IActionResult> ProductDetails(ProductDto productDto)
        {
            CartDto cartDto = new CartDto()
            {
                CartHeaderDto = new CartHeaderDto()
                {
                    UserId = User.Claims.Where(x => x.Type == JwtRegisteredClaimNames.Sub).FirstOrDefault().Value
                }
            };

            CartDetailsDto cartDetailsDto = new CartDetailsDto()
            {
                Count = productDto.Count,
                ProductId = productDto.Id
            };

            List<CartDetailsDto> cartDetailsDtos = new() { cartDetailsDto };
            cartDto.CartDetailsDtos = cartDetailsDtos;

            ResponseDto? responseDto = await _shoppingCartService.UpsertCartAsync(cartDto);
            if (responseDto != null && responseDto.IsSucess)
            {
                TempData["success"] = "Item agregado exitosamente al shopping cart";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["error"] = responseDto?.Message;
            }

            return View(productDto);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

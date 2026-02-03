using G7_Microservices.FrontEnd.Web.Models.Dto;
using G7_Microservices.FrontEnd.Web.Models.Dto.ShoppingCart;
using G7_Microservices.FrontEnd.Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;

namespace G7_Microservices.FrontEnd.Web.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IShoppingCartService _shoppingCartService;

        public ShoppingCartController(IShoppingCartService shoppingCartService)
        {
            _shoppingCartService = shoppingCartService;
        }

        [HttpGet]
        public async Task<IActionResult> ShoppingCartIndex()
        {
            var cartDto = await LoadCartDtoBassedOnLoggedInUser();
            return View(cartDto);
        }


        public async Task<IActionResult> RemoveCart(int cartDeailsId)
        {
            ResponseDto responseDto = await _shoppingCartService.RemoveCartAsync(cartDeailsId);
            if (responseDto != null && responseDto.IsSucess)
            {
                TempData["success"] = "Cart eliminado exitosamente";
                return RedirectToAction(nameof(ShoppingCartIndex));
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ApplyCoupon(CartDto applyCouponDto)
        {
            ResponseDto responseDto = await _shoppingCartService.ApplyCouponAsync(applyCouponDto);
            if (responseDto != null && responseDto.IsSucess)
            {
                TempData["success"] = "Cupon aplicado exitosamente";
                return RedirectToAction(nameof(ShoppingCartIndex));
            }
            return View();
        }

        public async Task<IActionResult> RemoveProduct(int cartDetailsId)
        {
            var userId = User.Claims.Where(x => x.Type == JwtRegisteredClaimNames.Sub)?.FirstOrDefault()?.Value;
            ResponseDto responseDto = await _shoppingCartService.RemoveCartAsync(cartDetailsId);
            if (responseDto != null && responseDto.IsSucess)
            {
                TempData["success"] = "Cupon eliminado exitosamente";
                return RedirectToAction(nameof(ShoppingCartIndex));
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RemoveCoupon(CartDto cartDto)
        {
            cartDto.CartHeaderDto.CouponCode = "";
            ResponseDto responseDto = await _shoppingCartService.ApplyCouponAsync(cartDto);
            if (responseDto != null && responseDto.IsSucess)
            {
                TempData["success"] = "Cupon aplicado exitosamente";
                return RedirectToAction(nameof(ShoppingCartIndex));
            }
            return View();
        }

        private async Task<CartDto> LoadCartDtoBassedOnLoggedInUser()
        {
            var userId = User.Claims.Where(x=>x.Type==JwtRegisteredClaimNames.Sub)?.FirstOrDefault()?.Value;
            ResponseDto responseDto = await _shoppingCartService.GetCartByUserIdAsync(userId);
            if(responseDto != null && responseDto.IsSucess)
            {
                CartDto cartDto = JsonConvert.DeserializeObject<CartDto>(Convert.ToString(responseDto.Result));
                return cartDto;
            }
            return new CartDto();
        }
    }
}

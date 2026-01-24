using G7_Microservices.FrontEnd.Web.Models.Dto;
using G7_Microservices.FrontEnd.Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace G7_Microservices.FrontEnd.Web.Controllers
{
    public class CouponsController : Controller
    {
        private readonly ICouponService _couponService;
        public CouponsController(ICouponService couponService)
        {
            _couponService = couponService;
        }

        [HttpGet]
        public async Task<IActionResult> CouponIndex()
        {
            List<CouponDto> listCoupon = new List<CouponDto>();
            ResponseDto? responseDto = await _couponService.GetAllCouponsAsync();
            if (responseDto != null && responseDto.IsSucess)
            {
                listCoupon = JsonConvert.DeserializeObject<List<CouponDto>>(Convert.ToString(responseDto.Result));
            }
            else
            {
                TempData["error"] = responseDto?.Message;
            }
            return View(listCoupon);
        }
    }
}

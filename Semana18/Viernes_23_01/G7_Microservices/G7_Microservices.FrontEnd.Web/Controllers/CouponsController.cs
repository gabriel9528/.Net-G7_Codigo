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

        #region Create
        [HttpGet]
        public async Task<IActionResult> CouponCreate()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CouponCreate(CouponRequestDto couponRequestDto)
        {
            if (ModelState.IsValid)
            {
                ResponseDto? responseDto = await _couponService.CreateCouponAsync(couponRequestDto);
                if(responseDto != null && responseDto.IsSucess)
                {
                    TempData["success"] = "Cupon creado con exito";
                    return RedirectToAction(nameof(CouponIndex));
                }
                else
                {
                    TempData["error"] = responseDto?.Message;
                    return View(couponRequestDto);
                }
            }
            return View(couponRequestDto);
        }
        #endregion

        #region Update
        [HttpGet]
        public async Task<IActionResult> CouponEdit(int couponId)
        {
            CouponDto? couponDto = new CouponDto();
            CouponRequestDto? couponRequestDto = new CouponRequestDto();
            ResponseDto? responseDto = await _couponService.GetCouponByIdAsync(couponId);
            if(responseDto != null && responseDto.IsSucess)
            {
                couponDto = JsonConvert.DeserializeObject<CouponDto>(Convert.ToString(responseDto?.Result));
                if(couponDto != null)
                {
                    couponRequestDto.Id = couponDto.Id;
                    couponRequestDto.Code = couponDto.Code;
                    couponRequestDto.DiscountAmount = couponDto.DiscountAmount;
                    couponRequestDto.MinimunAmount = couponDto.MinimunAmount;
                }
                return View(couponRequestDto);
            }
            else
            {
                TempData["error"] = responseDto?.Message;
                return View(couponRequestDto);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CouponEdit(CouponRequestDto couponRequestDto)
        {
            if (ModelState.IsValid)
            {
                ResponseDto? responseDto = await _couponService.UpdateCouponAsync(couponRequestDto);
                if (responseDto != null && responseDto.IsSucess)
                {
                    TempData["success"] = "Cupon actualizado con exito";
                    return RedirectToAction(nameof(CouponIndex));
                }
                else
                {
                    TempData["error"] = responseDto?.Message;
                    return View(couponRequestDto);
                }
            }
            return View(couponRequestDto);
        }
        #endregion

        #region Delete
        [HttpGet]
        public async Task<IActionResult> CouponDelete(int couponId)
        {
            CouponDto? couponDto = new CouponDto();
            
            ResponseDto? responseDto = await _couponService.GetCouponByIdAsync(couponId);
            if (responseDto != null && responseDto.IsSucess)
            {
                couponDto = JsonConvert.DeserializeObject<CouponDto>(Convert.ToString(responseDto?.Result));
                
                return View(couponDto);
            }
            else
            {
                TempData["error"] = responseDto?.Message;
                return View(couponDto);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CouponDelete(CouponDto couponDto)
        {
            ResponseDto? responseDto = await _couponService.DeleteCouponAsync(couponDto.Id);
            if (responseDto != null && responseDto.IsSucess)
            {
                TempData["success"] = "Cupon eliminado con exito";
                return RedirectToAction(nameof(CouponIndex));
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

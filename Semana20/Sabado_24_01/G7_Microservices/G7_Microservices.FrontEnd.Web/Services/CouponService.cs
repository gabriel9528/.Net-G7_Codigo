using G7_Microservices.FrontEnd.Web.Models.Dto;
using G7_Microservices.FrontEnd.Web.Models.Dto.Coupon;
using G7_Microservices.FrontEnd.Web.Services.IServices;
using G7_Microservices.FrontEnd.Web.Utility;

namespace G7_Microservices.FrontEnd.Web.Services
{
    public class CouponService : ICouponService
    {
        private readonly IBaseService _baseService;
        public CouponService(IBaseService baseService)
        {
            _baseService = baseService;
        }
        
        //Post
        public async Task<ResponseDto?> CreateCouponAsync(CouponRequestDto couponRequestDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                API_TYPE = SD.API_TYPE.POST,
                Data = couponRequestDto,
                Url = SD.CouponAPIBase + "/api/CouponsAPI"
            });
        }

        //Put
        public async Task<ResponseDto?> UpdateCouponAsync(CouponRequestDto couponRequestDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                API_TYPE = SD.API_TYPE.PUT,
                Data = couponRequestDto,
                Url = SD.CouponAPIBase + "/api/CouponsAPI"
            });
        }

        //Delete
        public async Task<ResponseDto?> DeleteCouponAsync(int id)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                API_TYPE = SD.API_TYPE.DELETE,
                Url = SD.CouponAPIBase + "/api/CouponsAPI/" + id
            });
        }

        //GetAll
        public async Task<ResponseDto?> GetAllCouponsAsync()
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                API_TYPE = SD.API_TYPE.GET,
                Url = SD.CouponAPIBase + "/api/CouponsAPI"
            });
        }

        //GetByCode
        public async Task<ResponseDto?> GetCouponByCodeAsync(string code)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                API_TYPE = SD.API_TYPE.GET,
                Url = SD.CouponAPIBase + "/api/CouponsAPI/" + $"getByCode/{code}"
            });
        }

        //GetById
        public async Task<ResponseDto?> GetCouponByIdAsync(int id)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                API_TYPE = SD.API_TYPE.GET,
                Url = SD.CouponAPIBase + "/api/CouponsAPI/" + id
            });
        }

    }
}

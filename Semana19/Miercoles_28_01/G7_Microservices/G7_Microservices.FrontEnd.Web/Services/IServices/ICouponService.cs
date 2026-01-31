using G7_Microservices.FrontEnd.Web.Models.Dto;
using G7_Microservices.FrontEnd.Web.Models.Dto.Coupon;

namespace G7_Microservices.FrontEnd.Web.Services.IServices
{
    public interface ICouponService
    {
        Task<ResponseDto?> GetCouponByIdAsync(int id);
        Task<ResponseDto?> GetAllCouponsAsync();
        Task<ResponseDto?> GetCouponByCodeAsync(string code);
        Task<ResponseDto?> CreateCouponAsync(CouponRequestDto couponRequestDto);
        Task<ResponseDto?> UpdateCouponAsync(CouponRequestDto couponRequestDto);
        Task<ResponseDto?> DeleteCouponAsync(int id);
    }
}

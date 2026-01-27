using G7_Microservices.FrontEnd.Web.Models.Dto;
using G7_Microservices.FrontEnd.Web.Models.Dto.Auth;
using G7_Microservices.FrontEnd.Web.Services.IServices;
using G7_Microservices.FrontEnd.Web.Utility;
using static G7_Microservices.FrontEnd.Web.Utility.SD;

namespace G7_Microservices.FrontEnd.Web.Services
{
    public class AuthService : IAuthService
    {
        private readonly IBaseService _baseService;
        public AuthService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<ResponseDto?> AssignRoleAsync(RegisterRequestDto registerRequestDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                API_TYPE = API_TYPE.POST,
                Data = registerRequestDto,
                Url = SD.AuthAPIBase + "/api/AuthAPI/assignRole"
            });
        }

        public async Task<ResponseDto?> LoginAsync(LoginRequestDto loginRequestDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                API_TYPE = API_TYPE.POST,
                Data = loginRequestDto,
                Url = SD.AuthAPIBase + "/api/AuthAPI/login"
            }, withBearer: false);
        }

        public async Task<ResponseDto?> RegisterAsync(RegisterRequestDto registerRequestDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                API_TYPE = API_TYPE.POST,
                Data = registerRequestDto,
                Url = SD.AuthAPIBase + "/api/AuthAPI/register"
            }, withBearer: false);
        }
    }
}

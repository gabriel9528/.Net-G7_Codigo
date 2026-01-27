using G7_Microservices.Backend.AuthAPI.Models.Dto;

namespace G7_Microservices.Backend.AuthAPI.Services.IServices
{
    public interface IAuthService
    {
        Task<string> Register(RegisterRequestDto registerRequestDto);
        Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);
        Task<bool> AssignRole(string email, string roleName);
    }
}

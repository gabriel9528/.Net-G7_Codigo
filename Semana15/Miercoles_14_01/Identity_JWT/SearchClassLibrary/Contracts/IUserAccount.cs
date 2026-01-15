using SearchClassLibrary.Dto;
using static SearchClassLibrary.Dto.ServiceResponse;

namespace SearchClassLibrary.Contracts
{
    public interface IUserAccount
    {
        Task<GeneralResponse> RegisterAccountAsync(RegisterDto registerDto);
        Task<LoginResponse> LoginAccountAsync(LoginDto loginDto);
    }
}

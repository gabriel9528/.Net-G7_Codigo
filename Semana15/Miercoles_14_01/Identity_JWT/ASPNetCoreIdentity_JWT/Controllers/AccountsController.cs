using Microsoft.AspNetCore.Mvc;
using SearchClassLibrary.Contracts;
using SearchClassLibrary.Dto;

namespace ASPNetCoreIdentity_JWT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IUserAccount _userAccount;
        public AccountsController(IUserAccount userAccount)
        {
            _userAccount = userAccount;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            var response = await _userAccount.RegisterAccountAsync(registerDto);
            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var response = await _userAccount.LoginAccountAsync(loginDto);
            return Ok(response);
        }

    }
}

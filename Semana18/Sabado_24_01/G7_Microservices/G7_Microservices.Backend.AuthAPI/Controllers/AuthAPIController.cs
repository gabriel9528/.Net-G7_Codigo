using G7_Microservices.Backend.AuthAPI.Models.Dto;
using G7_Microservices.Backend.AuthAPI.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace G7_Microservices.Backend.AuthAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthAPIController : ControllerBase
    {
        private readonly IAuthService _authService;
        protected ResponseDto _responseDto;

        public AuthAPIController(IAuthService authService)
        {
            _authService = authService;
            _responseDto = new ResponseDto();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
        {
            var errorMessage = await _authService.Register(registerRequestDto);
            if(!string.IsNullOrEmpty(errorMessage))
            {
                _responseDto.IsSucess = false;
                _responseDto.Message = errorMessage;
                _responseDto.Result = false;
                
                return BadRequest(_responseDto);
            }
            else
            {
                _responseDto.Message = "Usuario registrado con exito";
                _responseDto.Result = true;

                return Ok(_responseDto);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            var loginResponse = await _authService.Login(loginRequestDto);
            if(loginResponse.User == null)
            {
                _responseDto.IsSucess = false;
                _responseDto.Message = "Usuario o contraseña incorrecto";
                _responseDto.Result = false;

                return BadRequest(_responseDto);
            }
            else
            {
                _responseDto.Message = "Usuario logueado con exito";
                _responseDto.Result = loginResponse;

                return Ok(_responseDto);
            }
        }

        [HttpPost("assignRole")]
        public async Task<IActionResult> AssignRole([FromBody] RegisterRequestDto registerRequestDto)
        {
            var assignRole = await _authService.AssignRole(registerRequestDto.Email, registerRequestDto.Role);
            if (!assignRole)
            {
                _responseDto.IsSucess = false;
                _responseDto.Message = "Error encontrado al asignar el rol";
                _responseDto.Result = false;

                return BadRequest(_responseDto);
            }
            else
            {
                _responseDto.Message = "Rol asignado exitosamente";
                _responseDto.Result = true;

                return Ok(_responseDto);
            }
        }
    }
}

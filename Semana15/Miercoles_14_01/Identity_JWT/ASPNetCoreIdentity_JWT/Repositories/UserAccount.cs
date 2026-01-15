using ASPNetCoreIdentity_JWT.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using SearchClassLibrary.Contracts;
using SearchClassLibrary.Dto;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ASPNetCoreIdentity_JWT.Repositories
{
    public class UserAccount : IUserAccount
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public UserAccount(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        public async Task<ServiceResponse.GeneralResponse> RegisterAccountAsync(RegisterDto registerDto)
        {
            if (registerDto == null) return new ServiceResponse.GeneralResponse(false, "Modelo vacio...");

            var newUser = new ApplicationUser()
            {
                Name = registerDto.Name,
                Email = registerDto.Email,
                PasswordHash = registerDto.Password,
                UserName = registerDto.Email
            };

            var oldUser = await _userManager.FindByEmailAsync(newUser.Email);
            if (oldUser != null) return new ServiceResponse.GeneralResponse(false, "Usuario ya registrado");

            var createdUser = await _userManager.CreateAsync(newUser, registerDto.Password);
            if (!createdUser.Succeeded) return new ServiceResponse.GeneralResponse(false, "Ups, ocurio un error durante la creacion del usuario");

            var checkRoleAdmin = await _roleManager.FindByNameAsync("Admin");
            if(checkRoleAdmin == null)
            {
                await _roleManager.CreateAsync(new IdentityRole() { Name = "Admin" });
                await _userManager.AddToRoleAsync(newUser, "Admin");
                return new ServiceResponse.GeneralResponse(true, "Usuario creado con exito, con rol Admin");
            }
            else
            {
                var checkRoleUser = await _roleManager.FindByNameAsync("User");
                if(checkRoleUser == null)
                {
                    await _roleManager.CreateAsync(new IdentityRole() { Name = "User" });
                    await _userManager.AddToRoleAsync(newUser, "Admin");
                    return new ServiceResponse.GeneralResponse(true, "Usuario creado con exito, con rol User");
                }
                return new ServiceResponse.GeneralResponse(false, "No se pudo crear el usuario");
            }
        }

        public async Task<ServiceResponse.LoginResponse> LoginAccountAsync(LoginDto loginDto)
        {
            if (loginDto == null) return new ServiceResponse.LoginResponse(false, null, "Modelo vacio..");

            var getUser = await _userManager.FindByEmailAsync(loginDto.Email);
            if (getUser == null) return new ServiceResponse.LoginResponse(false, null, "Usuario no encontrado.");

            var checkUserPassword = await _userManager.CheckPasswordAsync(getUser, loginDto.Password);
            if (!checkUserPassword) return new ServiceResponse.LoginResponse(false, null, "Contraseña invalida");

            var getUserRole = await _userManager.GetRolesAsync(getUser);
            var userSession = new UserSession(getUser.Id, getUser.Name, getUser.Email, getUserRole.FirstOrDefault());

            string token = GenerateToken(userSession);

            return new ServiceResponse.LoginResponse(true, token, "Login completado exitosamente.");

        }

        private string GenerateToken(UserSession userSession)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var userClaims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userSession.id),
                new Claim(ClaimTypes.Name, userSession.name),
                new Claim(ClaimTypes.Email, userSession.email),
                new Claim(ClaimTypes.Role, userSession.role)
            };
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: userClaims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: signingCredentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

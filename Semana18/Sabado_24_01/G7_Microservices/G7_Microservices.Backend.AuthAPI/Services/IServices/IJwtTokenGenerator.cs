using G7_Microservices.Backend.AuthAPI.Models;

namespace G7_Microservices.Backend.AuthAPI.Services.IServices
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(ApplicationUser applicationUser, IEnumerable<string> roles);
    }
}

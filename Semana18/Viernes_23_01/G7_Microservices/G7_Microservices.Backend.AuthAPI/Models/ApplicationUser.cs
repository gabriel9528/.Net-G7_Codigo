using Microsoft.AspNetCore.Identity;

namespace G7_Microservices.Backend.AuthAPI.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Names { get; set; }
    }
}

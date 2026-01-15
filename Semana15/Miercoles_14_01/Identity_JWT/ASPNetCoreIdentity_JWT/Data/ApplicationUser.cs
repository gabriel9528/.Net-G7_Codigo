using Microsoft.AspNetCore.Identity;

namespace ASPNetCoreIdentity_JWT.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
    }
}

using G7_Microservices.Backend.ShoppingCartAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace G7_Microservices.Backend.ShoppingCartAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<CartHeader> CartHeaders { get; set; }
        public DbSet<CartDetails> CartDetails { get; set; }
    }
}

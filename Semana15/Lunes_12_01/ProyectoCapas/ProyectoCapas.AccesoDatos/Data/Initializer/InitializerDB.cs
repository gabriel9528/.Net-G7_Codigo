using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProyectoCapas.Data;
using ProyectoCapas.Models;
using ProyectoCapas.Utilidades;

namespace ProyectoCapas.AccesoDatos.Data.Initializer
{
    public class InitializerDB : IInitializerDB
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public InitializerDB(ApplicationDbContext db, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void Initialize()
        {
            try
            {
                if(_db.Database.GetPendingMigrations().Count() > 0)
                {
                    _db.Database.Migrate();
                }
            }
            catch (Exception ex)
            {
                new Exception(ex.Message);
            }

            if (_db.Roles.Any(x => x.Name == Roles.Administrator)) return;

            //Crear Roles
            _roleManager.CreateAsync(new IdentityRole(Roles.Administrator)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(Roles.Customer)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(Roles.User)).GetAwaiter().GetResult();

            //Crear usuario Administrador
            _userManager.CreateAsync(new ApplicationUser
            {
                UserName = "gabriel@gmail.com",
                Email = "gabriel@gmail.com",
                Name = "Gabriel",
                EmailConfirmed = true
            }, "Admin123@").GetAwaiter().GetResult();

            ApplicationUser user = _db.ApplicationUsers.Where(x => x.Email == "gabriel@gmail.com").FirstOrDefault();

            //Le asignamos el rol de administrador
            _userManager.AddToRoleAsync(user, Roles.Administrator).GetAwaiter().GetResult();

        }
    }
}

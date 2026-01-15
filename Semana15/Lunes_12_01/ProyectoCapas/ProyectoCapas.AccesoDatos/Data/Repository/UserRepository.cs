using ProyectoCapas.AccesoDatos.Data.Repository.IRepository;
using ProyectoCapas.Data;
using ProyectoCapas.Models;

namespace ProyectoCapas.AccesoDatos.Data.Repository
{
    public class UserRepository : Repository<ApplicationUser>, IUserRepository
    {
        private readonly ApplicationDbContext _db;
        public UserRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void BlockUser(string userId)
        {
            var userFromDb = _db.ApplicationUsers.FirstOrDefault(x => x.Id == userId);
            if (userFromDb != null)
            {
                userFromDb.LockoutEnd = DateTime.Now.AddYears(1000);
            }
            _db.SaveChanges();
        }

        public void UnBlockUser(string userId)
        {
            var userFromDb = _db.ApplicationUsers.FirstOrDefault(x => x.Id == userId);
            if (userFromDb != null)
            {
                userFromDb.LockoutEnd = DateTime.Now;
            }
            _db.SaveChanges();
        }
    }
}

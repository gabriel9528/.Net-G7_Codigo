using ProyectoCapas.Models;

namespace ProyectoCapas.AccesoDatos.Data.Repository.IRepository
{
    public interface IUserRepository : IRepository<ApplicationUser>
    {
        void BlockUser(string userId);
        void UnBlockUser(string userId);
    }
}

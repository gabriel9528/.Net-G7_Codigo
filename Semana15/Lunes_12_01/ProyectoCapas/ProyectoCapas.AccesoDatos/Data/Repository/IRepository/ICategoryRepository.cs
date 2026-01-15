using Microsoft.AspNetCore.Mvc.Rendering;
using ProyectoCapas.Models;

namespace ProyectoCapas.AccesoDatos.Data.Repository.IRepository
{
    public interface ICategoryRepository : IRepository<Category>
    {
        void Update(Category category);
        void Delete(int id);
        IEnumerable<SelectListItem> GetListCategories();
    }
}

using Microsoft.AspNetCore.Mvc.Rendering;
using ProyectoCapas.AccesoDatos.Data.Repository.IRepository;
using ProyectoCapas.Data;
using ProyectoCapas.Models;

namespace ProyectoCapas.AccesoDatos.Data.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CategoryRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public void Delete(int id)
        {
            var categoryFromDb = _dbContext.Categories.FirstOrDefault(x => x.Id == id);
            if (categoryFromDb != null)
            {
                categoryFromDb.IsActive = false;
            }
            _dbContext.SaveChanges();
        }

        public IEnumerable<SelectListItem> GetListCategories()
        {
            return _dbContext.Categories.Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString()
            });
        }

        public void Update(Category category)
        {
            var categoryFromDb = _dbContext.Categories.FirstOrDefault(x => x.Id == category.Id);
            if (categoryFromDb != null)
            {
                categoryFromDb.Name = category.Name;
                categoryFromDb.Order = category.Order;
            }

            _dbContext.SaveChanges();
        }
    }
}

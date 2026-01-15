using ProyectoCapas.Models;

namespace ProyectoCapas.AccesoDatos.Data.Repository.IRepository
{
    public interface IArticleRepository : IRepository<Article>
    {
        void Update(Article article);
        void Delete(int id);
        IQueryable<Article> AsQueryable();
    }
}

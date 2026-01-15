using ProyectoCapas.AccesoDatos.Data.Repository.IRepository;
using ProyectoCapas.Data;

namespace ProyectoCapas.AccesoDatos.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            ICategoryRepository = new CategoryRepository(dbContext);
            IArticleRepository = new ArticleRepository(dbContext);
            ISliderRepository = new SliderRepository(dbContext);
            IUserRepository = new UserRepository(dbContext);
        }

        public ICategoryRepository ICategoryRepository { get; private set; }
        public IArticleRepository IArticleRepository { get; private set; }
        public ISliderRepository ISliderRepository { get; private set; }
        public IUserRepository IUserRepository { get; private set; }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}

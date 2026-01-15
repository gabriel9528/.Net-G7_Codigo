namespace ProyectoCapas.AccesoDatos.Data.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        ICategoryRepository ICategoryRepository { get; }
        IArticleRepository IArticleRepository { get; }
        ISliderRepository ISliderRepository { get; }
        IUserRepository IUserRepository { get; }

        void Save();
    }
}

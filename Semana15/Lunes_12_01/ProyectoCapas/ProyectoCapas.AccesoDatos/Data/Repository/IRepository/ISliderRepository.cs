using ProyectoCapas.Models;

namespace ProyectoCapas.AccesoDatos.Data.Repository.IRepository
{
    public interface ISliderRepository : IRepository<Slider>
    {
        void Update(Slider slider);
        void Delete(int id);
    }
}

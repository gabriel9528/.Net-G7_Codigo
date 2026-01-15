using ProyectoCapas.AccesoDatos.Data.Repository.IRepository;
using ProyectoCapas.Data;
using ProyectoCapas.Models;

namespace ProyectoCapas.AccesoDatos.Data.Repository
{
    public class SliderRepository : Repository<Slider>, ISliderRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public SliderRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public void Delete(int id)
        {
            var sliderFromDb = _dbContext.Sliders.FirstOrDefault(s => s.Id == id);
            if (sliderFromDb != null)
            {
                sliderFromDb.IsActive = false;
            }
            _dbContext.SaveChanges();
        }

        public void Update(Slider slider)
        {
            var sliderFromDb = _dbContext.Sliders.FirstOrDefault(s => s.Id == slider.Id);
            if (sliderFromDb != null)
            {
                sliderFromDb.Name = slider.Name;
                sliderFromDb.State = slider.State;
                sliderFromDb.UrlImage = slider.UrlImage;
            }
            _dbContext.SaveChanges();
        }
    }
}

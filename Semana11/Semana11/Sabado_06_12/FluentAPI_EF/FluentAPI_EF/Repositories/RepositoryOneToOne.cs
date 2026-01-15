using FluentAPI_EF.Data;
using FluentAPI_EF.Models.OneToOne;
using Microsoft.EntityFrameworkCore;

namespace FluentAPI_EF.Repositories
{
    public class RepositoryOneToOne
    {
        private readonly ApplicationDbContext _context;
        public RepositoryOneToOne(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<CarCompany>> GetCarCompaniesAsync() =>
            await _context.CarCompanies.Include(x => x.CarModel).ToListAsync();

        public async Task<List<CarModel>> GetCarModelsAsync() =>
            await _context.CarModels.Include(a => a.CarCompany).ToListAsync();

        public async Task AddCarCompany(CarCompany carCompany)
        {
            _context.CarCompanies.Add(carCompany);
            await _context.SaveChangesAsync();
        }

        public async Task AddCarModel(CarModel carModel)
        {
            _context.CarModels.Add(carModel);
            await _context.SaveChangesAsync();
        }
    }
}

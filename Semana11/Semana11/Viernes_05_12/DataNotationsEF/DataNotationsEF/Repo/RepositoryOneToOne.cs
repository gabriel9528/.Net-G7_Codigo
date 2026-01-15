using DataNotationsEF.Data;
using DataNotationsEF.Models.OneToOne;
using Microsoft.EntityFrameworkCore;

namespace DataNotationsEF.Repo
{
    public class RepositoryOneToOne
    {
        private readonly ApplicationDbContext _context;

        public RepositoryOneToOne(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<CarCompany>> GetCarCompaniesAsync() =>
            await _context.CarCompanies.ToListAsync();

        public async Task<List<CarModel>> GetCarModelssAsync() =>
            await _context.CarModels.ToListAsync();

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

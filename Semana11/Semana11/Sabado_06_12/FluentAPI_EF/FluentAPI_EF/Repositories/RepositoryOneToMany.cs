using FluentAPI_EF.Data;
using FluentAPI_EF.Models.OneToMany;
using Microsoft.EntityFrameworkCore;

namespace FluentAPI_EF.Repositories
{
    public class RepositoryOneToMany
    {
        private readonly ApplicationDbContext _context;
        public RepositoryOneToMany(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddDoctorAsync(Doctor doctor)
        {
            try
            {
                _context.Doctors.Add(doctor);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task AddPatientAsync(Patient patient)
        {
            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Doctor>> GetDoctorsAsync() =>
            await _context.Doctors.ToListAsync();

        public async Task<List<Patient>> GetPatientsAsync() =>
            await _context.Patients.ToListAsync();

    }
}

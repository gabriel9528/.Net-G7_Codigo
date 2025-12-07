using DataNotationsEF.Data;
using DataNotationsEF.Models.OneToMany;
using Microsoft.EntityFrameworkCore;

namespace DataNotationsEF.Repo
{
    public class RepositoryOneToMany
    {
        private readonly ApplicationDbContext _context;

        public RepositoryOneToMany(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Patient>> GetPatientsAsync() =>
            await _context.Patients.Include(x => x.Doctor).ToListAsync();

        public async Task<List<Doctor>> GetDoctorAsync() =>
            await _context.Doctors.Include(x => x.Patients).ToListAsync();

        public async Task AddDoctor(Doctor doctor)
        {
            _context.Doctors.Add(doctor);
            await _context.SaveChangesAsync();
        }

        public async Task AddPatient(Patient patient)
        {
            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();
        }
    }
}

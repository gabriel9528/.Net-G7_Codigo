using DataNotationsEF.Models.ManyToMany;
using DataNotationsEF.Models.OneToMany;
using DataNotationsEF.Models.OneToOne;
using Microsoft.EntityFrameworkCore;

namespace DataNotationsEF.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        //One To One
        public DbSet<CarModel> CarModels { get; set; }
        public DbSet<CarCompany> CarCompanies { get; set; }

        //One To Many
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }

        //Many To many
        public DbSet<Student> Students { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<StudentSubject> StudentSubjects { get; set; }
    }
}

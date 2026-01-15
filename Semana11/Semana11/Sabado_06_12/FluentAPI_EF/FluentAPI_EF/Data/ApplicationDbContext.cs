using FluentAPI_EF.Models.ManyToMany;
using FluentAPI_EF.Models.OneToMany;
using FluentAPI_EF.Models.OneToOne;
using Microsoft.EntityFrameworkCore;

namespace FluentAPI_EF.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<CarCompany> CarCompanies { get; set; }
        public DbSet<CarModel> CarModels { get; set; }

        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }


        public DbSet<Student> Students { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<StudentSubject> StudentSubjects { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //One To One
            modelBuilder.Entity<CarCompany>()
                .HasOne(x => x.CarModel)
                .WithOne(a => a.CarCompany)
                .HasForeignKey<CarModel>(p => p.CarCompanyId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            //One To Many
            modelBuilder.Entity<Doctor>()
                .HasMany(x => x.Patients)
                .WithOne(b => b.Doctor)
                .HasForeignKey(z => z.DoctorsIds)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            //Many to many
            modelBuilder.Entity<StudentSubject>(entity =>
            {
                entity.HasKey(x => new { x.StudentId, x.SubjectId });

                entity
                .HasOne(a => a.Student)
                .WithMany(x => x.StudentSubjects)
                .HasForeignKey(ss => ss.StudentId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

                entity
                .HasOne(p => p.Subject)
                .WithMany(x => x.StudentSubjects)
                .HasForeignKey(tt => tt.SubjectId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Doctors.Domain.Entities;

namespace AnaPrevention.GeneralMasterData.Api.Doctors.Configuration
{
    public class DoctorConfig : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            builder.ToTable("doctors").HasKey(k => k.Id);
            builder.Property(p => p.Photo).IsRequired().IsUnicode(false);
            builder.Property(p => p.Certifications).IsRequired().IsUnicode(false);
            builder.Property(p => p.Signs).IsRequired().IsUnicode(false);
            builder.Property(p => p.PersonId).IsRequired();
            builder.Property(p => p.Code).HasMaxLength(CommonStatic.CodeMaxLength).IsRequired().IsUnicode(false);
            builder.Property(p => p.Status).IsRequired();
            builder.HasOne(c => c.Person).WithMany().HasForeignKey(c => c.PersonId).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany<DoctorSpecialty>().WithOne(p => p.Doctor).HasForeignKey(p => p.DoctorId);
            builder.HasIndex(p => p.PersonId).IsUnique();
        }
    }
}

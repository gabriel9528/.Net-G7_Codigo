using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Doctors.Domain.Entities;

namespace AnaPrevention.GeneralMasterData.Api.Doctors.Configuration
{
    public class DoctorSpecialtyConfig : IEntityTypeConfiguration<DoctorSpecialty>
    {
        public void Configure(EntityTypeBuilder<DoctorSpecialty> builder)
        {
            builder.ToTable("doctorSpecialties").HasKey(k => k.Id);
            builder.Property(p => p.SpecialtyId).IsRequired();
            builder.Property(p => p.DoctorId).IsRequired();
            builder.Property(p => p.Code).HasMaxLength(CommonStatic.CodeMaxLength).IsRequired().IsUnicode(false);
            builder.HasOne(c => c.Doctor).WithMany().HasForeignKey(c => c.DoctorId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(c => c.Specialty).WithMany().HasForeignKey(c => c.SpecialtyId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}

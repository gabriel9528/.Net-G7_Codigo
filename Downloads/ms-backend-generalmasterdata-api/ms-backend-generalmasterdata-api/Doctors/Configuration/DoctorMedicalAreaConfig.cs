using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AnaPrevention.GeneralMasterData.Api.Doctors.Domain.Entities;

namespace AnaPrevention.GeneralMasterData.Api.Doctors.Configuration
{
    public class DoctorMedicalAreaConfig : IEntityTypeConfiguration<DoctorMedicalArea>
    {
        public void Configure(EntityTypeBuilder<DoctorMedicalArea> builder)
        {
            builder.ToTable("doctorMedicalAreas").HasKey(k => k.Id);
            builder.Property(p => p.MedicalAreaId).IsRequired();
            builder.Property(p => p.DoctorId).IsRequired();
            builder.HasOne(c => c.Doctor).WithMany().HasForeignKey(c => c.DoctorId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(c => c.MedicalArea).WithMany().HasForeignKey(c => c.MedicalAreaId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Subsidiaries.Domain.Entities;

namespace AnaPrevention.GeneralMasterData.Api.Subsidiaries.Configuration
{
    public class SubsidiaryConfig : IEntityTypeConfiguration<Subsidiary>
    {
        public void Configure(EntityTypeBuilder<Subsidiary> builder)
        {
            builder.ToTable("subsidiaries").HasKey(k => k.Id);
            builder.Property(p => p.Description).HasMaxLength(CommonStatic.DescriptionMaxLength).IsRequired().IsUnicode(false);
            builder.Property(p => p.Code).HasMaxLength(CommonStatic.CodeMaxLength).IsRequired().IsUnicode(false);
            builder.Property(p => p.DistrictId).HasMaxLength(CommonStatic.CodeMaxLength).IsRequired().IsUnicode(false);
            builder.Property(p => p.Address).HasMaxLength(CommonStatic.DescriptionMaxLength).IsRequired().IsUnicode(false);
            builder.Property(p => p.PhoneNumber).HasMaxLength(CommonStatic.PhoneNumberMaxLenght).IsRequired(false).IsUnicode(false);
            builder.Property(p => p.LedgerAccount).HasMaxLength(CommonStatic.DescriptionMaxLength).IsRequired(false).IsUnicode(false);
            builder.Property(p => p.LogoUrl).HasMaxLength(CommonStatic.DescriptionMaxLength).IsRequired(false).IsUnicode(false);
            builder.Property(p => p.SubsidiaryTypeId).IsRequired();
            builder.Property(p => p.GeoLocation).IsRequired(false).IsUnicode(false);
            builder.Property(p => p.Capacity).IsRequired();
            builder.Property(p => p.DoctorId).IsRequired(false);
            builder.Property(p => p.OfficeHours).IsRequired(false).IsUnicode(false);
            builder.Property(p => p.EmailForAppointment).IsRequired(false).IsUnicode(false);
            builder.Property(p => p.CompanyId).IsRequired();
            builder.Property(p => p.Status).IsRequired();
            builder.Property(p => p.CamoDoctorId).IsRequired(false);
            builder.Property(p => p.DistributionListEmail).IsRequired(false);
            builder.Property(p => p.DistributionListEmaillaboratory).IsRequired(false);
            builder.HasOne(c => c.District).WithMany().HasForeignKey(c => c.DistrictId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(c => c.Doctor).WithMany().HasForeignKey(c => c.DoctorId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(c => c.SubsidiaryType).WithMany().HasForeignKey(c => c.SubsidiaryTypeId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(c => c.Company).WithMany().HasForeignKey(c => c.CompanyId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(c => c.CamoDoctor).WithMany().HasForeignKey(c => c.CamoDoctorId).OnDelete(DeleteBehavior.Restrict);

        }
    }
}

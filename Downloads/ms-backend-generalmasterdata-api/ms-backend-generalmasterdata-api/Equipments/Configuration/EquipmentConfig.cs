using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Equipments.Domain.Entities;

namespace AnaPrevention.GeneralMasterData.Api.Equipments.Configuration
{
    public class EquipmentConfig : IEntityTypeConfiguration<Equipment>
    {
        public void Configure(EntityTypeBuilder<Equipment> builder)
        {
            builder.ToTable("equipments").HasKey(k => k.Id);
            builder.Property(p => p.Code).HasMaxLength(CommonStatic.CodeMaxLength).IsRequired().IsUnicode(false);
            builder.Property(p => p.Description).HasMaxLength(CommonStatic.DescriptionMaxLength).IsRequired().IsUnicode(false);
            builder.Property(p => p.Supplier).HasMaxLength(CommonStatic.DescriptionMaxLength).IsRequired().IsUnicode(false); ;
            builder.Property(p => p.Brand).HasMaxLength(CommonStatic.DescriptionMaxLength).IsRequired().IsUnicode(false);
            builder.Property(p => p.Model).HasMaxLength(CommonStatic.DescriptionMaxLength).IsRequired().IsUnicode(false);
            builder.Property(p => p.SerialNumber).HasMaxLength(CommonStatic.DescriptionMaxLength).IsRequired().IsUnicode(false);
            builder.Property(p => p.MedicalAreaId).IsRequired();
            builder.Property(p => p.CompanyId).IsRequired();
            builder.Property(p => p.PersonDeviceManagerId).IsRequired();
            builder.Property(p => p.SubsidiaryId).IsRequired();
            builder.Property(p => p.Status).IsRequired();
            builder.HasOne(c => c.MedicalArea).WithMany().HasForeignKey(c => c.MedicalAreaId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(c => c.Company).WithMany().HasForeignKey(c => c.CompanyId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(c => c.Person).WithMany().HasForeignKey(c => c.PersonDeviceManagerId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(c => c.Subsidiary).WithMany().HasForeignKey(c => c.SubsidiaryId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}

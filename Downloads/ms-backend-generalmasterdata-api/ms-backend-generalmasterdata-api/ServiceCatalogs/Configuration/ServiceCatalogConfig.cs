using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.ServiceCatalogs.Domain.Entities;

namespace AnaPrevention.GeneralMasterData.Api.ServiceCatalogs.Configuration
{
    public class ServiceCatalogConfig : IEntityTypeConfiguration<ServiceCatalog>
    {
        public void Configure(EntityTypeBuilder<ServiceCatalog> builder)
        {
            builder.ToTable("serviceCatalogs").HasKey(k => k.Id);
            builder.Property(p => p.Description).HasMaxLength(CommonStatic.DescriptionMaxLength).IsRequired().IsUnicode(false);
            builder.Property(p => p.Code).HasMaxLength(CommonStatic.CodeMaxLength).IsRequired().IsUnicode(false);
            builder.Property(p => p.CodeSecond).HasMaxLength(CommonStatic.CodeMaxLength).IsRequired().IsUnicode(false);
            builder.Property(p => p.OrderRow).IsRequired(true).HasDefaultValue(CommonStatic.DefaultOrderRow);
            builder.Property(p => p.OrderRowTourSheet).IsRequired(true).HasDefaultValue(CommonStatic.DefaultOrderRow);
            builder.Property(p => p.SubFamilyId).IsRequired();
            builder.Property(p => p.UomId).IsRequired();
            builder.Property(p => p.UomSecondId).IsRequired();
            builder.Property(p => p.ExistenceTypeId).IsRequired();
            builder.Property(p => p.TaxId).IsRequired();
            builder.Property(p => p.IsActive).IsRequired();
            builder.Property(p => p.IsSales).IsRequired();
            builder.Property(p => p.IsBuy).IsRequired();
            builder.Property(p => p.IsInventory).IsRequired();
            builder.Property(p => p.IsRetention).IsRequired();
            builder.Property(p => p.OrderRowLaboratory).IsRequired();
            builder.Property(p => p.Comment).HasMaxLength(CommonStatic.DescriptionMaxLength).IsRequired().IsUnicode(false);
            builder.Property(p => p.Status).IsRequired();
            builder.Property(p => p.MedicalAreaId).IsRequired(false);
            builder.HasOne(c => c.SubFamily).WithMany().HasForeignKey(c => c.SubFamilyId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(c => c.Uom).WithMany().HasForeignKey(c => c.UomId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(c => c.UomSecond).WithMany().HasForeignKey(c => c.UomSecondId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(c => c.ExistenceType).WithMany().HasForeignKey(c => c.ExistenceTypeId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(c => c.Tax).WithMany().HasForeignKey(c => c.TaxId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(c => c.MedicalArea).WithMany().HasForeignKey(c => c.MedicalAreaId).OnDelete(DeleteBehavior.Restrict);

        }
    }
}

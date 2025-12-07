using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AnaPrevention.GeneralMasterData.Api.ServiceCatalogs.Domain.Entities;

namespace AnaPrevention.GeneralMasterData.Api.ServiceCatalogs.Configuration
{
    public class ServiceCatalogFieldConfig : IEntityTypeConfiguration<ServiceCatalogField>
    {
        public void Configure(EntityTypeBuilder<ServiceCatalogField> builder)
        {
            builder.ToTable("serviceCatalogFields").HasKey(k => k.Id);
            builder.Property(p => p.FieldId).IsRequired();
            builder.Property(p => p.ServiceCatalogId).IsRequired();
            builder.Property(p => p.OrderRow).IsRequired();
            builder.HasOne(c => c.Field).WithMany().HasForeignKey(c => c.FieldId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(c => c.ServiceCatalog).WithMany().HasForeignKey(c => c.ServiceCatalogId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}

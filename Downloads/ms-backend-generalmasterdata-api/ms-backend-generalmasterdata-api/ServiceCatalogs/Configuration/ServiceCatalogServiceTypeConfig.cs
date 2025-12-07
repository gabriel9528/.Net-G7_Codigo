using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using AnaPrevention.GeneralMasterData.Api.ServiceTypes.Domain.Entities;

namespace AnaPrevention.GeneralMasterData.Api.ServiceCatalogs.Configuration
{
    public class ServiceCatalogServiceTypeConfig : IEntityTypeConfiguration<ServiceCatalogServiceType>
    {
        public void Configure(EntityTypeBuilder<ServiceCatalogServiceType> builder)
        {
            builder.ToTable("serviceCatalogServiceTypes").HasKey(k => k.Id);
            builder.Property(p => p.ServiceTypeId).IsRequired();
            builder.Property(p => p.ServiceCatalogId).IsRequired();
            builder.HasOne(c => c.ServiceType).WithMany().HasForeignKey(c => c.ServiceTypeId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(c => c.ServiceCatalog).WithMany().HasForeignKey(c => c.ServiceCatalogId).OnDelete(DeleteBehavior.Restrict);

        }
    }
}

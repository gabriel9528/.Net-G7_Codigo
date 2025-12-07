using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AnaPrevention.GeneralMasterData.Api.ServiceCatalogs.Domain.Entities;

namespace AnaPrevention.GeneralMasterData.Api.ServiceCatalogs.Configuration
{
    public class ServiceCatalogMedicalFormConfig : IEntityTypeConfiguration<ServiceCatalogMedicalForm>
    {
        public void Configure(EntityTypeBuilder<ServiceCatalogMedicalForm> builder)
        {
            builder.ToTable("serviceCatalogMedicalForms").HasKey(k => k.Id);
            builder.Property(p => p.ServiceCatalogId).IsRequired();
            builder.Property(p => p.MedicalFormId).IsRequired();
            builder.HasOne(c => c.ServiceCatalog).WithMany().HasForeignKey(c => c.ServiceCatalogId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(c => c.MedicalForm).WithMany().HasForeignKey(c => c.MedicalFormId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}

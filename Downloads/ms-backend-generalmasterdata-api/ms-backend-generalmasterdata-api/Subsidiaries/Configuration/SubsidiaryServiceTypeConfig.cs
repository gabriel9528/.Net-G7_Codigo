using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AnaPrevention.GeneralMasterData.Api.Subsidiaries.Domain.Entities;

namespace AnaPrevention.GeneralMasterData.Api.Subsidiaries.Configuration
{
    public class SubsidiaryServiceTypeConfig : IEntityTypeConfiguration<SubsidiaryServiceType>
    {
        public void Configure(EntityTypeBuilder<SubsidiaryServiceType> builder)
        {
            builder.ToTable("subsidiaryServiceTypes").HasKey(k => k.Id);
            builder.Property(p => p.SubsidiaryId).IsRequired();
            builder.Property(p => p.ServiceTypeId).IsRequired();
            builder.HasOne(c => c.Subsidiary).WithMany().HasForeignKey(c => c.SubsidiaryId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(c => c.ServiceType).WithMany().HasForeignKey(c => c.ServiceTypeId).OnDelete(DeleteBehavior.Restrict);
            builder.HasIndex(t1 => new { t1.SubsidiaryId, t1.ServiceTypeId }).IsUnique();
        }
    }
}

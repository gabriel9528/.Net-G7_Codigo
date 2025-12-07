using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AnaPrevention.GeneralMasterData.Api.Businesses.Domain.Entities;

namespace AnaPrevention.GeneralMasterData.Api.Businesses.Configuration
{
    public class BusinessEconomicActivityConfig : IEntityTypeConfiguration<BusinessEconomicActivity>
    {
        public void Configure(EntityTypeBuilder<BusinessEconomicActivity> builder)
        {
            builder.ToTable("businessEconomicActivities").HasKey(k => k.Id);
            builder.Property(p => p.BusinessId).IsRequired();
            builder.Property(p => p.EconomicActivityId).IsRequired();
            builder.HasIndex(t1 => new { t1.EconomicActivityId, t1.BusinessId }).IsUnique();
            builder.HasOne(c => c.Business).WithMany().HasForeignKey(c => c.BusinessId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(c => c.EconomicActivity).WithMany().HasForeignKey(c => c.EconomicActivityId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
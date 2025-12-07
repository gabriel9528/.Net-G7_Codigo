using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AnaPrevention.GeneralMasterData.Api.EconomicActivities.Domain.Entities;

namespace AnaPrevention.GeneralMasterData.Api.EconomicActivities.Configuration
{
    public class EconomicActivityConfig : IEntityTypeConfiguration<EconomicActivity>
    {
        public void Configure(EntityTypeBuilder<EconomicActivity> builder)
        {
            builder.ToTable("economicActivities").HasKey(k => k.Id);
            builder.Property(t1 => t1.Description).HasMaxLength(200).IsRequired().IsUnicode(false);
            builder.Property(t1 => t1.Code).HasMaxLength(20).IsRequired().IsUnicode(false);
            builder.HasIndex(t1 => t1.Code).IsUnique();
            builder.HasIndex(t1 => t1.Description).IsUnique();
           

        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Dimensions.Domain.Entities;

namespace AnaPrevention.GeneralMasterData.Api.Dimensions.Configuration
{
    public class CostCenterConfig : IEntityTypeConfiguration<CostCenter>
    {
        public void Configure(EntityTypeBuilder<CostCenter> builder)
        {
            builder.ToTable("costCenters").HasKey(k => k.Id);
            builder.Property(p => p.Code).HasMaxLength(CommonStatic.CodeMaxLength).IsRequired().IsUnicode(false);
            builder.Property(p => p.Description).HasMaxLength(CommonStatic.DescriptionMaxLength).IsRequired().IsUnicode(false);
            builder.Property(p => p.Status).IsRequired();
            builder.Property(p => p.DimensionId).IsRequired();
            builder.HasOne(c => c.Dimension).WithMany().HasForeignKey(c => c.DimensionId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}

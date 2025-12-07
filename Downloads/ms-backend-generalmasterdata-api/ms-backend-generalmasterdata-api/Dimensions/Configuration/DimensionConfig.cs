using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Dimensions.Domain.Entities;

namespace AnaPrevention.GeneralMasterData.Api.Dimensions.Configuration
{
    public class DimensionConfig : IEntityTypeConfiguration<Dimension>
    {
        public void Configure(EntityTypeBuilder<Dimension> builder)
        {
            builder.ToTable("dimensions").HasKey(k => k.Id);
            builder.Property(p => p.Code).HasMaxLength(CommonStatic.CodeMaxLength).IsRequired().IsUnicode(false);
            builder.Property(p => p.Description).HasMaxLength(CommonStatic.DescriptionMaxLength).IsRequired().IsUnicode(false);
            builder.Property(p => p.Status).IsRequired();
            builder.Property(p => p.CompanyId).IsRequired();
            builder.HasMany<CostCenter>().WithOne(p => p.Dimension).HasForeignKey(p => p.DimensionId);
        }
    }
}

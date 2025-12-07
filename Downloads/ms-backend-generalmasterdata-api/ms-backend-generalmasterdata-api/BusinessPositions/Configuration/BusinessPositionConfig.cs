using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AnaPrevention.GeneralMasterData.Api.BusinessPositions.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;

namespace AnaPrevention.GeneralMasterData.Api.BusinessPositions.Configuration
{
    public class BusinessPositionConfig : IEntityTypeConfiguration<BusinessPosition>
    {
        public void Configure(EntityTypeBuilder<BusinessPosition> builder)
        {
            builder.ToTable("businessPositions").HasKey(k => k.Id);
            builder.Property(p => p.Description).HasMaxLength(CommonStatic.DescriptionMaxLength).IsRequired().IsUnicode(false);
            builder.Property(p => p.BusinessAreaId).IsRequired();
            builder.Property(p => p.Status).IsRequired();
            builder.HasOne(c => c.BusinessArea).WithMany().HasForeignKey(c => c.BusinessAreaId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}

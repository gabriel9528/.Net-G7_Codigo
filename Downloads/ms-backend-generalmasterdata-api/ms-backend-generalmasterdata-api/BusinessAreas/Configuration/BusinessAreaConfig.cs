using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AnaPrevention.GeneralMasterData.Api.BusinessAreas.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;

namespace AnaPrevention.GeneralMasterData.Api.BusinessAreas.Configuration
{
    public class BusinessAreaConfig : IEntityTypeConfiguration<BusinessArea>
    {
        public void Configure(EntityTypeBuilder<BusinessArea> builder)
        {
            builder.ToTable("businessAreas").HasKey(k => k.Id);
            builder.Property(p => p.Description).HasMaxLength(CommonStatic.DescriptionMaxLength).IsRequired().IsUnicode(false);
            builder.Property(p => p.BusinessId).IsRequired();
            builder.Property(p => p.Status).IsRequired();
            builder.HasOne(c => c.Business).WithMany().HasForeignKey(c => c.BusinessId).OnDelete(DeleteBehavior.Restrict);
            builder.HasIndex(p => new { p.Description, p.BusinessId }).IsUnique();
        }
    }
}

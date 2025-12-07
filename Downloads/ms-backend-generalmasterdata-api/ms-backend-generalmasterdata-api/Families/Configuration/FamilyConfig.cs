using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Families.Domain.Entities;

namespace AnaPrevention.GeneralMasterData.Api.Families.Configuration
{
    public class FamilyConfig : IEntityTypeConfiguration<Family>
    {
        public void Configure(EntityTypeBuilder<Family> builder)
        {
            builder.ToTable("families").HasKey(k => k.Id);
            builder.Property(p => p.Code).HasMaxLength(CommonStatic.CodeMaxLength).IsRequired().IsUnicode(false);
            builder.Property(p => p.Description).HasMaxLength(CommonStatic.DescriptionMaxLength).IsRequired().IsUnicode(false);
            builder.Property(p => p.Status).IsRequired();
            builder.Property(p => p.OrderRow).IsRequired(true).HasDefaultValue(CommonStatic.DefaultOrderRow);
            builder.Property(p => p.CompanyId).IsRequired();
            builder.Property(p => p.LineId).IsRequired();
            builder.HasOne(c => c.Line).WithMany().HasForeignKey(c => c.LineId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(c => c.Company).WithMany().HasForeignKey(c => c.CompanyId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Families.Domain.Entities;

namespace AnaPrevention.GeneralMasterData.Api.Lines.Configuration
{

    internal class LineConfig : IEntityTypeConfiguration<Line>
    {
        public void Configure(EntityTypeBuilder<Line> builder)
        {
            builder.ToTable("lines").HasKey(k => k.Id);
            builder.Property(p => p.Code).HasMaxLength(CommonStatic.CodeMaxLength).IsRequired().IsUnicode(false);
            builder.Property(p => p.Description).HasMaxLength(CommonStatic.DescriptionMaxLength).IsRequired().IsUnicode(false);
            builder.Property(p => p.OrderRow).IsRequired(true).HasDefaultValue(CommonStatic.DefaultOrderRow);
            builder.HasIndex(p => p.Description).IsUnique();
            builder.HasOne(p => p.Company).WithMany().HasForeignKey(p => p.CompanyId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(c => c.LineType).WithMany().HasForeignKey(c => c.LineTypeId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}


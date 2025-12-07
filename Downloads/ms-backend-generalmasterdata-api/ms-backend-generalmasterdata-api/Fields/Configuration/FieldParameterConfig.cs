using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Fields.Domain.Entities;

namespace AnaPrevention.GeneralMasterData.Api.Fields.Configuration
{
    public class FieldParameterConfig : IEntityTypeConfiguration<FieldParameter>
    {
        public void Configure(EntityTypeBuilder<FieldParameter> builder)
        {
            builder.ToTable("fieldParameters").HasKey(k => k.Id);

            builder.Property(t1 => t1.FieldId).IsRequired();
            builder.Property(t1 => t1.GenderId).IsRequired(false);
            builder.Property(t1 => t1.DefaultValue).HasMaxLength(CommonStatic.DescriptionMaxLength).IsRequired().IsUnicode(false);
            builder.Property(t1 => t1.Legend).HasMaxLength(CommonStatic.DescriptionMaxLength).IsRequired(false).IsUnicode(false);
            builder.Property(t1 => t1.RangeJson).IsRequired().IsUnicode(false);
            builder.Property(t1 => t1.Uom).HasMaxLength(20).IsRequired().IsUnicode(false);
            builder.Property(t1 => t1.OptionsJson).IsRequired(false).IsUnicode(false);
            builder.Property(t1 => t1.IsMandatory).IsRequired();
            builder.Property(t1 => t1.Show).IsRequired();
            builder.Property(t1 => t1.Status).IsRequired();
            builder.HasOne(c => c.Gender).WithMany().HasForeignKey(c => c.GenderId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(c => c.Field).WithMany().HasForeignKey(c => c.FieldId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Fields.Domain.Entities;

namespace AnaPrevention.GeneralMasterData.Api.Fields.Configuration
{
    public class FieldConfig : IEntityTypeConfiguration<Field>
    {
        public void Configure(EntityTypeBuilder<Field> builder)
        {
            builder.ToTable("Fields").HasKey(k => k.Id);
            builder.Property(p => p.Description).HasMaxLength(CommonStatic.DescriptionMaxLength).IsRequired().IsUnicode(false);
            builder.Property(p => p.Code).HasMaxLength(CommonStatic.DescriptionMaxLength).IsRequired().IsUnicode(false);
            builder.Property(p => p.SecondCode).HasMaxLength(CommonStatic.CodeMaxLength).IsRequired(false).IsUnicode(false);
            builder.Property(p => p.Name).HasMaxLength(CommonStatic.DescriptionMaxLength).IsRequired().IsUnicode(false);
            builder.Property(p => p.Uom).HasMaxLength(CommonStatic.DescriptionMaxLength).IsRequired().IsUnicode(false);
            builder.Property(p => p.Legend).IsRequired(false).IsUnicode(false);
            builder.Property(p => p.OptionsJson).IsRequired(false).IsUnicode();
            builder.Property(p => p.FieldType).IsRequired();
            builder.Property(p => p.MedicalFormSubType).IsRequired();
            builder.Property(p => p.MedicalFormId).IsRequired(false);
            builder.Property(p => p.Status).IsRequired();
            builder.Property(p => p.CreateType).IsRequired();
            builder.Property(p => p.OrderRow).IsRequired();
            builder.Property(p => p.FieldExamenType).IsRequired();
            builder.Property(p => p.IsforAllFormats).IsRequired();
            builder.Property(p => p.ReferenceValuesJson).IsRequired(false).IsUnicode();
            builder.HasOne(c => c.MedicalForm).WithMany().HasForeignKey(c => c.MedicalFormId).OnDelete(DeleteBehavior.Restrict);


        }
    }
}

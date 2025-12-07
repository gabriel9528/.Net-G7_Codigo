using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AnaPrevention.GeneralMasterData.Api.Fields.Domain.Entities;

namespace AnaPrevention.GeneralMasterData.Api.Fields.Configuration
{
    public class FieldMedicalFormatConfig : IEntityTypeConfiguration<FieldMedicalFormat>
    {
        public void Configure(EntityTypeBuilder<FieldMedicalFormat> builder)
        {
            builder.ToTable("fieldMedicalFormats").HasKey(k => new { k.MedicalFormatId, k.FieldId });
            builder.Property(t1 => t1.MedicalFormatId).IsRequired();
            builder.Property(t1 => t1.FieldId).IsRequired();

            builder.HasOne(t1 => t1.MedicalFormat).WithMany().HasForeignKey(c => c.MedicalFormatId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(t1 => t1.Field).WithMany().HasForeignKey(c => c.FieldId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
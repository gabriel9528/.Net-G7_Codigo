using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.ServiceCatalogs.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.ServiceCatalogs.Domain.Enum;

namespace AnaPrevention.GeneralMasterData.Api.ServiceCatalogs.Configuration
{
    public class MedicalFormatConfig : IEntityTypeConfiguration<MedicalFormat>
    {
        public void Configure(EntityTypeBuilder<MedicalFormat> builder)
        {
            builder.ToTable("medicalFormats").HasKey(k => k.Id);
            builder.Property(p => p.Description).HasMaxLength(CommonStatic.DescriptionMaxLength).IsRequired().IsUnicode(false);
            builder.Property(p => p.Code).HasMaxLength(CommonStatic.CodeMaxLength).IsRequired().IsUnicode(false);
            builder.Property(p => p.MedicalFormatType).IsRequired();
            builder.Property(p => p.Status).IsRequired();
            builder.HasIndex(p => p.Code).IsUnique();
            builder.HasIndex(p => p.Description).IsUnique();


        }
    }
}

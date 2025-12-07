using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.MedicalAreas.Domain.Entities;

namespace AnaPrevention.GeneralMasterData.Api.MedicalAreas.Configuration
{
    public class MedicalAreaConfig : IEntityTypeConfiguration<MedicalArea>
    {
        public void Configure(EntityTypeBuilder<MedicalArea> builder)
        {
            builder.ToTable("medicalAreas").HasKey(k => k.Id);
            builder.Property(p => p.Description).HasMaxLength(CommonStatic.DescriptionMaxLength).IsRequired();
            builder.Property(p => p.Code).HasMaxLength(CommonStatic.CodeMaxLength).IsRequired();
            builder.Property(p => p.OrderRowTourSheet).IsRequired(true).HasDefaultValue(CommonStatic.DefaultOrderRow);
            builder.Property(p => p.Icon).IsRequired(false).IsUnicode(false);
            builder.Property(p => p.OrderRowAttention).IsRequired();
            builder.Property(p => p.MedicalAreaType).IsRequired();
            builder.Property(p => p.Status).IsRequired();


        }
    }
}

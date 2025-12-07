using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.ServiceCatalogs.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.MedicalForms.Domain.Entities;

namespace AnaPrevention.GeneralMasterData.Api.ServiceCatalogs.Configuration
{
    public class MedicalFormConfig : IEntityTypeConfiguration<MedicalForm>
    {
        public void Configure(EntityTypeBuilder<MedicalForm> builder)
        {
            builder.ToTable("medicalForms").HasKey(k => k.Id);
            builder.Property(p => p.Description).HasMaxLength(CommonStatic.DescriptionMaxLength).IsRequired().IsUnicode(false);
            builder.Property(p => p.IconDescription).HasMaxLength(CommonStatic.DescriptionMaxLength).IsRequired(false).IsUnicode(false);
            builder.Property(p => p.Abbreviation).HasMaxLength(5).IsRequired(false).IsUnicode(false);
            builder.Property(p => p.Status).IsRequired();
            builder.Property(p => p.MedicalAreaId).IsRequired();
            builder.Property(p => p.ServiceTypeId).IsRequired();
            builder.Property(p => p.MedicalFormsType).IsRequired();
            builder.Property(p => p.DescriptionInDiagnostic).IsRequired(false).HasMaxLength(CommonStatic.DescriptionMaxLength);
            builder.Property(p => p.OptionDiagnosticJson).IsRequired(false).IsUnicode(false);
            builder.Property(p => p.ShowInDiagnostic).IsRequired();
            builder.Property(p => p.OrderRowAttention).IsRequired();
            builder.Property(p => p.OrderRowDiagnostic).IsRequired();
            builder.Property(p => p.IsMedicalExam).IsRequired().HasDefaultValue(true);
            builder.Property(p => p.Icon).IsRequired(false).IsUnicode(false);
            builder.Property(p => p.DiagnosticForm).IsRequired().HasDefaultValue(DiagnosticForm.WITH_CIE10);
            builder.HasOne(c => c.MedicalArea).WithMany().HasForeignKey(c => c.MedicalAreaId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(c => c.ServiceType).WithMany().HasForeignKey(c => c.ServiceTypeId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}

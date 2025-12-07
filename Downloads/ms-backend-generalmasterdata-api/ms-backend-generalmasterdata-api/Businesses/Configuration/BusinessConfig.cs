using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AnaPrevention.GeneralMasterData.Api.Businesses.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;

namespace AnaPrevention.GeneralMasterData.Api.Businesses.Configuration
{
    public class BusinessConfig : IEntityTypeConfiguration<Business>
    {
        public void Configure(EntityTypeBuilder<Business> builder)
        {
            builder.ToTable("businesses").HasKey(k => k.Id);
            builder.Property(t1 => t1.Description).HasMaxLength(CommonStatic.DescriptionMaxLength).IsUnicode(false).IsRequired();
            builder.Property(t1 => t1.Tradename).HasMaxLength(CommonStatic.DescriptionMaxLength).IsUnicode(false).IsRequired();
            builder.Property(t1 => t1.Address).HasMaxLength(CommonStatic.DescriptionMaxLength).IsUnicode(false).IsRequired();
            builder.Property(t1 => t1.IdentityDocumentTypeId).IsRequired();
            builder.Property(t1 => t1.MedicalFormatId).IsRequired();
            builder.Property(t1 => t1.CreditTimeId).IsRequired();
            builder.Property(t1 => t1.DistrictId).HasMaxLength(CommonStatic.CodeMaxLength).IsRequired().IsUnicode(false);
            builder.Property(t1 => t1.DocumentNumber).IsRequired();
            builder.Property(t1 => t1.Comment).IsRequired().IsUnicode(false);
            builder.Property(t1 => t1.DateInscription).IsRequired();
            builder.Property(t1 => t1.IsActive).IsRequired();
            builder.Property(t1 => t1.IsPatientResults).IsRequired();
            builder.Property(t1 => t1.IsGenerateUsers).IsRequired();
            builder.Property(t1 => t1.IsMedicalReportDisplay).IsRequired();
            builder.Property(t1 => t1.IsSendingResultsPatients).IsRequired();
            builder.Property(t1 => t1.IsWaybillShipping).IsRequired();
            builder.Property(t1 => t1.Status).IsRequired();
            builder.Property(t1 => t1.ExceptionsByMainBusinessJson).IsRequired(false);
            builder.HasIndex(t1 => new { t1.DocumentNumber, t1.IdentityDocumentTypeId }).IsUnique();
            builder.HasIndex(t1 => t1.Description).IsUnique();
            builder.HasOne(c => c.CreditTime).WithMany().HasForeignKey(c => c.CreditTimeId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(c => c.IdentityDocumentType).WithMany().HasForeignKey(c => c.IdentityDocumentTypeId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(c => c.District).WithMany().HasForeignKey(c => c.DistrictId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(c => c.MedicalFormat).WithMany().HasForeignKey(c => c.MedicalFormatId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}

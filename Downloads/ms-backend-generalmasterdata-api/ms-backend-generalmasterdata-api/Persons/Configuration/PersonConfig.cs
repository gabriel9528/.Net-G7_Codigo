using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Common.Domain.ValueObjects;
using AnaPrevention.GeneralMasterData.Api.Persons.Domain.Entities;

namespace AnaPrevention.GeneralMasterData.Api.Persons.Configuration
{
    internal class PersonConfig : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {

            builder.ToTable("persons").HasKey(k => k.Id);
            builder.Property(t1 => t1.DocumentNumber).HasMaxLength(50).IsRequired().IsUnicode(false);
            builder.Property(t1 => t1.IdentityDocumentTypeId).IsRequired();
            builder.Property(t1 => t1.Names).HasMaxLength(CommonStatic.DescriptionMaxLength).IsRequired().IsUnicode(false);
            builder.Property(t1 => t1.LastName).HasMaxLength(CommonStatic.DescriptionMaxLength).IsRequired().IsUnicode(false);
            builder.Property(t1 => t1.SecondLastName).HasMaxLength(CommonStatic.DescriptionMaxLength).IsRequired(false).IsUnicode(false);
            builder.Property(t1 => t1.PhoneNumber).HasMaxLength(CommonStatic.DescriptionMaxLength).IsRequired(false).IsUnicode(false);
            builder.Property(t1 => t1.Email).HasMaxLength(CommonStatic.DescriptionMaxLength).IsRequired(false).IsUnicode(false).HasConversion(p => p.Value, p => Email.Create(p).Value);
            builder.Property(t1 => t1.GenderId).IsRequired(false);
            builder.Property(t1 => t1.DateBirth).IsRequired(false).HasConversion(CommonStatic.ConvertDate);
            builder.Property(t1 => t1.PersonalEmail).HasMaxLength(CommonStatic.DescriptionMaxLength).IsRequired(false).IsUnicode(false).HasConversion(p => p.Value, p => Email.Create(p).Value);
            builder.Property(t1 => t1.PersonalPhoneNumber).HasMaxLength(30).IsRequired(false).IsUnicode();
            builder.Property(t1 => t1.SecondDocumentNumber).HasMaxLength(50).IsRequired(false).IsUnicode(false);
            builder.Property(t1 => t1.SecondIdentityDocumentTypeId).IsRequired(false);
            builder.Property(t1 => t1.PersonalAddress).HasMaxLength(CommonStatic.DescriptionMaxLength).IsRequired(false).IsUnicode(false);
            builder.Property(t1 => t1.EmergencyContactName).HasMaxLength(CommonStatic.DescriptionMaxLength).IsRequired(false).IsUnicode(false);
            builder.Property(t1 => t1.EmergencyContactNumberPhone).HasMaxLength(CommonStatic.DescriptionMaxLength).IsRequired(false).IsUnicode(false);
            builder.Property(t1 => t1.EmergencyContactRelationship).HasMaxLength(CommonStatic.DescriptionMaxLength).IsRequired(false).IsUnicode(false);
            builder.Property(t1 => t1.Photo).IsRequired(false).IsUnicode(false);
            builder.Property(t1 => t1.DegreeInstructionId).IsRequired(false);
            builder.Property(t1 => t1.MaritalStatusId).IsRequired(false);
            builder.Property(t1 => t1.HealthInsuranceJson).IsRequired(false);
            builder.Property(t1 => t1.CountryResidenceId).HasMaxLength(CommonStatic.CodeMaxLength).IsRequired(false).IsUnicode(false);
            builder.Property(t1 => t1.CountryBirthId).HasMaxLength(CommonStatic.CodeMaxLength).IsRequired(false).IsUnicode(false);
            builder.Property(t1 => t1.DistrictResidenceId).HasMaxLength(CommonStatic.CodeMaxLength).IsRequired(false).IsUnicode(false);
            builder.Property(t1 => t1.DistrictBirthId).HasMaxLength(CommonStatic.CodeMaxLength).IsRequired(false).IsUnicode(false);
            builder.Property(p => p.ResetPassword).IsRequired();
            builder.Property(p => p.DataProcessingAuthorization).IsRequired().HasDefaultValue(false);

            builder.HasOne(t1 => t1.IdentityDocumentType).WithMany().HasForeignKey(c => c.IdentityDocumentTypeId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(t1 => t1.SecondIdentityDocumentType).WithMany().HasForeignKey(c => c.SecondIdentityDocumentTypeId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(t1 => t1.Gender).WithMany().HasForeignKey(c => c.GenderId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(t1 => t1.DegreeInstruction).WithMany().HasForeignKey(c => c.DegreeInstructionId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(t1 => t1.MaritalStatus).WithMany().HasForeignKey(c => c.MaritalStatusId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(t1 => t1.CountryResidence).WithMany().HasForeignKey(c => c.CountryResidenceId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(t1 => t1.CountryBirth).WithMany().HasForeignKey(c => c.CountryBirthId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(t1 => t1.DistrictResidence).WithMany().HasForeignKey(c => c.DistrictResidenceId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(t1 => t1.DistrictBirth).WithMany().HasForeignKey(c => c.DistrictBirthId).OnDelete(DeleteBehavior.Restrict);
            builder.HasIndex(t1 => new { t1.DocumentNumber, t1.IdentityDocumentTypeId }).IsUnique();
            //builder.HasData(new Person("99999999",Guid.Parse("C0644A1C-CA2B-4DDA-939B-342B4A45B9A0"),"Admin","Admin","","",null,Guid.Parse("219F04D3-C5A0-4958-9544-618B2EA56610")));
        }
    }
}

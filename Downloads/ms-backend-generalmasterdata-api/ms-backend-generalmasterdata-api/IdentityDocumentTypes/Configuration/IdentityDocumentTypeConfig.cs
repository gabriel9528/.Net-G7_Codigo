using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.IdentityDocumentTypes.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.IdentityDocumentTypes.Domain.Enums;


namespace AnaPrevention.GeneralMasterData.Api.IdentityDocumentTypes.Configuration
{
    public class IdentityDocumentTypeConfig : IEntityTypeConfiguration<IdentityDocumentType>
    {
        public void Configure(EntityTypeBuilder<IdentityDocumentType> builder)
        {
            builder.ToTable("identityDocumentTypes").HasKey(k => k.Id);
            builder.Property(p => p.Description).HasMaxLength(CommonStatic.DescriptionMaxLength).IsRequired().IsUnicode(false);
            builder.Property(p => p.Code).HasMaxLength(CommonStatic.CodeMaxLength).IsRequired().IsUnicode(false);
            builder.Property(p => p.Abbreviation).HasMaxLength(5).IsRequired().IsUnicode(false);
            builder.Property(p => p.Length).IsRequired();
            builder.Property(p => p.TaxpayerType).IsRequired();
            builder.Property(p => p.PersonType).IsRequired().HasDefaultValue(PersonType.BOTH);
            builder.Property(p => p.IndicatorLength).IsRequired();
            builder.Property(p => p.InputType).IsRequired();
            builder.Property(p => p.Status).IsRequired();
            builder.HasIndex(p => p.Description).IsUnique();
            builder.HasIndex(p => p.Code).IsUnique();
           // builder.HasMany<Person>().WithOne(p => p.IdentityDocumentType).HasForeignKey(p => p.IdentityDocumentTypeId);
        }
    }
}

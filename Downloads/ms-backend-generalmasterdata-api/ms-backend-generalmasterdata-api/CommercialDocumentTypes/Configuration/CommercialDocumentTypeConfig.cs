using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AnaPrevention.GeneralMasterData.Api.CommercialDocumentTypes.Application.Entities;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;

namespace AnaPrevention.GeneralMasterData.Api.CommercialDocumentTypes.Configuration
{
    public class CommercialDocumentTypeConfig : IEntityTypeConfiguration<CommercialDocumentType>
    {
        public void Configure(EntityTypeBuilder<CommercialDocumentType> builder)
        {
            builder.ToTable("commercialDocumentTypes").HasKey(k => k.Id);
            builder.Property(p => p.Description).HasMaxLength(CommonStatic.DescriptionMaxLength).IsRequired().IsUnicode(false);
            builder.Property(p => p.Code).HasMaxLength(CommonStatic.CodeMaxLength).IsRequired().IsUnicode(false);
            builder.Property(p => p.Abbreviation).HasMaxLength(3).IsRequired().IsUnicode(false);
            builder.Property(p => p.SalesDocument).IsRequired();
            builder.Property(p => p.PurchaseDocument).IsRequired();
            builder.Property(p => p.GetSetDocument).IsRequired();
            builder.Property(p => p.Status).IsRequired();
           
        }
    }
}

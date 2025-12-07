using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Emails.EmailTags.Domain.Entities;

namespace AnaPrevention.GeneralMasterData.Api.Emails.EmailTags.Configuration
{
    public class EmailTagConfig : IEntityTypeConfiguration<EmailTag>
    {
        public void Configure(EntityTypeBuilder<EmailTag> builder)
        {
            builder.ToTable("EmailTags").HasKey(k => k.Id);
            builder.Property(p => p.Description).HasMaxLength(CommonStatic.DescriptionMaxLength).IsRequired().IsUnicode(false).IsRequired();
            builder.Property(p => p.Tag).HasMaxLength(CommonStatic.DescriptionMaxLength).IsRequired().IsUnicode(false).IsRequired();
            builder.Property(p => p.Status).IsRequired();
        }
    }
}
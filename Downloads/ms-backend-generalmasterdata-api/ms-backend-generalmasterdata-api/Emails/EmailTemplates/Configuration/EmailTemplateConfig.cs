using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Emails.EmailTemplates.Domain.Entities;

namespace AnaPrevention.GeneralMasterData.Api.Emails.EmailTemplates.Configuration
{
    public class EmailTemplateConfig : IEntityTypeConfiguration<EmailTemplate>
    {
        public void Configure(EntityTypeBuilder<EmailTemplate> builder)
        {
            builder.ToTable("emailTemplates").HasKey(k => k.Id);
            builder.Property(p => p.Body).IsRequired().IsUnicode(false);
            builder.Property(p => p.Description).HasMaxLength(CommonStatic.DescriptionMaxLength).IsRequired().IsUnicode(false);
            builder.Property(p => p.Subject).HasMaxLength(CommonStatic.DescriptionMaxLength).IsRequired().IsUnicode(false); ;
            builder.Property(p => p.EmailUserId).IsRequired();
            builder.Property(p => p.IsDefault).IsRequired();
            builder.Property(p => p.Status).IsRequired().HasDefaultValue(true);

            builder.HasOne(p => p.EmailUser).WithMany().HasForeignKey(p => p.EmailUserId).OnDelete(DeleteBehavior.Restrict);

        }
    }
}
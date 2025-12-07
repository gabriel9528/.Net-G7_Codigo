using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Emails.EmailContents.Domain.Entitites;

namespace AnaPrevention.GeneralMasterData.Api.Emails.EmailContents.Configuration
{
    public class EmailContentConfig : IEntityTypeConfiguration<EmailContent>
    {
        public void Configure(EntityTypeBuilder<EmailContent> builder)
        {
            builder.ToTable("emailContent").HasKey(x => x.Id);
            builder.Property(x => x.Status).IsRequired(true);
            builder.Property(x => x.Body).IsRequired(false);
            builder.Property(x => x.FromAddress).IsRequired(true);
            builder.Property(x => x.ToAddress).IsRequired(true);
            builder.Property(x => x.DateSend).IsRequired(true);
            builder.Property(x => x.AttachmentUrls).IsRequired(false);
            builder.Property(x => x.EmailTemplateId).IsRequired(false);
            builder.Property(x => x.ToPersonId).IsRequired(false);
            builder.Property(x => x.ReferenceId).IsRequired(false);
            builder.Property(x => x.Result).IsRequired(false);
            builder.Property(x => x.Subject).IsRequired(false).HasMaxLength(CommonStatic.DescriptionMaxLength);
            //builder.HasOne(x => x.UserEmail).WithMany().HasForeignKey(x => x.UserId);
            builder.HasOne(x => x.EmailTemplate).WithMany().HasForeignKey(x => x.EmailTemplateId);
            builder.HasOne(x => x.ToPerson).WithMany().HasForeignKey(x => x.ToPersonId);
        }
    }
}

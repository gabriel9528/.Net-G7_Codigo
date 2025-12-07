using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Emails.EmailTags.Domain.Entities;

namespace AnaPrevention.GeneralMasterData.Api.Emails.EmailTags.Configuration
{
    public class AuditEmailTagConfig : IEntityTypeConfiguration<AuditEmailTag>
    {
        public void Configure(EntityTypeBuilder<AuditEmailTag> builder)
        {
            builder.ToTable("occupationalAuditEmailTag").HasKey(k => k.Id);
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Datetime).IsRequired();
            builder.Property(e => e.AuditType).HasMaxLength(CommonStatic.DescriptionMaxLength).IsRequired().IsUnicode(false);
            builder.Property(e => e.UserId).IsRequired();
            builder.Property(e => e.TableName).HasMaxLength(CommonStatic.DescriptionMaxLength).IsRequired().IsUnicode(false);
            builder.Property(e => e.KeyValues).IsRequired(false).IsUnicode(false);
            builder.Property(e => e.OldValues).IsRequired(false).IsUnicode(false);
            builder.Property(e => e.NewValues).IsRequired(false).IsUnicode(false);
            builder.Property(e => e.ItemId).IsRequired();
            builder.HasIndex(e => e.ItemId);
            builder.HasIndex(e => e.TableName);
        }
    }
}

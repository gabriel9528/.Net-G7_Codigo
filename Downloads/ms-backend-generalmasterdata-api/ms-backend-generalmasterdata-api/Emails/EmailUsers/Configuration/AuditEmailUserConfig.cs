using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Common.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Emails.EmailUsers.Domain.Entities;

namespace AnaPrevention.GeneralMasterData.Api.Emails.EmailUsers.Configuration
{
    public class AuditEmailUserConfig : IEntityTypeConfiguration<AuditEmailUser>
    {
        public void Configure(EntityTypeBuilder<AuditEmailUser> builder)
        {
            builder.ToTable("occupationalAuditEmaiUser").HasKey(k => k.Id);
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

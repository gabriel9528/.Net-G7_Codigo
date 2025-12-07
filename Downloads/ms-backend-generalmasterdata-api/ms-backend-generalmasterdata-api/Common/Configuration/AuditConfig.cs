using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Common.Domain.Entities;

namespace AnaPrevention.GeneralMasterData.Api.Common.Configuration
{
    internal class AuditConfig : IEntityTypeConfiguration<Audit>
    {
        public void Configure(EntityTypeBuilder<Audit> entity)
        {
            entity.HasKey(e => e.Id);

            entity.ToTable("auditMaster");
            entity.Property(e => e.Id);
            entity.Property(e => e.Datetime).IsRequired();
            entity.Property(e => e.AuditType).HasMaxLength(CommonStatic.DescriptionMaxLength).IsRequired().IsUnicode(false);
            entity.Property(e => e.UserId).IsRequired();
            entity.Property(e => e.TableName).HasMaxLength(CommonStatic.DescriptionMaxLength).IsRequired().IsUnicode(false);
            entity.Property(e => e.KeyValues).IsRequired(false).IsUnicode(false);
            entity.Property(e => e.OldValues).IsRequired(false).IsUnicode(false);
            entity.Property(e => e.NewValues).IsRequired(false).IsUnicode(false);
            entity.Property(e => e.ItemId).IsRequired();
            entity.HasIndex(e => e.ItemId);
            entity.HasIndex(e => e.TableName);
        }
    }
}

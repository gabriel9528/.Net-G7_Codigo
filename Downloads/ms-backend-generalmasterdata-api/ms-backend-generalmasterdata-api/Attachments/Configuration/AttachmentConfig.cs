using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AnaPrevention.GeneralMasterData.Api.Attachments.Domain.Entities;

namespace AnaPrevention.GeneralMasterData.Api.Attachments.Configuration
{
    public class AttachmentConfig : IEntityTypeConfiguration<Attachment>
    {
        public void Configure(EntityTypeBuilder<Attachment> builder)
        {
            builder.ToTable("attachments").HasKey(k => k.Id);
            builder.Property(t1 => t1.Name).IsUnicode(false);
            builder.Property(t1 => t1.Url).IsUnicode(false);
            builder.HasIndex(t1 => t1.EntityId);
            builder.Property(t1 => t1.DateCreated);


        }
    }
}
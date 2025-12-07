using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Common.Domain.ValueObjects;
using AnaPrevention.GeneralMasterData.Api.Emails.EmailUsers.Domain.Entities;

namespace AnaPrevention.GeneralMasterData.Api.Emails.EmailUsers.Configuration
{
    public class EmailUserConfig : IEntityTypeConfiguration<EmailUser>
    {

        public void Configure(EntityTypeBuilder<EmailUser> builder)
        {
            builder.ToTable("EmailUsers").HasKey(k => k.Id);
            builder.Property(p => p.Email).HasMaxLength(CommonStatic.DescriptionMaxLength).IsRequired().IsUnicode(false).HasConversion(p => p.Value, p => Email.Create(p).Value);
            builder.Property(p => p.Name).HasMaxLength(CommonStatic.DescriptionMaxLength).IsRequired().IsUnicode(false);
            builder.Property(p => p.Password).HasMaxLength(CommonStatic.DescriptionMaxLength).IsRequired().IsUnicode(false); ;
            builder.Property(p => p.Port).IsRequired();
            builder.Property(p => p.Host).HasMaxLength(CommonStatic.DescriptionMaxLength).IsRequired().IsUnicode(false);
            builder.Property(p => p.ProtocolType).IsRequired();
            builder.Property(p => p.Status).IsRequired().HasDefaultValue(true);
        }
    }
}

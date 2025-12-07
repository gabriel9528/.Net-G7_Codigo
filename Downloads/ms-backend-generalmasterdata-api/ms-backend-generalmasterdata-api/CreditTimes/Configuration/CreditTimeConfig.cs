using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.CreditTimes.Domain.Entities;

namespace AnaPrevention.GeneralMasterData.Api.CreditTimes.Configuration
{
    public class CreditTimeConfig : IEntityTypeConfiguration<CreditTime>
    {
        public void Configure(EntityTypeBuilder<CreditTime> builder)
        {
            builder.ToTable("creditTimes").HasKey(k => k.Id);
            builder.Property(p => p.Description).HasMaxLength(CommonStatic.DescriptionMaxLength).IsRequired().IsUnicode(false);
            builder.Property(p => p.Code).HasMaxLength(CommonStatic.CodeMaxLength).IsRequired().IsUnicode(false);
            builder.Property(p => p.NumberDay).IsRequired();
            builder.Property(p => p.Status).IsRequired();
            builder.HasIndex(p => p.Description);
            builder.HasIndex(p => p.Code).IsUnique();
           
        }
    }
}

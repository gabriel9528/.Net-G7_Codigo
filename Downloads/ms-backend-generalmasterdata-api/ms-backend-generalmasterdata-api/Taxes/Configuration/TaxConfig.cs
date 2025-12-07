using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Taxes.Domain.Entities;

namespace AnaPrevention.GeneralMasterData.Api.Taxes.Configuration
{
    public class TaxConfig : IEntityTypeConfiguration<Tax>
    {
        public void Configure(EntityTypeBuilder<Tax> builder)
        {
            builder.ToTable("taxes").HasKey(k => k.Id);
            builder.Property(p => p.Description).HasMaxLength(CommonStatic.DescriptionMaxLength).IsRequired().IsUnicode(false);
            builder.Property(p => p.Code).HasMaxLength(CommonStatic.CodeMaxLength).IsRequired().IsUnicode(false);
            builder.Property(p => p.Rate).IsRequired();
            builder.Property(p => p.Status).IsRequired();

        }
    }
}

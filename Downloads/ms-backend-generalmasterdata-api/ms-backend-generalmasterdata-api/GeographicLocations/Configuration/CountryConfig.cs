using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.GeographicLocations.Domain.Entities;

namespace AnaPrevention.GeneralMasterData.Api.GeographicLocations.Configuration
{
    public class CountryConfig : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.ToTable("countries").HasKey(k => k.Id);
            builder.Property(p => p.Id).HasMaxLength(CommonStatic.CodeMaxLength).IsRequired().IsUnicode(false);
            builder.Property(p => p.Description).HasMaxLength(CommonStatic.DescriptionMaxLength).IsRequired().IsUnicode(false);
            builder.Property(p => p.SecondDescription).HasMaxLength(CommonStatic.DescriptionMaxLength).IsRequired().IsUnicode(false);
            builder.Property(p => p.SecondCode).HasMaxLength(CommonStatic.CodeMaxLength).IsRequired().IsUnicode(false);
            builder.Property(p => p.PhoneCode).HasMaxLength(CommonStatic.CodeMaxLength).IsRequired().IsUnicode(false);
            builder.Property(p => p.Status).IsRequired();
            builder.HasIndex(p => p.Description).IsUnique();
            builder.HasIndex(p => p.SecondCode).IsUnique();
            builder.HasMany<Department>().WithOne(p => p.Country).HasForeignKey(p => p.CountryId).OnDelete(DeleteBehavior.Restrict); ;

        }
    }
}

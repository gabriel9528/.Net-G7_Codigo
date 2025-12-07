using Microsoft.EntityFrameworkCore;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.GeographicLocations.Domain.Entities;

namespace AnaPrevention.GeneralMasterData.Api.GeographicLocations.Configuration
{
    public class DepartmentConfig : IEntityTypeConfiguration<Department>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Department> builder)
        {
            builder.ToTable("departments").HasKey(k => k.Id);
            builder.Property(p => p.Id).HasMaxLength(CommonStatic.CodeMaxLength).IsRequired().IsUnicode(false);
            builder.Property(p => p.Description).HasMaxLength(CommonStatic.DescriptionMaxLength).IsRequired().IsUnicode(false);
            builder.Property(p => p.CountryId).HasMaxLength(CommonStatic.CodeMaxLength).IsRequired().IsUnicode(false);
            builder.Property(p => p.Status).IsRequired();
            builder.HasOne(c => c.Country).WithMany().HasForeignKey(c => c.CountryId).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany<Province>().WithOne(p => p.Department).HasForeignKey(p => p.DepartmentId).OnDelete(DeleteBehavior.Restrict); ;

        }
    }
}

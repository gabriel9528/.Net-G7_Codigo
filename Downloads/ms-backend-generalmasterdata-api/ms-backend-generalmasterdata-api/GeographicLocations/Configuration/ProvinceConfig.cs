using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.GeographicLocations.Domain.Entities;

namespace AnaPrevention.GeneralMasterData.Api.GeographicLocations.Configuration
{
    public class ProvinceConfig : IEntityTypeConfiguration<Province>
    {
        public void Configure(EntityTypeBuilder<Province> builder)
        {
            builder.ToTable("provinces").HasKey(k => k.Id);
            builder.Property(p => p.Id).HasMaxLength(CommonStatic.CodeMaxLength).IsRequired().IsUnicode(false);
            builder.Property(p => p.Description).HasMaxLength(CommonStatic.DescriptionMaxLength).IsRequired().IsUnicode(false);
            builder.Property(p => p.DepartmentId).HasMaxLength(CommonStatic.CodeMaxLength).IsRequired().IsUnicode(false);
            builder.Property(p => p.Status).IsRequired();
            builder.HasOne(c => c.Department).WithMany().HasForeignKey(c => c.DepartmentId).OnDelete(DeleteBehavior.Restrict); ;
            builder.HasMany<District>().WithOne(p => p.Province).HasForeignKey(p => p.ProvinceId).OnDelete(DeleteBehavior.Restrict);


        }
    }
}
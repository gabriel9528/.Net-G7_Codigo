using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Specialties.Entities;

namespace AnaPrevention.GeneralMasterData.Api.Specialties.Configuration
{
    public class SpecialtyConfig:IEntityTypeConfiguration<Specialty>
    { 
        public void Configure(EntityTypeBuilder<Specialty> builder)
        {
            builder.ToTable("specialties").HasKey(k => k.Id);
            builder.Property(p => p.Description).HasMaxLength(CommonStatic.DescriptionMaxLength).IsRequired().IsUnicode(false);
            builder.Property(p => p.Code).HasMaxLength(CommonStatic.CodeMaxLength).IsRequired().IsUnicode(false);
            builder.Property(p => p.Status).IsRequired();
        }
    }
}

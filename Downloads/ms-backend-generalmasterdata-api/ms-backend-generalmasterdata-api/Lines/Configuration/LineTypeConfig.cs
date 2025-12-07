using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Lines.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Lines.Domain.Enums;

namespace AnaPrevention.GeneralMasterData.Api.Lines.Configuration
{
    public class LineTypeConfig : IEntityTypeConfiguration<LineType>
    {
        public void Configure(EntityTypeBuilder<LineType> builder)
        {            
            builder.ToTable("lineTypes").HasKey(k => k.Id);
            builder.Property(p => p.Description).HasMaxLength(CommonStatic.DescriptionMaxLength).IsRequired().IsUnicode(false);
            builder.HasIndex(p => p.Description).IsUnique();
            
        }
    }
}

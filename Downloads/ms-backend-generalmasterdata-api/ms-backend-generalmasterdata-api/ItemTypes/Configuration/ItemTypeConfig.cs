using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.ItemTypes.Domain.Entities;

namespace AnaPrevention.GeneralMasterData.Api.ItemTypes.Configuration
{
    public class ItemTypeConfig : IEntityTypeConfiguration<ItemType>
    {
        public void Configure(EntityTypeBuilder<ItemType> builder)
        {
            builder.ToTable("itemTypes").HasKey(k => k.Id);
            builder.Property(p => p.Description).HasMaxLength(CommonStatic.DescriptionMaxLength).IsUnicode(false).IsRequired();
            builder.Property(p => p.Code).HasMaxLength(CommonStatic.CodeMaxLength).IsUnicode(false).IsRequired();
            builder.Property(p => p.Status).IsRequired();
        }
    }
}

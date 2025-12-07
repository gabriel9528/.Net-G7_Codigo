using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.ExistenceTypes.Domain.Entities;

namespace AnaPrevention.GeneralMasterData.Api.ExistenceTypes.Configuration
{
    public class ExistenceTypeConfig : IEntityTypeConfiguration<ExistenceType>
    {
        public void Configure(EntityTypeBuilder<ExistenceType> builder)
        {
            builder.ToTable("existenceTypes").HasKey(k => k.Id);
            builder.Property(p => p.Description).HasMaxLength(CommonStatic.DescriptionMaxLength).IsUnicode(false).IsRequired();
            builder.Property(p => p.Code).IsUnicode(false).IsRequired();
            builder.Property(p => p.Status).IsRequired();

        }
    }
}

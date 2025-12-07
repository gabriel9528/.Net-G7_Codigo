using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.SubsidiaryTypes.Domain.Entities;

namespace AnaPrevention.GeneralMasterData.Api.SubsidiaryTypes.Configuration
{
    internal class SubsidiaryTypeConfig : IEntityTypeConfiguration<SubsidiaryType>
    {
        public void Configure(EntityTypeBuilder<SubsidiaryType> builder)
        {
            builder.ToTable("subsidiaryTypes").HasKey(k => k.Id);
            builder.Property(p => p.Code).HasMaxLength(CommonStatic.CodeMaxLength).IsRequired().IsUnicode(false);
            builder.Property(p => p.Description).HasMaxLength(CommonStatic.DescriptionMaxLength).IsRequired().IsUnicode(false);
            builder.Property(p => p.Status).IsRequired();
            //builder.HasData(new List<SubsidiaryType>()
            //{
            //    new("Clinica","001",Guid.Parse("81D84F23-D7CE-4BEC-8D55-4D733B8F95DF")),
            //    new("Policlinico","002",Guid.Parse("D190CA59-ED8E-435B-8F63-E7BB88EC4ACE")),
            //});
        }
    }
}
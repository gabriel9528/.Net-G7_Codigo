using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.SubFamilies.Domain.Entities;

namespace AnaPrevention.GeneralMasterData.Api.SubFamilies.Configuration
{
    public class SubFamilyConfig : IEntityTypeConfiguration<SubFamily>
    {
        public void Configure(EntityTypeBuilder<SubFamily> builder)
        {
            builder.ToTable("subfamilies").HasKey(k => k.Id);
            builder.Property(p => p.Code).HasMaxLength(CommonStatic.CodeMaxLength).IsRequired().IsUnicode();
            builder.Property(p => p.Description).HasMaxLength(CommonStatic.DescriptionMaxLength).IsRequired().IsUnicode(); ;
            builder.Property(p => p.Status).IsRequired();
            builder.Property(p => p.OrderRow).IsRequired(true).HasDefaultValue(CommonStatic.DefaultOrderRow);
            builder.Property(p => p.CompanyId).IsRequired();
            builder.Property(p => p.FamilyId).IsRequired();
            builder.HasOne(c => c.Family).WithMany().HasForeignKey(c => c.FamilyId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(c => c.Company).WithMany().HasForeignKey(c => c.CompanyId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}

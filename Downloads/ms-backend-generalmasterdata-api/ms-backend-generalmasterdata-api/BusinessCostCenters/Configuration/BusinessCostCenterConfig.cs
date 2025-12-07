using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AnaPrevention.GeneralMasterData.Api.BusinessCostCenters.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;

namespace AnaPrevention.GeneralMasterData.Api.BusinessCostCenters.Configuration
{
    public class BusinessCostCenterConfig : IEntityTypeConfiguration<BusinessCostCenter>
    {
        public void Configure(EntityTypeBuilder<BusinessCostCenter> builder)
        {
            builder.ToTable("businessCostCenters").HasKey(k => k.Id);
            builder.Property(p => p.Description).HasMaxLength(CommonStatic.DescriptionMaxLength).IsRequired().IsUnicode(false);
            builder.Property(p => p.BusinessId).IsRequired();
            builder.Property(p => p.Status).IsRequired();
            builder.HasOne(c => c.Business).WithMany().HasForeignKey(c => c.BusinessId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}

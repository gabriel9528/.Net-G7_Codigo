using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AnaPrevention.GeneralMasterData.Api.BusinessProjects.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;

namespace AnaPrevention.GeneralMasterData.Api.BusinessProjects.Configuration
{
    public class BusinessProjectConfig : IEntityTypeConfiguration<BusinessProject>
    {
        public void Configure(EntityTypeBuilder<BusinessProject> builder)
        {
            builder.ToTable("businessProjects").HasKey(k => k.Id);
            builder.Property(p => p.Description).HasMaxLength(CommonStatic.DescriptionMaxLength).IsRequired().IsUnicode(false);
            builder.Property(p => p.BusinessId).IsRequired();
            builder.Property(p => p.Status).IsRequired();
            builder.HasOne(c => c.Business).WithMany().HasForeignKey(c => c.BusinessId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}

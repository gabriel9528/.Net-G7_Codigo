using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using AnaPrevention.GeneralMasterData.Api.BusinessCampaigns.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Common.Domain.ValueObjects;

namespace AnaPrevention.GeneralMasterData.Api.BusinessCampaigns.Configuration
{
    public class BusinessCampaignConfig : IEntityTypeConfiguration<BusinessCampaign>
    {

        public void Configure(EntityTypeBuilder<BusinessCampaign> builder)
        {

            builder.ToTable("businessCampaigns").HasKey(k => k.Id);
            builder.Property(p => p.Description).HasMaxLength(CommonStatic.DescriptionMaxLength).IsRequired().IsUnicode(false);
            builder.Property(p => p.BusinessId).IsRequired();
            builder.Property(p => p.Status).IsRequired();
            builder.Property(p => p.DateFinish).HasConversion(CommonStatic.ConvertDate)
                        .HasColumnName("dateFinish").IsRequired();
            builder.Property(p => p.DateStart).HasConversion(CommonStatic.ConvertDate)
                        .HasColumnName("dateStart").IsRequired();

            builder.HasOne(c => c.Business).WithMany().HasForeignKey(c => c.BusinessId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}

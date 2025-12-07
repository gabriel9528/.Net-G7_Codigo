using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;

using AnaPrevention.GeneralMasterData.Api.ServiceTypes.Domain.Entities;


namespace AnaPrevention.GeneralMasterData.Api.ServiceTypes.Configuration
{
    public class ServiceTypeConfig : IEntityTypeConfiguration<ServiceType>
    {
        public void Configure(EntityTypeBuilder<ServiceType> builder)
        {
            builder.ToTable("serviceTypes").HasKey(k => k.Id);
            builder.Property(p => p.Code).HasMaxLength(CommonStatic.CodeMaxLength).IsRequired().IsUnicode(false);
            builder.Property(p => p.Description).HasMaxLength(CommonStatic.DescriptionMaxLength).IsRequired().IsUnicode(false);
            builder.Property(p => p.Status).IsRequired();
            builder.Property(p => p.CompanyId).IsRequired();
            // builder.HasOne(c => c.Company).WithMany().HasForeignKey(c => c.CompanyId).OnDelete(DeleteBehavior.Restrict);
            //builder.HasData(new List<ServiceType>()
            //{
            //    new ServiceType("OCUPACIONAL",ServiceTypeEnum.OCCUPATIONAL,Guid.Parse("721b327e-82be-4345-ac30-3c980b804f3d"),Guid.Parse("0629749d-6535-11ed-a147-0a971fe1e98d")),
            //    new ServiceType("ASISTENCIAL",ServiceTypeEnum.ASASSISTENTIAL,Guid.Parse("721b327e-82be-4345-ac30-3c980b804f3d"),Guid.Parse("15e6a44e-6535-11ed-a147-0a971fe1e98d")),
            //});
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AnaPrevention.GeneralMasterData.Api.ServiceTypes.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.ServiceTypes.Domain.Enums;

namespace AnaPrevention.GeneralMasterData.Api.ServiceTypes.Configuration
{
    public class ServiceTypeSeed : IEntityTypeConfiguration<ServiceType>
    {
        public void Configure(EntityTypeBuilder<ServiceType> builder)
        {
            builder.HasData(new List<ServiceType>()
            {
                new ServiceType("OCUPACIONAL",ServiceTypeEnum.OCCUPATIONAL,Guid.Parse("721b327e-82be-4345-ac30-3c980b804f3d"),Guid.Parse("0629749d-6535-11ed-a147-0a971fe1e98d")),
                new ServiceType("ASISTENCIAL",ServiceTypeEnum.ASASSISTENTIAL,Guid.Parse("721b327e-82be-4345-ac30-3c980b804f3d"),Guid.Parse("15e6a44e-6535-11ed-a147-0a971fe1e98d")),
            });
        }
    }
}

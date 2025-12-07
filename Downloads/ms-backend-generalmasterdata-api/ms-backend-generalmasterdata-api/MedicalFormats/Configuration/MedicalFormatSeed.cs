using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AnaPrevention.GeneralMasterData.Api.ServiceCatalogs.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.ServiceCatalogs.Domain.Enum;

namespace AnaPrevention.GeneralMasterData.Api.MedicalFormats.Configuration
{
    public class MedicalFormatSeed : IEntityTypeConfiguration<MedicalFormat>
    {
        public void Configure(EntityTypeBuilder<MedicalFormat> builder)
        {
            builder.HasData(new List<MedicalFormat>()
            {
                new MedicalFormat("FORMATO 312","000001",MedicalFormatType.FORMAT312,Guid.Parse("9ba50dbf-652f-11ed-a147-0a971fe1e98d")),
                new MedicalFormat("ANTAPACCAY","000002",MedicalFormatType.ANTAPACCAY,Guid.Parse("ace48e53-652f-11ed-a147-0a971fe1e98d")),
                new MedicalFormat("ANEXO16","000003",MedicalFormatType.FORMAT312,Guid.Parse("b5fe7999-652f-11ed-a147-0a971fe1e98d")),
            });
        }
    }
}

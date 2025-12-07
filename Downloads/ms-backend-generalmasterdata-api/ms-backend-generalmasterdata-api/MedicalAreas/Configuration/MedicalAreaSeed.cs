using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AnaPrevention.GeneralMasterData.Api.MedicalAreas.Domain.Entities;

namespace AnaPrevention.GeneralMasterData.Api.MedicalAreas.Configuration
{
    public class MedicalAreaSeed : IEntityTypeConfiguration<MedicalArea>
    {
        public void Configure(EntityTypeBuilder<MedicalArea> builder)
        {
            builder.HasData(new List<MedicalArea>()
            {
                new MedicalArea("ESPIROMETRIA","000001",Guid.Parse("9ba50dbf-652f-11ed-a147-0a971fe1e98d")),
                new MedicalArea("AUDIOMETRIA","000002",Guid.Parse("ace48e53-652f-11ed-a147-0a971fe1e98d")),
                new MedicalArea("CARDIOLOGIA","000003",Guid.Parse("b5fe7999-652f-11ed-a147-0a971fe1e98d")),
                new MedicalArea("MEDICINA","000004",Guid.Parse("c376e0cb-652f-11ed-a147-0a971fe1e98d")),
                new MedicalArea("OFTALMOLOGIA","000005",Guid.Parse("ce4dd950-652f-11ed-a147-0a971fe1e98d")),
                new MedicalArea("PSICOLOGIA","000006",Guid.Parse("d2e4c621-652f-11ed-a147-0a971fe1e98d")),
                new MedicalArea("PSICOSENSOMETRICO","000007",Guid.Parse("ed154945-652f-11ed-a147-0a971fe1e98d")),
                new MedicalArea("RAYOS X","000008",Guid.Parse("f5c0b8c3-652f-11ed-a147-0a971fe1e98d")),
            });
        }
    }
}

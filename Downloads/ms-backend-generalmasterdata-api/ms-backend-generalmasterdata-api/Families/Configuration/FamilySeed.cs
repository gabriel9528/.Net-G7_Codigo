using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AnaPrevention.GeneralMasterData.Api.Families.Domain.Entities;

namespace AnaPrevention.GeneralMasterData.Api.Families.Configuration
{
    public class FamilySeed : IEntityTypeConfiguration<Family>
    {
        public void Configure(EntityTypeBuilder<Family> builder)
        {
            builder.HasData(new List<Family>()
            {
                new("ANTROPOMETRIA","000003",Guid.Parse("721b327e-82be-4345-ac30-3c980b804f3d"),Guid.Parse("6601f017-f7ca-4db6-a145-8bb8b51eccec"),Guid.Parse("36ef981a-ddf7-4aaf-9b26-388a828f3c27")),
                new("AUDIOMETRIA","000004",Guid.Parse("721b327e-82be-4345-ac30-3c980b804f3d"),Guid.Parse("b2c1c552-4f0a-4001-85da-681f331a27c0"),Guid.Parse("9578ba43-7462-451d-825b-257fc21c15fb")),
                new("CARDIOLOGIA","000005",Guid.Parse("721b327e-82be-4345-ac30-3c980b804f3d"),Guid.Parse("654454fa-dece-4405-a001-1b01db9e7980"),Guid.Parse("a6ed769d-d5ad-43e6-985d-c0a037a47ab9")),
                new("ENFERMERIA","000006",Guid.Parse("721b327e-82be-4345-ac30-3c980b804f3d"),Guid.Parse("c4a18c1b-4813-4a39-8ba5-9cfed93f9855"),Guid.Parse("d083626f-f8e7-4731-87a7-da8ce0f595e1")),
                new("ESPIROMETRIA","000007",Guid.Parse("721b327e-82be-4345-ac30-3c980b804f3d"),Guid.Parse("c0a3a02a-3c16-4b6d-94c8-e9c9e11f9c67"),Guid.Parse("8ced653e-1c53-4383-abb9-e8e487092707")),
                new("LABORATORIO","000009",Guid.Parse("721b327e-82be-4345-ac30-3c980b804f3d"),Guid.Parse("909aa814-9b47-4a36-9dca-a9328ad79a07"),Guid.Parse("116f2a40-b5ed-464a-a62e-d271b82f0954")),
                new("MEDICINA","000010",Guid.Parse("721b327e-82be-4345-ac30-3c980b804f3d"),Guid.Parse("1ef0fabc-c50d-4921-a335-091e714f18be"),Guid.Parse("d4461eae-6719-40d0-8fd6-11bfa1fe6d49")),
                new("OPTOMETRIA","000011",Guid.Parse("721b327e-82be-4345-ac30-3c980b804f3d"),Guid.Parse("1face69c-fe6c-42ef-93dc-e65bf139f7ea"),Guid.Parse("aa2d099f-ad87-4637-9be8-f83cfc5b3b73")),
                new("PSICOSENSOMETRIA","000012",Guid.Parse("721b327e-82be-4345-ac30-3c980b804f3d"),Guid.Parse("d74fc516-d4f0-4304-9a4a-a19b59454ee0"),Guid.Parse("5a897fe1-c070-420e-b526-a6e275b94819")),
                new("RADIOLOGIA","000008",Guid.Parse("721b327e-82be-4345-ac30-3c980b804f3d"),Guid.Parse("f2d1d3c1-bc10-418b-a2f2-f7c055666e2a"),Guid.Parse("b8e50140-e41b-4ebc-8023-821e8ac7ac81")),
                new("SERVICIOS","000013",Guid.Parse("721b327e-82be-4345-ac30-3c980b804f3d"),Guid.Parse("e52dc816-2309-48ef-a22d-2d2834d615a6"),Guid.Parse("705d835a-2933-4356-8b77-c6ed55f29a9b")),

            });
        }
    }
}

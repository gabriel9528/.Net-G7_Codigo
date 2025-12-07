using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AnaPrevention.GeneralMasterData.Api.Families.Domain.Entities;

namespace AnaPrevention.GeneralMasterData.Api.Lines.Configuration
{
    public class LineSeed : IEntityTypeConfiguration<Line>
    {
        public void Configure(EntityTypeBuilder<Line> builder)
        {
            List<Line> DataInitial = new()
            {
                new Line("SERVICIOS","000012",Guid.Parse("721b327e-82be-4345-ac30-3c980b804f3d"),Guid.Parse("5600564f-1660-4b90-b5a4-423e11bdb3d6"),Guid.Parse("e52dc816-2309-48ef-a22d-2d2834d615a6")),
                new Line("PSICOSENSOMETRIA","000011",Guid.Parse("721b327e-82be-4345-ac30-3c980b804f3d"),Guid.Parse("5600564f-1660-4b90-b5a4-423e11bdb3d6"),Guid.Parse("d74fc516-d4f0-4304-9a4a-a19b59454ee0")),
                new Line("OPTOMETRIA","000010",Guid.Parse("721b327e-82be-4345-ac30-3c980b804f3d"),Guid.Parse("5600564f-1660-4b90-b5a4-423e11bdb3d6"),Guid.Parse("1face69c-fe6c-42ef-93dc-e65bf139f7ea")),
                new Line("MEDICINA","000002",Guid.Parse("721b327e-82be-4345-ac30-3c980b804f3d"),Guid.Parse("5600564f-1660-4b90-b5a4-423e11bdb3d6"),Guid.Parse("1ef0fabc-c50d-4921-a335-091e714f18be")),
                new Line("LABORATORIO","000009",Guid.Parse("721b327e-82be-4345-ac30-3c980b804f3d"),Guid.Parse("fa25b52e-0252-4f79-a690-6b0ce8ebc154"),Guid.Parse("909aa814-9b47-4a36-9dca-a9328ad79a07")),
                new Line("IMAGENOLOGIA","000008",Guid.Parse("721b327e-82be-4345-ac30-3c980b804f3d"),Guid.Parse("5600564f-1660-4b90-b5a4-423e11bdb3d6"),Guid.Parse("f2d1d3c1-bc10-418b-a2f2-f7c055666e2a")),
                new Line("ESPIROMETRIA","000007",Guid.Parse("721b327e-82be-4345-ac30-3c980b804f3d"),Guid.Parse("5600564f-1660-4b90-b5a4-423e11bdb3d6"),Guid.Parse("c0a3a02a-3c16-4b6d-94c8-e9c9e11f9c67")),
                new Line("ENFERMERIA","000006",Guid.Parse("721b327e-82be-4345-ac30-3c980b804f3d"),Guid.Parse("5600564f-1660-4b90-b5a4-423e11bdb3d6"),Guid.Parse("c4a18c1b-4813-4a39-8ba5-9cfed93f9855")),
                new Line("CARDIOLOGIA","000005",Guid.Parse("721b327e-82be-4345-ac30-3c980b804f3d"),Guid.Parse("5600564f-1660-4b90-b5a4-423e11bdb3d6"),Guid.Parse("654454fa-dece-4405-a001-1b01db9e7980")),
                new Line("AUDIOMETRIA","000004",Guid.Parse("721b327e-82be-4345-ac30-3c980b804f3d"),Guid.Parse("5600564f-1660-4b90-b5a4-423e11bdb3d6"),Guid.Parse("b2c1c552-4f0a-4001-85da-681f331a27c0")),
                new Line("ANTROPOMETRIA","000003",Guid.Parse("721b327e-82be-4345-ac30-3c980b804f3d"),Guid.Parse("5600564f-1660-4b90-b5a4-423e11bdb3d6"),Guid.Parse("6601f017-f7ca-4db6-a145-8bb8b51eccec")),
            };

            builder.HasData(DataInitial);
        }
    }
}

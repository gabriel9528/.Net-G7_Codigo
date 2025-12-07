using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AnaPrevention.GeneralMasterData.Api.SubFamilies.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.SubFamilies.Domain.Enums;

namespace AnaPrevention.GeneralMasterData.Api.SubFamilies.Configuration
{
    public class SubFamilySeed : IEntityTypeConfiguration<SubFamily>
    {
        public void Configure(EntityTypeBuilder<SubFamily> builder)
        {
            builder.HasData(new List<SubFamily>()
            {
                new("BIOQUIMICA","000008",Guid.Parse("721b327e-82be-4345-ac30-3c980b804f3d"),Guid.Parse("d083626f-f8e7-4731-87a7-da8ce0f595e1"),Guid.Parse("36bb82ce-7b83-4a97-8d38-fae06f28b5be"),(SubFamilyType)0),
                new("CONSULTAS","000006",Guid.Parse("721b327e-82be-4345-ac30-3c980b804f3d"),Guid.Parse("d083626f-f8e7-4731-87a7-da8ce0f595e1"),Guid.Parse("10e43b4d-a6d4-4e2b-a43b-0730ca40f2da"),(SubFamilyType)1),
                new("CONSULTAS","000024",Guid.Parse("721b327e-82be-4345-ac30-3c980b804f3d"),Guid.Parse("d4461eae-6719-40d0-8fd6-11bfa1fe6d49"),Guid.Parse("c91b2367-8426-40a9-98cb-e20e4a0985f9"),(SubFamilyType)0),
                new("DECLARACIONES","000020",Guid.Parse("721b327e-82be-4345-ac30-3c980b804f3d"),Guid.Parse("d4461eae-6719-40d0-8fd6-11bfa1fe6d49"),Guid.Parse("f9373345-ada6-4225-83b9-a3787771c298"),(SubFamilyType)1),
                new("EKG","000005",Guid.Parse("721b327e-82be-4345-ac30-3c980b804f3d"),Guid.Parse("a6ed769d-d5ad-43e6-985d-c0a037a47ab9"),Guid.Parse("177a534a-6482-41ec-a2a3-07be048a2695"),(SubFamilyType)0),
                new("ENCUESTAS","000017",Guid.Parse("721b327e-82be-4345-ac30-3c980b804f3d"),Guid.Parse("5a897fe1-c070-420e-b526-a6e275b94819"),Guid.Parse("6d4f3371-3b0a-4329-8946-394aee99cc98"),(SubFamilyType)0),
                new("ENCUESTAS","000023",Guid.Parse("721b327e-82be-4345-ac30-3c980b804f3d"),Guid.Parse("d4461eae-6719-40d0-8fd6-11bfa1fe6d49"),Guid.Parse("cd552d10-223e-4920-bc0b-23e87cb04e76"),(SubFamilyType)0),
                new("EXAMENES","000003",Guid.Parse("721b327e-82be-4345-ac30-3c980b804f3d"),Guid.Parse("36ef981a-ddf7-4aaf-9b26-388a828f3c27"),Guid.Parse("d37fed97-e214-48cb-ba9b-a0b3f9fddfac"),(SubFamilyType)0),
                new("EXAMENES","000004",Guid.Parse("721b327e-82be-4345-ac30-3c980b804f3d"),Guid.Parse("9578ba43-7462-451d-825b-257fc21c15fb"),Guid.Parse("cdde9a47-e61b-472b-bd9a-7d0de0171422"),(SubFamilyType)0),
                new("EXAMENES","000007",Guid.Parse("721b327e-82be-4345-ac30-3c980b804f3d"),Guid.Parse("8ced653e-1c53-4383-abb9-e8e487092707"),Guid.Parse("34ede200-6237-4c45-91e6-b16545c07cd0"),(SubFamilyType)0),
                new("EXAMENES","000013",Guid.Parse("721b327e-82be-4345-ac30-3c980b804f3d"),Guid.Parse("aa2d099f-ad87-4637-9be8-f83cfc5b3b73"),Guid.Parse("70e5e9eb-9ca4-4929-8c50-0370d7cf1b4b"),(SubFamilyType)0),
                new("EXAMENES","000021",Guid.Parse("721b327e-82be-4345-ac30-3c980b804f3d"),Guid.Parse("d4461eae-6719-40d0-8fd6-11bfa1fe6d49"),Guid.Parse("dda10a5a-6d76-49af-938b-9e063533bd20"),(SubFamilyType)0),
                new("HEMATOLOGIA","000009",Guid.Parse("721b327e-82be-4345-ac30-3c980b804f3d"),Guid.Parse("116f2a40-b5ed-464a-a62e-d271b82f0954"),Guid.Parse("3e460795-6af9-4b29-a094-77ca41bf7f2f"),(SubFamilyType)0),
                new("INDICADORES","000022",Guid.Parse("721b327e-82be-4345-ac30-3c980b804f3d"),Guid.Parse("d4461eae-6719-40d0-8fd6-11bfa1fe6d49"),Guid.Parse("6e61f810-bf26-48b1-aaf2-f5ad575f6bfc"),(SubFamilyType)0),
                new("INFORMES","000015",Guid.Parse("721b327e-82be-4345-ac30-3c980b804f3d"),Guid.Parse("5a897fe1-c070-420e-b526-a6e275b94819"),Guid.Parse("8c8d5495-456c-47a7-8aeb-c9046b262582"),(SubFamilyType)0),
                new("MICROBIOLOGIA","000010",Guid.Parse("721b327e-82be-4345-ac30-3c980b804f3d"),Guid.Parse("116f2a40-b5ed-464a-a62e-d271b82f0954"),Guid.Parse("6724e3a9-dabc-4d69-be57-d596e435ef74"),(SubFamilyType)0),
                new("OTROS SERVICIOS","000019",Guid.Parse("721b327e-82be-4345-ac30-3c980b804f3d"),Guid.Parse("705d835a-2933-4356-8b77-c6ed55f29a9b"),Guid.Parse("a94a2cd1-bc32-4425-a5fd-b2ca01da3a87"),(SubFamilyType)0),
                new("PLACAS","000018",Guid.Parse("721b327e-82be-4345-ac30-3c980b804f3d"),Guid.Parse("b8e50140-e41b-4ebc-8023-821e8ac7ac81"),Guid.Parse("3ec5b0d0-ebbd-43f6-8687-62ff84c4de13"),(SubFamilyType)0),
                new("TOMA DE MUESTRAS","000011",Guid.Parse("721b327e-82be-4345-ac30-3c980b804f3d"),Guid.Parse("116f2a40-b5ed-464a-a62e-d271b82f0954"),Guid.Parse("3d022c7c-c173-40cc-a6d7-93cd07ad6b14"),(SubFamilyType)0),
                new("TOXICOLOGIA","000012",Guid.Parse("721b327e-82be-4345-ac30-3c980b804f3d"),Guid.Parse("116f2a40-b5ed-464a-a62e-d271b82f0954"),Guid.Parse("6c416c17-4513-47f9-a94d-db2b48411b1e"),(SubFamilyType)0),
                new("TEST","000014",Guid.Parse("721b327e-82be-4345-ac30-3c980b804f3d"),Guid.Parse("5a897fe1-c070-420e-b526-a6e275b94819"),Guid.Parse("316e43df-69a9-45d8-9ddd-865d697e8da5"),(SubFamilyType)0),
                new("PRUEBAS","000016",Guid.Parse("721b327e-82be-4345-ac30-3c980b804f3d"),Guid.Parse("5a897fe1-c070-420e-b526-a6e275b94819"),Guid.Parse("72da520a-de6e-4d92-8b62-d3973d318429"),(SubFamilyType)0),

            });
        }
    }
}

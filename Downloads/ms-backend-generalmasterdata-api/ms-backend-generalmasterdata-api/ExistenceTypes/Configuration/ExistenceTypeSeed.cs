using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AnaPrevention.GeneralMasterData.Api.ExistenceTypes.Domain.Entities;

namespace AnaPrevention.GeneralMasterData.Api.ExistenceTypes.Configuration
{
    public class ExistenceTypeSeed : IEntityTypeConfiguration<ExistenceType>
    {
        public void Configure(EntityTypeBuilder<ExistenceType> builder)
        {
            builder.HasData(new List<ExistenceType>()
            {
                new ExistenceType("MERCADERÍAS","000001",Guid.NewGuid()),
                new ExistenceType("PRODUCTOS TERMINADOS","000002",Guid.NewGuid()),
                new ExistenceType("MATERIAS PRIMAS","000003",Guid.NewGuid()),
                new ExistenceType("ENVASES","000004",Guid.NewGuid()),
                new ExistenceType("MATERIALES AUXILIARES","000005",Guid.NewGuid()),
                new ExistenceType("SUMINISTROS","000006",Guid.NewGuid()),
                new ExistenceType("REPUESTOS","000007",Guid.NewGuid()),
                new ExistenceType("EMBALAJES","000008",Guid.NewGuid()),
                new ExistenceType("SUBPRODUCTOS ","000009",Guid.NewGuid()),
                new ExistenceType("DESECHOS Y DESPERDICIOS","000010",Guid.NewGuid()),
                 new ExistenceType("SERVICIO","000011",Guid.NewGuid()),
            });
        }
    }
}

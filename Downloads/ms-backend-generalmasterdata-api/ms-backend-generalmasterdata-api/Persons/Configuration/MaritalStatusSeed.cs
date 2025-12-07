using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AnaPrevention.GeneralMasterData.Api.Persons.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Persons.Domain.Enums;
namespace AnaPrevention.GeneralMasterData.Api.Persons.Configuration
{
    public class MaritalStatusSeed : IEntityTypeConfiguration<MaritalStatus>
    {
        public void Configure(EntityTypeBuilder<MaritalStatus> builder)
        {
            builder.HasData(new List<MaritalStatus>()
            {
                new("Viudo",MaritalStatusType.WIDOWER,Guid.NewGuid()),
                new("Conviviente",MaritalStatusType.COHABITANT,Guid.NewGuid()),
                new("Divorciado",MaritalStatusType.DIVORCED,Guid.NewGuid()),
                new("Casado",MaritalStatusType.MARRIED,Guid.NewGuid()),
                new("Soltero",MaritalStatusType.SINGLE,Guid.NewGuid()),

            });
        }
    }
}

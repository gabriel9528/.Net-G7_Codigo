using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AnaPrevention.GeneralMasterData.Api.Persons.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Persons.Domain.Enums;

namespace AnaPrevention.GeneralMasterData.Api.Persons.Configuration
{
    public class GenderConfigSeed : IEntityTypeConfiguration<Gender>
    {
        public void Configure(EntityTypeBuilder<Gender> builder)
        {
            builder.HasData(new List<Gender>()
            {
                new("Masculuno",GenderType.MALE,Guid.NewGuid()),
                new("Femenino",GenderType.FEMALE,Guid.NewGuid()),
            });
        }
    }
}

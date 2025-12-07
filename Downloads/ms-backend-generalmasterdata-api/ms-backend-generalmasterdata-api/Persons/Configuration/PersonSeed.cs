using Microsoft.EntityFrameworkCore;
using AnaPrevention.GeneralMasterData.Api.Persons.Domain.Entities;

namespace AnaPrevention.GeneralMasterData.Api.Persons.Configuration
{
    public class PersonSeed : IEntityTypeConfiguration<Person>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Person> builder)
        {
            builder.HasData(new Person("99999999", Guid.Parse("C0644A1C-CA2B-4DDA-939B-342B4A45B9A0"), "Admin", "Admin", "", "", null, Guid.Parse("219F04D3-C5A0-4958-9544-618B2EA56610")));
        }
    }
}

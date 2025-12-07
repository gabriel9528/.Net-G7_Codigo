using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AnaPrevention.GeneralMasterData.Api.GeographicLocations.Domain.Entities;

namespace AnaPrevention.GeneralMasterData.Api.GeographicLocations.Configuration
{
    public class DepartmentSeed : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.HasData(new List<Department>()
            {
                new ("1","Amazonas","PE"),
                new ("2","Ancash","PE"),
                new ("3","Apurimac","PE"),
                new ("4","Arequipa","PE"),
                new ("5","Ayacucho","PE"),
                new ("6","Cajamarca","PE"),
                new ("7","Callao","PE"),
                new ("8","Cusco","PE"),
                new ("9","Huancavelica","PE"),
                new ("10","Huanuco","PE"),
                new ("11","Ica","PE"),
                new ("12","Junin","PE"),
                new ("13","La Libertad","PE"),
                new ("14","Lambayeque","PE"),
                new ("15","Lima","PE"),
                new ("16","Loreto","PE"),
                new ("17","Madre De Dios","PE"),
                new ("18","Moquegua","PE"),
                new ("19","Pasco","PE"),
                new ("20","Piura","PE"),
                new ("21","Puno","PE"),
                new ("22","San Martin","PE"),
                new ("23","Tacna","PE"),
                new ("24","Tumbes","PE"),
                new ("25","Ucayali","PE"),
            });
        }
    }
}

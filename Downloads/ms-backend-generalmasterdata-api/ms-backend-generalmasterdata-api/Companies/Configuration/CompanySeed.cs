using Microsoft.EntityFrameworkCore;
using AnaPrevention.GeneralMasterData.Api.Companies.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.Companies.Domain.Entities;
using System.Text.Json;

namespace AnaPrevention.GeneralMasterData.Api.Companies.Configuration
{
    public class CompanySeed : IEntityTypeConfiguration<Company>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Company> builder)
        {
            builder.HasData(new Company("One health", JsonSerializer.Serialize(
               new SettingCompanyRequest()
               {
                   Information = new()
                   {
                       WebSite = "www.Onehealt.com",
                       Phones = "",
                       DescriptionDisplay = "One health",
                   }
               }
           ),
           Guid.Parse("721B327E-82BE-4345-AC30-3C980B804F3D")));
        }
    }
}

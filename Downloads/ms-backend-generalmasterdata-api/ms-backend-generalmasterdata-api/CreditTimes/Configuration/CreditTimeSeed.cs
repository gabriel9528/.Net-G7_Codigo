using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AnaPrevention.GeneralMasterData.Api.CreditTimes.Domain.Entities;

namespace AnaPrevention.GeneralMasterData.Api.CreditTimes.Configuration
{
    public class CreditTimeSeed : IEntityTypeConfiguration<CreditTime>
    {
        public void Configure(EntityTypeBuilder<CreditTime> builder)
        {
            builder.HasData(new List<CreditTime>()
            {
                new CreditTime("Contado","0D",0,Guid.NewGuid())
            });
        }
    }
}

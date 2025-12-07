using Microsoft.EntityFrameworkCore;
using AnaPrevention.GeneralMasterData.Api.Taxes.Domain.Entities;

namespace AnaPrevention.GeneralMasterData.Api.Taxes.Configuration
{
    public class TaxSeed : IEntityTypeConfiguration<Tax>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Tax> builder)
        {
            builder.HasData(
                new Tax("Impuesto sobre el Valor Añadido", 19, "IVA", Guid.Parse("980a43f5-2e68-47fa-ad52-3edef3cdc2b5")),
                new Tax("EXONERADO", 0, "03", Guid.Parse("cb0eb9f7-697e-4bc0-9b2f-c6cefd84832f"))
                );
        }
    }
}

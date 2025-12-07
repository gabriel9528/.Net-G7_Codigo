using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AnaPrevention.GeneralMasterData.Api.CommercialDocumentTypes.Application.Entities;

namespace AnaPrevention.GeneralMasterData.Api.CommercialDocumentTypes.Configuration
{
    public class CommercialDocumentTypeSeed : IEntityTypeConfiguration<CommercialDocumentType>
    {
        public void Configure(EntityTypeBuilder<CommercialDocumentType> builder)
        {
            builder.HasData(new List<CommercialDocumentType>()
            {
                new CommercialDocumentType("FACTURA","000001","FAC",true,true,false,Guid.NewGuid()),

            });
        }
    }
}

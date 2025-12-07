using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AnaPrevention.GeneralMasterData.Api.IdentityDocumentTypes.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.IdentityDocumentTypes.Domain.Enums;

namespace AnaPrevention.GeneralMasterData.Api.IdentityDocumentTypes.Configuration
{
    public class IdentityDocumentTypeSeed : IEntityTypeConfiguration<IdentityDocumentType>
    {
        public void Configure(EntityTypeBuilder<IdentityDocumentType> builder)
        {
            builder.HasData(new List<IdentityDocumentType>()
            {
                new IdentityDocumentType("OTROS TIPOS DE DOCUMENTOS","00","OTR",15, TaxpayerType.DOCUMENT_FOR_FOREIGNERS_ONLY, IndicatorLength.MAXIMUM_LENGTH, InputType.ALPHANUMERIC, Guid.Parse("C0644A1C-CA2B-4DDA-939B-342B4A45B9A0")),
                new IdentityDocumentType("DOCUMENTO NACIONAL DE IDENTIDAD (DNI)","01","DNI",8, TaxpayerType.DOCUMENT_FOR_NATIONALS_ONLY, IndicatorLength.EXACT_LENGTH, InputType.NUMERIC, Guid.NewGuid()),
                new IdentityDocumentType("CARNET DE EXTRANJERIA","04","CDE",12, TaxpayerType.DOCUMENT_FOR_NATIONALS_AND_FOREIGNERS, IndicatorLength.MAXIMUM_LENGTH, InputType.ALPHANUMERIC, Guid.NewGuid()),
                new IdentityDocumentType("REG. UNICO DE CONTRIBUYENTES","06","RUC",11, TaxpayerType.DOCUMENT_FOR_NATIONALS_ONLY, IndicatorLength.EXACT_LENGTH, InputType.NUMERIC, Guid.NewGuid()),
                new IdentityDocumentType("PASAPORTE","07","PASS",11, TaxpayerType.DOCUMENT_FOR_NATIONALS_AND_FOREIGNERS, IndicatorLength.MAXIMUM_LENGTH, InputType.ALPHANUMERIC, Guid.NewGuid()),
                new IdentityDocumentType("PART. DE NACIMIENTO-IDENTIDAD","11","P.NAC",15, TaxpayerType.DOCUMENT_FOR_NATIONALS_ONLY, IndicatorLength.MAXIMUM_LENGTH, InputType.ALPHANUMERIC, Guid.NewGuid()),
                new IdentityDocumentType("Rol único tributario","12","RUT",12, TaxpayerType.DOCUMENT_FOR_FOREIGNERS_ONLY, IndicatorLength.EXACT_LENGTH, InputType.ALPHANUMERIC, Guid.NewGuid()),
            });
        }
    }
}

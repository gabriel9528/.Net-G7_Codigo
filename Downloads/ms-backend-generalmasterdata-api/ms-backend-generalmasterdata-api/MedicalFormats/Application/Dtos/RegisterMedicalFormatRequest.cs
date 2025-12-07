using AnaPrevention.GeneralMasterData.Api.ServiceCatalogs.Domain.Enum;

namespace AnaPrevention.GeneralMasterData.Api.ServiceCatalogs.Application.Dtos
{
    public class RegisterMedicalFormatRequest
    {
        public string Description { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public MedicalFormatType MedicalFormatType { get; set; }

    }
}

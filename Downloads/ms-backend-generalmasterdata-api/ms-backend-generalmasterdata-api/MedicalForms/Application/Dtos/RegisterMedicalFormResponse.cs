using AnaPrevention.GeneralMasterData.Api.ServiceCatalogs.Domain.Enums;

namespace AnaPrevention.GeneralMasterData.Api.ServiceCatalogs.Application.Dtos
{
    public class RegisterMedicalFormResponse
    {
        public Guid Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool Status { get; set; }
        public Guid ServiceTypeId { get; set; }
        public Guid MedicalAreaId { get; set; }
        public MedicalFormsType MedicalFormsType { get; set; }
    }
}

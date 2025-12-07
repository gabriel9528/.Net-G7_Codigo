using AnaPrevention.GeneralMasterData.Api.ServiceCatalogs.Domain.Enums;

namespace AnaPrevention.GeneralMasterData.Api.ServiceCatalogs.Application.Dtos
{
    public class MedicalFormDto
    {
        public Guid Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Guid ServiceTypeId { get; set; }
        public string ServiceType { get; set; } = string.Empty;
        public Guid MedicalAreaId { get; set; }
        public string MedicalArea { get; set; } = string.Empty;
        public MedicalFormsType MedicalFormsType { get; set; }
        public bool Status { get; set; }
        public Guid? OccupationalHealthOrderFormId { get; set; }
    }
}

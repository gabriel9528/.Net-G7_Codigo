using AnaPrevention.GeneralMasterData.Api.ServiceCatalogs.Domain.Enums;

namespace AnaPrevention.GeneralMasterData.Api.ServiceCatalogs.Application.Dtos
{
    public class MedicalFormAttentionMinDto
    {
        public Guid MedicalFormId { get; set; }
        public string Code { get; set; } = string.Empty;
        public string MedicalForm { get; set; } = string.Empty;
        public Guid? OccupationalHealthId { get; set; }
        public MedicalFormsType? MedicalFormsType { get; set; }
        public bool? IsAudited { get; set; }
        public string Icon { get; set; } = string.Empty;
        public bool IsAttended { get; set; }

    }
}

using AnaPrevention.GeneralMasterData.Api.ServiceCatalogs.Domain.Enums;
using AnaPrevention.GeneralMasterData.Api.Settings.Domain.Enums;

namespace AnaPrevention.GeneralMasterData.Api.ServiceCatalogs.Application.Dtos
{
    public class MedicalFormMasiveDto
    {
        public Guid OccupationalHealthOrderId { get; set; }
        public Guid BusinessId { get; set; }
        public string BusinessDescription { get; set; } = string.Empty;
        public Guid? MedicalFormId { get; set; }
        public string Code { get; set; } = string.Empty;
        public string MedicalForm { get; set; } = string.Empty;
        public Guid? OccupationalHealthId { get; set; }
        public bool? IsAudited { get; set; }
        public bool IsAttended { get; set; }
        public MedicalFormsType MedicalFormsType { get; set; } = MedicalFormsType.NONE;
        public OrderFileType OrderFileType { get; set; } = OrderFileType.NONE;
        public bool IsPrintAttachments { get; set; }
    }
}

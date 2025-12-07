

using AnaPrevention.GeneralMasterData.Api.Attachments.Application.Dtos;

namespace AnaPrevention.GeneralMasterData.Api.Businesses.Application.Dtos
{
    public class EditbusinessRequest
	{
        public Guid Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Tradename { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public Guid IdentityDocumentTypeId { get; set; }
        public Guid MedicalFormatId { get; set; }
        public Guid CreditTimeId { get; set; }
        public string DistrictId { get; set; } = string.Empty;
        public string DocumentNumber { get; set; } = string.Empty;
        public string Comment { get; set; } = string.Empty;
        public List<Guid> ListEconomicActivityId { get; set; } = new List<Guid>();
        public DateTime DateInscription { get; set; }
        public bool IsWaybillShipping { get; set; } = true;
        public bool IsSendingResultsPatients { get; set; } = true;
        public bool IsPatientResults { get; set; } = true;
        public bool IsMedicalReportDisplay { get; set; } = true;
        public bool IsGenerateUsers { get; set; } = true;
        public List<Guid>? ExceptionsByMainBusiness { get; set; }
        public bool IsActive { get; set; }
        public List<RegisterAttachmentRequest>? Attachments { get; set; }
        public List<EditAttachmentRequest1>? AttachmentsEdit { get; set; }
        public List<Guid> AttachmentsDelete { get; set; } = new List<Guid>();

    }
}

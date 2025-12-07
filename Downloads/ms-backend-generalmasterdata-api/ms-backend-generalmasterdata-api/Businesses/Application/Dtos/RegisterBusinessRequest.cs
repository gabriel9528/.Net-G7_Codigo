using AnaPrevention.GeneralMasterData.Api.Attachments.Application.Dtos;

namespace AnaPrevention.GeneralMasterData.Api.Businesses.Application.Dtos
{
    public class RegisterBusinessRequest
    {
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
        public bool IsWaybillShipping { get; set; }
        public bool IsSendingResultsPatients { get; set; }
        public bool IsPatientResults { get; set; }
        public bool IsMedicalReportDisplay { get; set; }
        public bool IsGenerateUsers { get; set; }
        public List<Guid>? ExceptionsByMainBusiness { get; set; }
        public bool IsActive { get; set; }
        public List<RegisterAttachmentRequest>? Attachments { get; set; }
    }
}

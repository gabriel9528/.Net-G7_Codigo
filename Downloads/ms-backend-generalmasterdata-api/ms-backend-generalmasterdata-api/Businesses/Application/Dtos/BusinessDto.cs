
using AnaPrevention.GeneralMasterData.Api.EconomicActivities.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.ServiceCatalogs.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.ServiceCatalogs.Domain.Enum;

namespace AnaPrevention.GeneralMasterData.Api.Businesses.Application.Dtos
{
    public class BusinessDto
    {
        public Guid Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Tradename { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public Guid IdentityDocumentTypeId { get; set; }
        public string IdentityDocumentType { get; set; } = string.Empty;
        public Guid MedicalFormatId { get; set; }
        public string MedicalFormat { get; set; } = string.Empty;
        public MedicalFormatType MedicalFormatType { get; set; }
        public Guid CreditTimeId { get; set; }
        public int CreditTimeNumberDay { get; set; }
        public string CreditTime { get; set; } = string.Empty;
        public string DistrictId { get; set; } = string.Empty;
        public string District { get; set; } = string.Empty;
        public string Province { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
        public string DocumentNumber { get; set; } = string.Empty;
        public string Comment { get; set; } = string.Empty;
        public string DocumentNumberAndDescription { get; set; } = string.Empty;
        public bool IsWaybillShipping { get; set; } = true;
        public bool IsSendingResultsPatients { get; set; } = true;
        public bool IsPatientResults { get; set; } = true;
        public bool IsMedicalReportDisplay { get; set; } = true;
        public bool IsGenerateUsers { get; set; } = true;
        public List<Guid>? ExceptionsByMainBusiness { get; set; }
        public DateTime DateInscription { get; set; }
       // public List<Attachment>? ListAttachments { get; set; } = new List<Attachment>();
        public List<EconomicActivityDto>? ListEconomicActivities { get; set; } = new List<EconomicActivityDto>();
        public bool IsActive { get; set; }
        public bool Status { get; set; }
    }
}

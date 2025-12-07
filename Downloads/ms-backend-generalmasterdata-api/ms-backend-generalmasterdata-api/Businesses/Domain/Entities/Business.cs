using AnaPrevention.GeneralMasterData.Api.CreditTimes.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.GeographicLocations.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.IdentityDocumentTypes.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.ServiceCatalogs.Domain.Entities;

namespace AnaPrevention.GeneralMasterData.Api.Businesses.Domain.Entities
{
    public class Business
    {
        public Business()
        {
        }

        public Guid Id { get; set; }
        public string Description { get; set; }
        public string Tradename { get; set; }
        public string Address { get; set; }
        public Guid IdentityDocumentTypeId { get; set; }
        public IdentityDocumentType IdentityDocumentType { get; set; }
        public Guid MedicalFormatId { get; set; }
        public MedicalFormat MedicalFormat { get; set; }
        public Guid CreditTimeId { get; set; }
        public CreditTime CreditTime { get; set; }
        public string DistrictId { get; set; }
        public District District { get; set; }
        public string DocumentNumber { get; set; }
        public string Comment { get; set; }
        public DateTime DateInscription { get; set; }
        public bool IsActive { get; set; }
        public bool IsWaybillShipping { get; set; }
        public bool IsSendingResultsPatients { get; set; }
        public bool IsPatientResults { get; set; }
        public bool IsMedicalReportDisplay { get; set; }
        public bool IsGenerateUsers { get; set; }
        public string? ExceptionsByMainBusinessJson { get; set; } = String.Empty;        
        public bool Status { get; set; }

        public Business(
            string description, string tradename, string address, Guid identityDocumentType_id,
            Guid medicalFormatId, Guid creditTimeId, string district_id, string documentNumber, string comment, DateTime dateInscription, bool isActive, Guid id, bool isWaybillShipping, bool isSendingResultsPatients, bool isPatientResults, bool isMedicalReportDisplay, bool isGenerateUsers, string? exceptionsByMainBusinessJson)
        {
            Description = description;
            Tradename = tradename;
            Address = address;
            IdentityDocumentTypeId = identityDocumentType_id;
            MedicalFormatId = medicalFormatId;
            CreditTimeId = creditTimeId;
            DistrictId = district_id;
            DocumentNumber = documentNumber;
            Comment = comment;
            IsActive = isActive;
            DateInscription = dateInscription;
            Status = true;
            Id = id;
            IsWaybillShipping = isWaybillShipping;
            IsSendingResultsPatients = isSendingResultsPatients;
            IsPatientResults = isPatientResults;
            IsMedicalReportDisplay = isMedicalReportDisplay;
            IsGenerateUsers = isGenerateUsers;
            ExceptionsByMainBusinessJson = exceptionsByMainBusinessJson;
        }
    }
}

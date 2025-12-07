

namespace AnaPrevention.GeneralMasterData.Api.Persons.Application.Dtos
{
    public class RegisterPersonFullRequest
    {
        public string DocumentNumber { get; set; } = string.Empty;
        public Guid IdentityDocumentTypeId { get; set; }
        public string IdentityDocumentTypeDescription { get; set; } = string.Empty;
        public string Names { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string SecondLastName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public Guid? GenderId { get; set; }
        public string? DateBirth { get; set; } = string.Empty;
        public string? PersonalEmail { get; set; } = string.Empty;
        public string? PersonalPhoneNumber { get; set; } = string.Empty;
        public string? SecondDocumentNumber { get; set; } = string.Empty;
        public Guid? SecondIdentityDocumentTypeId { get; set; }
        public string? PersonalAddress { get; set; } = string.Empty;
        public string? EmergencyContactName { get; set; } = string.Empty;
        public string? EmergencyContactNumberPhone { get; set; } = string.Empty;
        public string? EmergencyContactRelationship { get; set; } = string.Empty;
        public Guid? DegreeInstructionId { get; set; }
        public Guid? MaritalStatusId { get; set; }
        public string? CountryResidenceId { get; set; } = string.Empty;
        public string? CountryBirthId { get; set; } = string.Empty;
        public string? DistrictResidenceId { get; set; } = string.Empty;
        public string? DistrictBirthId { get; set; } = string.Empty;
        public string? Photo { get; set; } = string.Empty;
        public bool DataProcessingAuthorization { get; set; } = false;
        public List<OptionYesOrNot>? HealthInsurance { get; set; }
    }
}

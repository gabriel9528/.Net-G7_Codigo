using AnaPrevention.GeneralMasterData.Api.Common.Domain.ValueObjects;
using AnaPrevention.GeneralMasterData.Api.GeographicLocations.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.IdentityDocumentTypes.Domain.Entities;

namespace AnaPrevention.GeneralMasterData.Api.Persons.Domain.Entities
{
    public class Person
    {
        public Guid Id { get; set; }
        public string DocumentNumber { get; set; }
        public Guid IdentityDocumentTypeId { get; set; }
        public IdentityDocumentType IdentityDocumentType { get; set; }
        public string Names { get; set; }
        public string LastName { get; set; }
        public string? SecondLastName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? PersonalPhoneNumber { get; set; }
        public Email? Email { get; set; }
        public Guid? GenderId { get; set; }
        public Gender? Gender { get; set; }
        public Date? DateBirth { get; set; }
        public Email? PersonalEmail { get; set; }
        public string? SecondDocumentNumber { get; set; }
        public Guid? SecondIdentityDocumentTypeId { get; set; }
        public IdentityDocumentType? SecondIdentityDocumentType { get; set; }
        public string? PersonalAddress { get; set; }
        public string? EmergencyContactName { get; set; }
        public string? EmergencyContactNumberPhone { get; set; }
        public string? EmergencyContactRelationship { get; set; }
        public string? Photo { get; set; }
        public Guid? DegreeInstructionId { get; set; }
        public DegreeInstruction? DegreeInstruction { get; set; }
        public Guid? MaritalStatusId { get; set; }
        public MaritalStatus? MaritalStatus { get; set; }
        public string? CountryResidenceId { get; set; }
        public Country? CountryResidence { get; set; }
        public string? CountryBirthId { get; set; }
        public Country? CountryBirth { get; set; }
        public string? DistrictResidenceId { get; set; }
        public District? DistrictResidence { get; set; }
        public string? DistrictBirthId { get; set; }
        public District? DistrictBirth { get; set; }
        public bool ResetPassword { get; set; }
        public bool Status { get; set; }
        public bool DataProcessingAuthorization { get; set; }
        public string? HealthInsuranceJson { get; set; }

        public Person() { }
        public Person(
            string documentNumber,
            Guid IdentityDocumentTypeId,
            string names,
            string lastName,
            string? secondLastName,
            string? phoneNumber,
            Email? email,
            Guid? genderId,
            Date dateBirth,
            Email? personalEmail,
            string? personalPhoneNumber,
            string? secondDocumentNumber,
            Guid? secondIdentityDocumentTypeId,
            string? personalAddress,
            string? emergencyContactName,
            string? emergencyContactNumberPhone,
            string? emergencyContactRelationship,
            Guid? degreeInstructionId,
            Guid? maritalStatusId,
            string? countryResidenceId,
            string? countryBirthId,
            string? districtResidenceId,
            string? districtBirthId,
            string? photo,
            Guid id,
            bool dataProcessingAuthorization = false,
            string? healthInsuranceJson = null)
        {
            DocumentNumber = documentNumber;
            this.IdentityDocumentTypeId = IdentityDocumentTypeId;
            Names = names;
            LastName = lastName;
            SecondLastName = secondLastName;
            PhoneNumber = phoneNumber ?? throw new ArgumentNullException(nameof(phoneNumber));
            Email = email;
            GenderId = genderId;
            DateBirth = dateBirth;
            PersonalEmail = personalEmail;
            PersonalPhoneNumber = personalPhoneNumber;
            SecondDocumentNumber = secondDocumentNumber;
            SecondIdentityDocumentTypeId = secondIdentityDocumentTypeId;
            PersonalAddress = personalAddress;
            EmergencyContactName = emergencyContactName;
            EmergencyContactNumberPhone = emergencyContactNumberPhone;
            EmergencyContactRelationship = emergencyContactRelationship;
            DegreeInstructionId = degreeInstructionId;
            MaritalStatusId = maritalStatusId;
            CountryResidenceId = countryResidenceId;
            DistrictResidenceId = districtResidenceId;
            CountryBirthId = countryBirthId;
            DistrictBirthId = districtBirthId;
            if (!string.IsNullOrWhiteSpace(photo))
                Photo = photo;
            Status = true;
            Id = id;
            ResetPassword = false;
            DataProcessingAuthorization = dataProcessingAuthorization;
            HealthInsuranceJson = healthInsuranceJson;
        }

        public Person(
            string documentNumber,
            Guid identityDocumentTypeId,
            string names,
            string lastName,
            string secondLastName,
            string phoneNumber,
            Email? email
,
            Guid id)
        {
            DocumentNumber = documentNumber;
            IdentityDocumentTypeId = identityDocumentTypeId;
            Names = names;
            LastName = lastName;
            SecondLastName = secondLastName;
            PhoneNumber = phoneNumber;
            Email = email;
            Status = true;
            Id = id;
            ResetPassword = false;
        }

    }
}

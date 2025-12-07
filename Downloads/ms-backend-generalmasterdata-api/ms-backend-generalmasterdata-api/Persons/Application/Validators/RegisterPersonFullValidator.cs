using AnaPrevention.GeneralMasterData.Api.Common.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Common.Domain.ValueObjects;
using AnaPrevention.GeneralMasterData.Api.Persons.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.Persons.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Persons.Infrastructure.Repositories;
using AnaPrevention.GeneralMasterData.Api.IdentityDocumentTypes.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.IdentityDocumentTypes.Infrastructure.Repositories;
using AnaPrevention.GeneralMasterData.Api.Persons.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using CSharpFunctionalExtensions;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Validators;
using AnaPrevention.GeneralMasterData.Api.GeographicLocations.Infrastructure.Repositories;
using AnaPrevention.GeneralMasterData.Api.GeographicLocations.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.GeographicLocations.Application.Dtos;

namespace AnaPrevention.GeneralMasterData.Api.Persons.Application.Validators
{
    public class RegisterPersonFullValidator : Validator
    {
        private readonly PersonRepository _personRepository;
        private readonly IdentityDocumentTypeRepository _identityDocumentTypeRepository;
        private readonly GenderRepository _genderRepository;
        private readonly MaritalStatusRepository _maritalStatusRepository;
        private readonly DegreeInstructionRepository _degreeInstructionRepository;
        private readonly DistrictRepository _districtRepository;
        private readonly CountryRepository _countryRepository;

        public RegisterPersonFullValidator(
            PersonRepository personRepository,
            IdentityDocumentTypeRepository identityDocumentTypeRepository,
            GenderRepository genderRepository,
            MaritalStatusRepository maritalStatusRepositor,
            DegreeInstructionRepository degreeInstructionRepository,
            DistrictRepository districtRepository,
            CountryRepository countryRepository)
        {
            _personRepository = personRepository;
            _identityDocumentTypeRepository = identityDocumentTypeRepository;
            _genderRepository = genderRepository;
            _maritalStatusRepository = maritalStatusRepositor;
            _degreeInstructionRepository = degreeInstructionRepository;
            _districtRepository = districtRepository;
            _countryRepository = countryRepository;
        }

        public Notification Validate(RegisterPersonFullRequest request)
        {
            Notification notification = new();

            if (request.IdentityDocumentTypeId == Guid.Empty)
                notification.AddError(PersonStatic.IdentityDocumentTypeMsgErrorRequiered);
            if (request.GenderId == Guid.Empty)
                notification.AddError(PersonStatic.GenderIdMsgErrorRequiered);
            if (request.DegreeInstructionId == Guid.Empty)
                notification.AddError(PersonStatic.DegreeInstructionIdMsgErrorRequiered);
            if (request.MaritalStatusId == Guid.Empty)
                notification.AddError(PersonStatic.MaritalStatusIdMsgErrorRequiered);
            if (request.IdentityDocumentTypeId == Guid.Empty)
                notification.AddError(PersonStatic.IdentityDocumentTypeMsgErrorRequiered);
            if (request.IdentityDocumentTypeId == Guid.Empty)
                notification.AddError(PersonStatic.IdentityDocumentTypeMsgErrorRequiered);

            ValidatorString(notification, request.Names, PersonStatic.NamesMaxLength, PersonStatic.NamesMsgErrorMaxLength, PersonStatic.NamesMsgErrorRequiered, true);
            ValidatorString(notification, request.Email, PersonStatic.EmailMaxLength, PersonStatic.EmailMsgErrorMaxLength);
            ValidatorString(notification, request.LastName, PersonStatic.LastNameMaxLength, PersonStatic.LastNameMsgErrorMaxLength, PersonStatic.LastNameMsgErrorRequiered, true);
            ValidatorString(notification, request.SecondLastName, PersonStatic.SecondLastNameMaxLength, PersonStatic.SecondLastNameMsgErrorMaxLength);
            ValidatorString(notification, request.DocumentNumber, PersonStatic.DocumentNumberMaxLength, PersonStatic.DocumentNumberMsgErrorMaxLength, PersonStatic.DocumentNumberMsgErrorRequiered, true);
            ValidatorString(notification, request.PhoneNumber, PersonStatic.PhoneNumberMaxLength, PersonStatic.PhoneNumberMsgErrorMaxLength);
            ValidatorString(notification, request.PersonalPhoneNumber, PersonStatic.PersonalPhoneNumberMaxLength, PersonStatic.PersonalPhoneNumberMsgErrorMaxLength);
            ValidatorString(notification, request.PersonalEmail, PersonStatic.PersonalEmailMaxLength, PersonStatic.PersonalEmailMsgErrorMaxLength);
            ValidatorString(notification, request.SecondDocumentNumber, PersonStatic.SecondDocumentNumberMaxLength, PersonStatic.SecondDocumentNumberMsgErrorMaxLength);
            ValidatorString(notification, request.EmergencyContactName, PersonStatic.EmergencyContactNameMaxLength, PersonStatic.EmergencyContactNameMsgErrorMaxLength);
            ValidatorString(notification, request.EmergencyContactNumberPhone, PersonStatic.EmergencyContactNumberPhoneMaxLength, PersonStatic.EmergencyContactNumberPhoneMsgErrorMaxLength);
            ValidatorString(notification, request.EmergencyContactRelationship, PersonStatic.EmergencyContactRelationshipMaxLength, PersonStatic.EmergencyContactRelationshipMsgErrorMaxLength);
            ValidatorString(notification, request.PersonalAddress, PersonStatic.PersonalAddressMaxLength, PersonStatic.PersonalAddressMsgErrorMaxLength, PersonStatic.PersonalAddressMsgErrorRequiered, true);

            ValidatorString(notification, request.CountryResidenceId, PersonStatic.CountryResidenceMaxLength, PersonStatic.CountryResidenceMsgErrorMaxLength, PersonStatic.CountryResidenceMsgErrorRequiered, true);
            ValidatorString(notification, request.CountryBirthId, PersonStatic.CountryBirthMaxLength, PersonStatic.CountryBirthMsgErrorMaxLength, PersonStatic.CountryBirthMsgErrorRequiered, true);


            if (request.CountryResidenceId == CommonStatic.CountryDefault)
                ValidatorString(notification, request.DistrictResidenceId, PersonStatic.DistrictResidenceMaxLength, PersonStatic.DistrictResidenceMsgErrorMaxLength, PersonStatic.DistrictResidenceMsgErrorRequiered, true);

            if (request.CountryBirthId == CommonStatic.CountryDefault)
                ValidatorString(notification, request.DistrictBirthId, PersonStatic.DistrictBirthMaxLength, PersonStatic.DistrictBirthMsgErrorMaxLength, PersonStatic.DistrictBirthMsgErrorRequiered, true);



            if (!string.IsNullOrWhiteSpace(request.Email))
            {
                Result<Email, Notification> resultEmail = Email.Create(request.Email);
                if (resultEmail.IsFailure)
                    return resultEmail.Error;
            }

            if (!string.IsNullOrWhiteSpace(request.PersonalEmail))
            {
                Result<Email, Notification> resultEmail = Email.Create(request.PersonalEmail);
                if (resultEmail.IsFailure)
                    return resultEmail.Error; ;
            }

            if (!string.IsNullOrWhiteSpace(request.DateBirth))
            {
                Result<Date, Notification> resultDate = Date.Create(request.DateBirth, PersonStatic.DateBirth);
                if (resultDate.IsFailure)
                    return resultDate.Error; ;
            }

            string photo = string.IsNullOrWhiteSpace(request.Photo) ? "" : request.Photo.Trim();
            if (!string.IsNullOrWhiteSpace(photo))
            {
                if (photo.Contains("data:image"))
                {
                    int index = photo.IndexOf('/') + 1;
                    string fileExtension = photo[index..photo.LastIndexOf(';')];
                    photo = photo[(photo.LastIndexOf(',') + 1)..];
                    if (!CommonStatic.ImageFormartAccepted.Contains(fileExtension.ToUpper()))
                        notification.AddError(PersonStatic.PhotoMsgErrorExtension);
                }
                else
                    notification.AddError(PersonStatic.PhotoMsgErrorErrorFormart);

                if (notification.HasErrors())
                    return notification;


                if (!Convert.TryFromBase64String(photo, new(new byte[photo.Length]), out _))
                    notification.AddError(PersonStatic.PhotoMsgErrorErrorBase64);

                if (notification.HasErrors())
                    return notification;

            }


            if (notification.HasErrors())
                return notification;


            IdentityDocumentType? identityDocumentType = _identityDocumentTypeRepository.GetById(request.IdentityDocumentTypeId);
            if (identityDocumentType == null)
                notification.AddError(PersonStatic.IdentityDocumentTypeMsgErrorNotFound);

            if (request.GenderId != null)
            {
                Gender? gender = _genderRepository.GetById((Guid)request.GenderId);
                if (gender == null)
                    notification.AddError(PersonStatic.GenderMsgErrorNotFound);
            }

            if (request.DegreeInstructionId != null)
            {
                DegreeInstruction? degreeInstruction = _degreeInstructionRepository.GetById((Guid)request.DegreeInstructionId);
                if (degreeInstruction == null)
                    notification.AddError(PersonStatic.DegreeInstructionMsgErrorNotFound);
            }

            if (request.MaritalStatusId != null)
            {
                MaritalStatus? maritalStatus = _maritalStatusRepository.GetById((Guid)request.MaritalStatusId);
                if (maritalStatus == null)
                    notification.AddError(PersonStatic.MaritalStatusMsgErrorNotFound);
            }

            if (request.CountryResidenceId != null)
            {
                CountryDto? countryResidence = _countryRepository.GetDtoById(request.CountryResidenceId);
                if (countryResidence == null)
                    notification.AddError(PersonStatic.CountryResidenceMsgErrorNotFound);

                if (request.CountryResidenceId == CommonStatic.CountryDefault)
                {
                    if (request.DistrictResidenceId != null)
                    {
                        DistrictDto? districtResidence = _districtRepository.GetDtoById(request.DistrictResidenceId);
                        if (districtResidence == null)
                            notification.AddError(PersonStatic.DistrictResidenceMsgErrorNotFound);
                    }
                }
            }

            if (request.CountryBirthId != null)
            {
                CountryDto? countryBirth = _countryRepository.GetDtoById(request.CountryBirthId);
                if (countryBirth == null)
                    notification.AddError(PersonStatic.CountryBirthMsgErrorNotFound);

                if (request.CountryBirthId == CommonStatic.CountryDefault)
                {
                    if (request.DistrictBirthId != null)
                    {
                        DistrictDto? districtBirth = _districtRepository.GetDtoById(request.DistrictBirthId);
                        if (districtBirth == null)
                            notification.AddError(PersonStatic.DistrictBirthMsgErrorNotFound);
                    }
                }
            }

            if (request.SecondIdentityDocumentTypeId != null && request.SecondIdentityDocumentTypeId != Guid.Empty)
            {
                identityDocumentType = _identityDocumentTypeRepository.GetById((Guid)request.SecondIdentityDocumentTypeId);
                if (identityDocumentType == null)
                    notification.AddError(PersonStatic.SecondIdentityDocumentTypeMsgErrorNotFound);
            }

            if (notification.HasErrors())
                return notification;

            Person? person = _personRepository.GetbyDocumentNumber(request.DocumentNumber, request.IdentityDocumentTypeId);
            if (person != null)
            {
                if (person.Status == false)
                { notification.AddError(PersonStatic.DocumentNumberMsgErrorDuplicateAndStatusFalse); }
                else
                { notification.AddError(PersonStatic.DocumentNumberMsgErrorDuplicate); }

            }

            if (notification.HasErrors())
                return notification;



            return notification;
        }



    }
}

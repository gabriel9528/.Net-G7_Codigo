using CSharpFunctionalExtensions;
using AnaPrevention.GeneralMasterData.Api.Common.API;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Services;
using AnaPrevention.GeneralMasterData.Api.Common.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Common.Domain.ValueObjects;
using AnaPrevention.GeneralMasterData.Api.Common.Infrastructure.EF;
using AnaPrevention.GeneralMasterData.Api.Persons.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.Persons.Application.Validators;
using AnaPrevention.GeneralMasterData.Api.Persons.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Persons.Infrastructure.Repositories;

using System.Text.Json;

namespace AnaPrevention.GeneralMasterData.Api.Persons.Application.Services
{
    public class PersonApplicationService
    {
        private readonly AnaPreventionContext _context;
        private readonly RegisterPersonValidator _registerPersonValidator;
        private readonly RegisterPersonFullValidator _registerPersonFullValidator;
        private readonly EditPersonValidator _editPersonValidator;
        private readonly EditPersonFullValidator _editPersonFullValidator;
        private readonly PersonRepository _personRepository;
    

        public PersonApplicationService(
       AnaPreventionContext context,
       RegisterPersonValidator registerPersonValidator,
       EditPersonValidator editPersonValidator,
       EditPersonFullValidator editPersonFullValidator,
       PersonRepository personRepository,
       RegisterPersonFullValidator registerPersonFullValidator)
        {
            _context = context;
            _registerPersonValidator = registerPersonValidator;
            _editPersonValidator = editPersonValidator;
            _editPersonFullValidator = editPersonFullValidator;
            _personRepository = personRepository;
            _registerPersonFullValidator = registerPersonFullValidator;
            
        
        }

        public Result<RegisterPersonResponse, Notification> RegisterPerson(RegisterPersonRequest request, Guid userId)
        {
            Notification notification = _registerPersonValidator.Validate(request);

            if (notification.HasErrors())
                return notification;

            string documentNumber = request.DocumentNumber.Trim();
            Guid identityDocumentTypeId = request.IdentityDocumentTypeId;
            string names = request.Names.Trim();
            string lastName = request.LastName.Trim();
            string? secondLastName = request.SecondLastName.Trim();
            string? phoneNumber = request.PhoneNumber.Trim();
            Email email = Email.Create(request.Email).Value;


            Person person = new(documentNumber, identityDocumentTypeId, names, lastName, secondLastName, phoneNumber, email, Guid.NewGuid());

            _personRepository.Save(person);

            _context.SaveChanges(userId);

            var response = new RegisterPersonResponse
            {
                Id = person.Id,
                DocumentNumber = person.DocumentNumber,
                IdentityDocumentTypeId = person.IdentityDocumentTypeId,
                Names = person.Names,
                LastName = person.LastName,
                SecondLastName = string.IsNullOrWhiteSpace(person.SecondLastName) ? "" : person.SecondLastName,
                PhoneNumber = string.IsNullOrWhiteSpace(person.PhoneNumber) ? "" : person.PhoneNumber,
                Email = person.Email is null ? "" : person.Email.Value,
                Status = person.Status,
            };

            return response;
        }

        public async Task<Result<RegisterPersonFullResponse, Notification>> RegisterPersonFull(RegisterPersonFullRequest request, Guid userId)
        {
            Notification notification = _registerPersonFullValidator.Validate(request);

            if (notification.HasErrors())
                return notification;

            string documentNumber = request.DocumentNumber.Trim();
            Guid identityDocumentTypeId = request.IdentityDocumentTypeId;
            string names = request.Names.Trim();
            string lastName = request.LastName.Trim();
            string? secondLastName = request.SecondLastName.Trim();
            string? phoneNumber = request.PhoneNumber.Trim();
            string? personalPhoneNumber = request.PersonalPhoneNumber;
            string? personalAddress = request.PersonalAddress;
            bool dataProcessingAuthorization = request.DataProcessingAuthorization;
            string? secondDocumentNumber = request.SecondDocumentNumber;
            string? emergencyContactName = request.EmergencyContactName;
            string? emergencyContactNumberPhone = request.EmergencyContactNumberPhone;
            string? emergencyContactRelationship = request.EmergencyContactRelationship;
            Guid? genderId = request.GenderId;
            Guid? maritalStatusId = request.MaritalStatusId;
            Guid? degreeInstructionId = request.DegreeInstructionId;
            Guid? secondIdentityDocumentTypeId = request.SecondIdentityDocumentTypeId;


            string? countryBirthId = request.CountryBirthId;
            string? countryResidenceId = request.CountryResidenceId;
            string? districtBirthId = request.DistrictBirthId == "" ? null : request.DistrictBirthId;
            string? districtResidenceId = request.DistrictResidenceId == "" ? null : request.DistrictResidenceId;

            Email? email = null;
            if (!string.IsNullOrWhiteSpace(request.Email))
                email = Email.Create(request.Email).Value;

            Email? personalEmail = null;
            if (!string.IsNullOrWhiteSpace(request.PersonalEmail))
                personalEmail = Email.Create(request.PersonalEmail).Value;


            Date? dateBirth = null;
            if (!string.IsNullOrWhiteSpace(request.DateBirth))
                dateBirth = Date.Create(request.DateBirth).Value;


            string photo = "";
            if (!string.IsNullOrWhiteSpace(request.Photo))
            {
                var _fileApplicationsService = new FileApplicationsService();
                Result<FileDto, Notification> resultPhoto = _fileApplicationsService.ConverterBase64InBytes(request.Photo);

                if (resultPhoto.IsFailure)
                    return resultPhoto.Error;

                using var stream = new MemoryStream(resultPhoto.Value.Bytes);

                //var fileS3 = new S3FileDto
                //{
                //    FileName = "persons/full/" + request.DocumentNumber + "." + resultPhoto.Value.FileExtension,
                //    stream = stream,
                //};

                //S3ResponseDto signUpload = await _s3bucketService.UploadFileAsync(fileS3, CommonStatic.BucketNameFiles);

                //if (signUpload.StatusCode != 200)
                //{
                //    notification.AddError(signUpload.Message);
                //    return notification;
                //}

                if (resultPhoto.Value != null)
                    request.Photo = "persons/full/" + request.DocumentNumber + "." + resultPhoto.Value.FileExtension;
            }

            string? healthInsurance = null;
            if (request.HealthInsurance != null)
            {
                healthInsurance = JsonSerializer.Serialize(request.HealthInsurance);
            }


            Person person = new(documentNumber, identityDocumentTypeId, names, lastName, secondLastName, phoneNumber, email, genderId, dateBirth, personalEmail, personalPhoneNumber, secondDocumentNumber,
                secondIdentityDocumentTypeId, personalAddress, emergencyContactName, emergencyContactNumberPhone, emergencyContactRelationship,
                degreeInstructionId, maritalStatusId, countryResidenceId, countryBirthId, districtResidenceId, districtBirthId, photo, Guid.NewGuid(), dataProcessingAuthorization, healthInsurance);
            _personRepository.Save(person);

            _context.SaveChanges(userId);

            //#region("Create UserExternalPatient")

            //Result<RegisterUserExternalPatientResponse, Notification> userExternal = await _userExternalPatientService.RegisterUserExternalPatient(new RegisterUserExternalPatientRequest()
            //{
            //    DocumentNumber = person.DocumentNumber,
            //    IdentityDocumentTypeId = person.IdentityDocumentTypeId,
            //    Names = person.Names,
            //    LastNames = person.LastName,
            //    Email = person.PersonalEmail?.Value ?? "",
            //    PhoneNumber = person.PhoneNumber,
            //    UserName = person.DocumentNumber,
            //    Password = person.DocumentNumber,
            //    UserExternalType = UserExternalType.PACIENTE,
            //    EnableAppointments = true,
            //    ChangeOnEnter = true,
            //    IsActive = true
            //}, userId);

            //#endregion

            var response = new RegisterPersonFullResponse
            {
                Id = person.Id,
                DocumentNumber = person.DocumentNumber,
                IdentityDocumentTypeId = person.IdentityDocumentTypeId,
                Names = person.Names,
                LastName = person.LastName,
                SecondLastName = string.IsNullOrWhiteSpace(person.SecondLastName) ? "" : person.SecondLastName,
                PhoneNumber = string.IsNullOrWhiteSpace(person.PhoneNumber) ? "" : person.PhoneNumber,
                Email = person.Email is null ? "" : person.Email.Value,
                GenderId = person.GenderId,
                DegreeInstructionId = person.DegreeInstructionId,
                MaritalStatusId = person.MaritalStatusId,
                SecondIdentityDocumentTypeId = person.SecondIdentityDocumentTypeId,
                SecondDocumentNumber = person.SecondDocumentNumber,
                DateBirth = person.DateBirth is null ? "" : person.DateBirth.StringValue,
                PersonalEmail = person.PersonalEmail is null ? "" : person.PersonalEmail.Value,
                PersonalPhoneNumber = string.IsNullOrWhiteSpace(person.PersonalPhoneNumber) ? "" : person.PersonalPhoneNumber,
                EmergencyContactName = person.EmergencyContactName,
                EmergencyContactNumberPhone = person.EmergencyContactNumberPhone,
                EmergencyContactRelationship = person.EmergencyContactRelationship,
                CountryResidenceId = person.CountryResidenceId,
                CountryBirthId = person.CountryBirthId,
                DistrictResidenceId = person.DistrictResidenceId,
                DistrictBirthId = person.DistrictBirthId,
                PersonalAddress = person.PersonalAddress,
                Status = person.Status,
            };

            return await Task.FromResult(response);
        }

        public async Task<Result<EditPersonFullResponse, Notification>> EditPersonFull(EditPersonFullRequest request, Person person, Guid userId)
        {
            person.DocumentNumber = request.DocumentNumber.Trim();
            person.IdentityDocumentTypeId = request.IdentityDocumentTypeId;
            person.Names = request.Names.Trim();
            person.LastName = request.LastName.Trim();
            person.PhoneNumber = request.PhoneNumber.Trim();
            person.SecondLastName = request.SecondLastName.Trim();


            person.PersonalPhoneNumber = request.PersonalPhoneNumber;
            person.PersonalAddress = request.PersonalAddress;
            person.SecondDocumentNumber = request.SecondDocumentNumber;
            person.EmergencyContactName = request.EmergencyContactName;
            person.EmergencyContactNumberPhone = request.EmergencyContactNumberPhone;
            person.EmergencyContactRelationship = request.EmergencyContactRelationship;
            person.GenderId = request.GenderId;
            person.MaritalStatusId = request.MaritalStatusId;
            person.DegreeInstructionId = request.DegreeInstructionId;
            person.SecondIdentityDocumentTypeId = request.SecondIdentityDocumentTypeId;


            person.CountryBirthId = request.CountryBirthId;
            person.CountryResidenceId = request.CountryResidenceId;
            person.DistrictBirthId = request.DistrictBirthId == "" ? null : request.DistrictBirthId;
            person.DistrictResidenceId = request.DistrictResidenceId == "" ? null : request.DistrictResidenceId;

            if (request.DataProcessingAuthorization != null)
                person.DataProcessingAuthorization = (bool)request.DataProcessingAuthorization;


            person.HealthInsuranceJson = JsonSerializer.Serialize(request.HealthInsurance);


            Notification notification = new();


            Email? email = null;
            if (!string.IsNullOrWhiteSpace(request.Email))
                email = Email.Create(request.Email).Value;

            Email? personalEmail = null;
            if (!string.IsNullOrWhiteSpace(request.PersonalEmail))
                personalEmail = Email.Create(request.PersonalEmail).Value;


            Date? dateBirth = null;
            if (!string.IsNullOrWhiteSpace(request.DateBirth))
                dateBirth = Date.Create(request.DateBirth).Value;


            person.PersonalEmail = personalEmail;
            person.Email = email;
            person.DateBirth = dateBirth;
            if (!string.IsNullOrWhiteSpace(request.Photo))
            {
                var _fileApplicationsService = new FileApplicationsService();

                Result<FileDto, Notification> resultPhoto = _fileApplicationsService.ConverterBase64InBytes(request.Photo);

                if (resultPhoto.IsFailure)
                    return resultPhoto.Error;

                using var stream = new MemoryStream(resultPhoto.Value.Bytes);

                //var fileS3 = new S3FileDto
                //{
                //    FileName = "persons/full/" + request.DocumentNumber + "." + resultPhoto.Value.FileExtension,
                //    stream = stream,
                //};

                //S3ResponseDto signUpload = await _s3bucketService.UploadFileAsync(fileS3, CommonStatic.BucketNameFiles);

                //if (signUpload.StatusCode != 200)
                //{
                //    notification.AddError(signUpload.Message);
                //    return notification;
                //}

                person.Photo = "persons/full/" + request.DocumentNumber + "." + resultPhoto.Value.FileExtension;
            }



            _context.SaveChanges(userId);

            var response = new EditPersonFullResponse
            {
                Id = person.Id,
                DocumentNumber = person.DocumentNumber,
                IdentityDocumentTypeId = person.IdentityDocumentTypeId,
                Names = person.Names,
                LastName = person.LastName,
                SecondLastName = string.IsNullOrWhiteSpace(person.SecondLastName) ? "" : person.SecondLastName,
                PhoneNumber = string.IsNullOrWhiteSpace(person.PhoneNumber) ? "" : person.PhoneNumber,
                Email = person.Email is null ? "" : person.Email.Value,
                GenderId = person.GenderId,
                DegreeInstructionId = person.DegreeInstructionId,
                MaritalStatusId = person.MaritalStatusId,
                SecondIdentityDocumentTypeId = person.SecondIdentityDocumentTypeId,
                SecondDocumentNumber = person.SecondDocumentNumber,
                DateBirth = person.DateBirth is null ? "" : person.DateBirth.StringValue,
                PersonalEmail = person.PersonalEmail is null ? "" : person.PersonalEmail.Value,
                PersonalPhoneNumber = string.IsNullOrWhiteSpace(person.PersonalPhoneNumber) ? "" : person.PersonalPhoneNumber,
                EmergencyContactName = person.EmergencyContactName,
                EmergencyContactNumberPhone = person.EmergencyContactNumberPhone,
                EmergencyContactRelationship = person.EmergencyContactRelationship,
                CountryResidenceId = person.CountryResidenceId,
                CountryBirthId = person.CountryBirthId,
                DistrictResidenceId = person.DistrictResidenceId,
                DistrictBirthId = person.DistrictBirthId,
                PersonalAddress = person.PersonalAddress,
                Status = person.Status,
            };

            return await Task.FromResult(response);
        }

        public EditPersonResponse EditPerson(EditPersonRequest request, Person person, Guid userId)
        {
            person.DocumentNumber = request.DocumentNumber.Trim();
            person.IdentityDocumentTypeId = request.IdentityDocumentTypeId;
            person.Names = request.Names.Trim();
            person.LastName = request.LastName.Trim();
            person.PhoneNumber = request.PhoneNumber.Trim();
            person.SecondLastName = request.SecondLastName.Trim();

            if (request.DataProcessingAuthorization != null)
                person.DataProcessingAuthorization = (bool)request.DataProcessingAuthorization;

            Email email = Email.Create(request.Email).Value;
            person.Email = email;
            person.Status = request.Status;

            _context.SaveChanges(userId);

            var response = new EditPersonResponse
            {
                Id = person.Id,
                DocumentNumber = person.DocumentNumber,
                IdentityDocumentTypeId = person.IdentityDocumentTypeId,
                Names = person.Names,
                LastName = person.LastName,
                SecondLastName = string.IsNullOrWhiteSpace(person.SecondLastName) ? "" : person.SecondLastName,
                PhoneNumber = string.IsNullOrWhiteSpace(person.PhoneNumber) ? "" : person.PhoneNumber,
                Email = person.Email is null ? "" : person.Email.Value,
                Status = person.Status
            };

            return response;
        }
        //public async Task<Result<EditPersonResponse, Notification>> ResetPasswordAsync(ResetPasswordPersonRequest request, Person person, Guid userId)
        //{
        //    bool resetPassword = request.resetPassword;
        //    person.ResetPassword = resetPassword;

        //    var userExternalPatient = _userExternalPatientService.GetByUsername(person.DocumentNumber);

        //    if (userExternalPatient == null)
        //    {
        //        Result<RegisterUserExternalPatientResponse, Notification> userExternal = await _userExternalPatientService.RegisterUserExternalPatient(new RegisterUserExternalPatientRequest()
        //        {
        //            DocumentNumber = person.DocumentNumber,
        //            IdentityDocumentTypeId = person.IdentityDocumentTypeId,
        //            Names = person.Names,
        //            LastNames = person.LastName,
        //            Email = person.PersonalEmail?.Value ?? "",
        //            PhoneNumber = person.PhoneNumber,
        //            UserName = person.DocumentNumber,
        //            Password = person.DocumentNumber,
        //            UserExternalType = UserExternalType.PACIENTE,
        //            EnableAppointments = true,
        //            ChangeOnEnter = true,
        //            IsActive = true
        //        }, userId);

        //        if (userExternal.IsFailure)
        //            return userExternal.Error;
        //    }
        //    else
        //    {
        //        Result<Password, Notification> result = Password.CreateResetPassword(person.DocumentNumber);
        //        if (result.IsFailure)
        //            return result.Error;

        //        userExternalPatient.Password = result.Value;
        //    }

        //    _context.SaveChanges(userId);

        //    var response = new EditPersonResponse
        //    {
        //        Id = person.Id,
        //        DocumentNumber = person.DocumentNumber,
        //        IdentityDocumentTypeId = person.IdentityDocumentTypeId,
        //        Names = person.Names,
        //        LastName = person.LastName,
        //        SecondLastName = string.IsNullOrWhiteSpace(person.SecondLastName) ? "" : person.SecondLastName,
        //        PhoneNumber = string.IsNullOrWhiteSpace(person.PhoneNumber) ? "" : person.PhoneNumber,
        //        Email = person.Email is null ? "" : person.Email.Value,
        //        Status = person.Status,
        //        Password = person.DocumentNumber,
        //    };

        //    return response;
        //}
        public EditPersonResponse ActivePerson(Person person, Guid userId)
        {
            person.Status = true;

            _context.SaveChanges(userId);

            var response = new EditPersonResponse
            {
                Id = person.Id,
                DocumentNumber = person.DocumentNumber,
                IdentityDocumentTypeId = person.IdentityDocumentTypeId,
                Names = person.Names,
                LastName = person.LastName,
                SecondLastName = string.IsNullOrWhiteSpace(person.SecondLastName) ? "" : person.SecondLastName,
                PhoneNumber = string.IsNullOrWhiteSpace(person.PhoneNumber) ? "" : person.PhoneNumber,
                Email = person.Email is null ? "" : person.Email.Value,
                Status = person.Status
            };

            return response;
        }
        public Notification ValidateEditPersonRequest(EditPersonRequest request)
        {
            return _editPersonValidator.Validate(request);
        }

        public Notification ValidateEditPersonFullRequest(EditPersonFullRequest request)
        {
            return _editPersonFullValidator.Validate(request);
        }

        public EditPersonResponse RemovePerson(Person person, Guid userId)
        {
            person.Status = false;
            _context.SaveChanges(userId);

            var response = new EditPersonResponse
            {
                Id = person.Id,
                DocumentNumber = person.DocumentNumber,
                IdentityDocumentTypeId = person.IdentityDocumentTypeId,
                Names = person.Names,
                LastName = person.LastName,
                SecondLastName = string.IsNullOrWhiteSpace(person.SecondLastName) ? "" : person.SecondLastName,
                PhoneNumber = string.IsNullOrWhiteSpace(person.PhoneNumber) ? "" : person.PhoneNumber,
                Email = person.Email is null ? "" : person.Email.Value,
                Status = person.Status
            };

            return response;
        }

        public PersonDto? GetbyDtoDocumentNumber(string documentNumber, Guid identityDocumentTypeId)
        {
            return _personRepository.GetbyDtoDocumentNumber(documentNumber, identityDocumentTypeId);
        }
        public List<PersonDto>? GetListAll(string? names)
        {
            return _personRepository.GetListAll(names);
        }

        public Person? GetById(Guid id)
        {
            return _personRepository.GetById(id);
        }

        public PersonDto? GetDtoById(Guid id)
        {
            return _personRepository.GetDtoById(id);
        }
       
   
        public Tuple<IEnumerable<PersonDto>, PaginationMetadata> GetList(int pageNumber, int pageSize, bool status, string namesSearch = "", string documentSearch = "")
        {
            return _personRepository.GetList(pageNumber, pageSize, status, namesSearch, documentSearch);
        }

        
    }
}

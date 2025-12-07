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

namespace AnaPrevention.GeneralMasterData.Api.Persons.Application.Validators
{
    public class RegisterPersonValidator
    {
        private readonly PersonRepository _personRepository;
        private readonly IdentityDocumentTypeRepository _identityDocumentTypeRepository;

        public RegisterPersonValidator(PersonRepository personRepository, IdentityDocumentTypeRepository identityDocumentTypeRepository)
        {
            _personRepository = personRepository;
            _identityDocumentTypeRepository = identityDocumentTypeRepository;
        }

        public Notification Validate(RegisterPersonRequest request)
        {
            Notification notification = new();

            if (request.IdentityDocumentTypeId == Guid.Empty)
                notification.AddError(PersonStatic.IdentityDocumentTypeMsgErrorRequiered);

            string names = string.IsNullOrWhiteSpace(request.Names) ? "" : request.Names.Trim();

            if (string.IsNullOrWhiteSpace(names))
                notification.AddError(PersonStatic.NamesMsgErrorRequiered);

            if (names.Length > PersonStatic.NamesMaxLength)
                notification.AddError(String.Format(PersonStatic.NamesMsgErrorMaxLength, PersonStatic.NamesMaxLength.ToString()));

            string lastName = string.IsNullOrWhiteSpace(request.LastName) ? "" : request.LastName.Trim();

            if (string.IsNullOrWhiteSpace(lastName))
                notification.AddError(PersonStatic.LastNameMsgErrorRequiered);

            if (lastName.Length > PersonStatic.LastNameMaxLength)
                notification.AddError(String.Format(PersonStatic.LastNameMsgErrorMaxLength, PersonStatic.LastNameMaxLength.ToString()));

            string secondLastName = string.IsNullOrWhiteSpace(request.LastName) ? "" : request.LastName.Trim();

            if (secondLastName.Length > PersonStatic.LastNameMaxLength)
                notification.AddError(String.Format(PersonStatic.LastNameMsgErrorMaxLength, PersonStatic.LastNameMaxLength.ToString()));


            string documentNumber = string.IsNullOrWhiteSpace(request.DocumentNumber) ? "" : request.DocumentNumber.Trim();

            if (string.IsNullOrWhiteSpace(documentNumber))
                notification.AddError(PersonStatic.DocumentNumberMsgErrorRequiered);

            if (documentNumber.Length > PersonStatic.DocumentNumberMaxLength)
                notification.AddError(String.Format(PersonStatic.DocumentNumberMsgErrorMaxLength, PersonStatic.DocumentNumberMaxLength.ToString()));

            if (notification.HasErrors())
                return notification;

            IdentityDocumentType? identityDocumentType = _identityDocumentTypeRepository.GetById(request.IdentityDocumentTypeId);

            if (identityDocumentType == null)
                notification.AddError(PersonStatic.IdentityDocumentTypeMsgErrorNotFound);

            if (notification.HasErrors())
                return notification;

            Person? person = _personRepository.GetbyDocumentNumber(request.DocumentNumber, request.IdentityDocumentTypeId);
            if (person != null)
                notification.AddError(PersonStatic.DocumentNumberMsgErrorDuplicate);

            string email = string.IsNullOrWhiteSpace(request.Email) ? "" : request.Email.Trim();
            if (string.IsNullOrWhiteSpace(email))
                notification.AddError(CommonStatic.EmailMsgErrorRequiered);

            if (notification.HasErrors())
                return notification;

            Result<Email, Notification> resultEmail = Email.Create(email);

            if (resultEmail.IsFailure)
                return resultEmail.Error;


            return notification;
        }

    }
}

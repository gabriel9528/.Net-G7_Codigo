using CSharpFunctionalExtensions;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Validators;
using AnaPrevention.GeneralMasterData.Api.Common.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Common.Domain.ValueObjects;
using AnaPrevention.GeneralMasterData.Api.Emails.EmailUsers.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.Emails.EmailUsers.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Emails.EmailUsers.Infrastructure.Repositories;

namespace AnaPrevention.GeneralMasterData.Api.Emails.EmailUsers.Application.Validators
{
    public class EditEmailUserValidator: Validator
    {
        private readonly EmailUserRepository _emailUserRepository;

        public EditEmailUserValidator(EmailUserRepository emailUserRepository)
        {
            _emailUserRepository = emailUserRepository;

        }

        public Notification Validate(EditEmailUserRequest request)
        {
            Notification notification = new();
            ValidatorString(notification, request.Name, CommonStatic.DescriptionMaxLength, EmailUserStatic.NameMsgErrorMaxLength, EmailUserStatic.NameMsgErrorRequiered, true);

            ValidatorString(notification, request.Email, CommonStatic.DescriptionMaxLength, EmailUserStatic.EmailMsgErrorMaxLength, EmailUserStatic.EmailMsgErrorRequiered, true);

            ValidatorString(notification, request.Password, CommonStatic.DescriptionMaxLength, EmailUserStatic.PasswordMsgErrorMaxLength, EmailUserStatic.EmailMsgErrorRequiered, true);

            
            ValidatorString(notification, request.Host, CommonStatic.CodeMaxLength, EmailUserStatic.HostMsgErrorMaxLength, EmailUserStatic.HostMsgErrorRequiered, true);

            if (notification.HasErrors())
            {
                return notification;
            }

            if (!string.IsNullOrWhiteSpace(request.Email))
            {
                Result<Email, Notification> resultEmail = Email.Create(request.Email);
                if (resultEmail.IsFailure)
                    return resultEmail.Error;

                if (_emailUserRepository.EmailTakenForEdit(request.Id, resultEmail.Value))
                    notification.AddError(EmailUserStatic.EmailMsgErrorDuplicate);
            }

            if (_emailUserRepository.NameTakenForEdit(request.Id, request.Name))
                notification.AddError(EmailUserStatic.NameMsgErrorDuplicate);

            return notification;
        }

    }
}

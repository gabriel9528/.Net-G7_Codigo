using CSharpFunctionalExtensions;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Validators;
using AnaPrevention.GeneralMasterData.Api.Common.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Common.Domain.ValueObjects;
using AnaPrevention.GeneralMasterData.Api.Emails.EmailUsers.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.Emails.EmailUsers.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Emails.EmailUsers.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Emails.EmailUsers.Infrastructure.Repositories;

namespace AnaPrevention.GeneralMasterData.Api.Emails.EmailUsers.Application.Validators
{
    public class RegisterEmailUserValidator(EmailUserRepository emailUserRepository) : Validator
    {
        private readonly EmailUserRepository _emailUserRepository = emailUserRepository;

        public Notification Validate(RegisterEmailUserRequest request)
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

            EmailUser? emailUser;

            if (!string.IsNullOrWhiteSpace(request.Email))
            {
                Result<Email, Notification> resultEmail = Email.Create(request.Email);
                if (resultEmail.IsFailure)
                    return resultEmail.Error;

                emailUser = _emailUserRepository.GetbyEmail(resultEmail.Value);
                if (emailUser != null)
                    notification.AddError(EmailUserStatic.EmailMsgErrorDuplicate);
            }

            emailUser = _emailUserRepository.GetbyName(request.Name);
            if (emailUser != null)
                notification.AddError(EmailUserStatic.NameMsgErrorDuplicate);

            return notification;
        }
    }
}

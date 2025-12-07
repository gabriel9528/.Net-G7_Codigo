using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Validators;
using AnaPrevention.GeneralMasterData.Api.Common.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Emails.EmailTemplates.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.Emails.EmailTemplates.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Emails.EmailTemplates.Infrastructure.Repositories;
using AnaPrevention.GeneralMasterData.Api.Emails.EmailUsers.Infrastructure.Repositories;

namespace AnaPrevention.GeneralMasterData.Api.Emails.EmailTemplates.Application.Validators
{
    public class EmailTemplateValidator : Validator
    {
        private readonly EmailTemplateRepository _emailTemplateRepository;
        private readonly EmailUserRepository _emailUserRepository;

        public EmailTemplateValidator(EmailTemplateRepository emailTemplateRepository, EmailUserRepository emailUserRepository)
        {
            _emailTemplateRepository = emailTemplateRepository;
            _emailUserRepository = emailUserRepository;
        }

        public Notification ValidateRegister(EmailTemplateRequest request)
        {
            Notification notification = new();

            ValidatorString(notification, request.Description, CommonStatic.DescriptionMaxLength, EmailTemplateStatic.DescriptionMsgErrorMaxLength, EmailTemplateStatic.DescriptionMsgErrorRequiered, true);
            ValidatorString(notification, request.Subject, CommonStatic.DescriptionMaxLength, EmailTemplateStatic.SubjectMsgErrorMaxLength, EmailTemplateStatic.SubjectMsgErrorRequiered, true);

            if (string.IsNullOrEmpty(request.Body))
            {
                notification.AddError(EmailTemplateStatic.BodyMsgErrorRequiered);
            }

            var emailUser = _emailUserRepository.GetById(request.EmailUserId);

            if (emailUser == null)
            {
                notification.AddError(EmailTemplateStatic.EmailUserMsgErrorRequiered);
            }

            if (notification.HasErrors())
            {
                return notification;
            }

            var emailTemplate = _emailTemplateRepository.GetbyDescription(request.Description);
            if (emailTemplate != null)
                notification.AddError(EmailTemplateStatic.DescriptionMsgErrorDuplicate);


            return notification;
        }

        public Notification ValidateEdit(EditEmailTemplateRequest request)
        {
            Notification notification = new();

            ValidatorString(notification, request.Description, CommonStatic.DescriptionMaxLength, EmailTemplateStatic.DescriptionMsgErrorMaxLength, EmailTemplateStatic.DescriptionMsgErrorRequiered, true);
            ValidatorString(notification, request.Subject, CommonStatic.DescriptionMaxLength, EmailTemplateStatic.SubjectMsgErrorMaxLength, EmailTemplateStatic.SubjectMsgErrorRequiered, true);

            if (string.IsNullOrEmpty(request.Body))
            {
                notification.AddError(EmailTemplateStatic.BodyMsgErrorRequiered);
            }

            var emailUser = _emailUserRepository.GetById(request.EmailUserId);

            if (emailUser == null)
            {
                notification.AddError(EmailTemplateStatic.EmailUserMsgErrorRequiered);
            }

            if (notification.HasErrors())
            {
                return notification;
            }

            var TakenEmailTemplate = _emailTemplateRepository.NameTakenForEdit(request.Id, request.Description);
            if (TakenEmailTemplate)
                notification.AddError(EmailTemplateStatic.DescriptionMsgErrorDuplicate);
            return notification;
        }
    }
}

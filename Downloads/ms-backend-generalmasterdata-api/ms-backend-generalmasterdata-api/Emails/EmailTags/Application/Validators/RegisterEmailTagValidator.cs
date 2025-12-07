using AnaPrevention.GeneralMasterData.Api.Common.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Validators;
using AnaPrevention.GeneralMasterData.Api.Emails.EmailTags.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.Emails.EmailTags.Infrastructure.Repositories;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Emails.EmailTags.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Emails.EmailTags.Domain.Entities;

namespace AnaPrevention.GeneralMasterData.Api.Emails.EmailTags.Application.Validators
{
    public class RegisterEmailTagValidator(EmailTagRepository emailTagRepository) : Validator
    {
        private readonly EmailTagRepository _emailTagRepository = emailTagRepository;

        public Notification ValidateRegister(RegisterEmailTagRequest request)
        {
            Notification notification = new();

            ValidatorString(notification, request.Description, CommonStatic.DescriptionMaxLength, EmailTagStatic.DescriptionMsgErrorMaxLength, EmailTagStatic.DescriptionMsgErrorRequiered, true);

            ValidatorString(notification, request.Tag, CommonStatic.DescriptionMaxLength, EmailTagStatic.TagMsgErrorMaxLength, EmailTagStatic.TagMsgErrorRequiered, true);


            if (notification.HasErrors())
            {
                return notification;
            }

            EmailTag? emailTag;


            emailTag = _emailTagRepository.GetbyDescription(request.Description, request.EmailTagTemplateType);
            if (emailTag != null)
                notification.AddError(EmailTagStatic.DescriptionMsgErrorDuplicate);


            emailTag = _emailTagRepository.GetbyTag(request.Tag, request.EmailTagTemplateType);
            if (emailTag != null)
                notification.AddError(EmailTagStatic.TagMsgErrorDuplicate);

            return notification;
        }

        public Notification ValidateEdit(EditEmailTagRequest request)
        {
            Notification notification = new();

            ValidatorString(notification, request.Description, CommonStatic.DescriptionMaxLength, EmailTagStatic.DescriptionMsgErrorMaxLength, EmailTagStatic.DescriptionMsgErrorRequiered, true);

            ValidatorString(notification, request.Tag, CommonStatic.DescriptionMaxLength, EmailTagStatic.TagMsgErrorMaxLength, EmailTagStatic.TagMsgErrorRequiered, true);


            if (notification.HasErrors())
            {
                return notification;
            }

            if (_emailTagRepository.DescriptionTakenForEdit(request.Id, request.Description, request.EmailTagTemplateType))
                notification.AddError(EmailTagStatic.DescriptionMsgErrorDuplicate);

            if (_emailTagRepository.TagTakenForEdit(request.Id, request.Tag, request.EmailTagTemplateType))
                notification.AddError(EmailTagStatic.TagMsgErrorDuplicate);

            return notification;
        }
    }
}

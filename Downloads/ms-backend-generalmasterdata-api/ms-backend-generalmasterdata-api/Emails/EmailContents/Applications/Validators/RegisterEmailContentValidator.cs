using CSharpFunctionalExtensions;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Validators;
using AnaPrevention.GeneralMasterData.Api.Common.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Common.Domain.ValueObjects;
using AnaPrevention.GeneralMasterData.Api.Emails.EmailContents.Applications.Dtos;
using AnaPrevention.GeneralMasterData.Api.Emails.EmailContents.Applications.Static;
using AnaPrevention.GeneralMasterData.Api.Emails.EmailContents.Infraestructure.Repository;

namespace AnaPrevention.GeneralMasterData.Api.Emails.EmailContents.Applications.Validators
{

    public class RegisterEmailContentValidator : Validator
    {
        private readonly EmailContentRepository _emailContentRepository;
        
        public RegisterEmailContentValidator(EmailContentRepository emailContentRepository)
        {
            _emailContentRepository = emailContentRepository;
        }

        public Notification ValidateRegister(RegisterEmailContentRequest request)
        {
            Notification notification = new();

            if (request.ToAddress == null)
            {
                notification.AddError(EmailContentStatic.ToAddressMsgErrorRequiered);
            }

            if (request.FromAddress == null)
            {
                notification.AddError(EmailContentStatic.FromAddressMsgErrorRequiered);
            }

            if (notification.HasErrors())
            {
                return notification;
            }

            ValidatorString(notification, request.ToAddress, CommonStatic.EmailContentMaxLength, EmailContentStatic.EmailContentMsgErrorMaxLength, EmailContentStatic.NameMsgErrorRequiered, true);
            ValidatorString(notification, request.FromAddress, CommonStatic.EmailContentMaxLength, EmailContentStatic.EmailContentMsgErrorMaxLength, EmailContentStatic.NameMsgErrorRequiered, true);

            if (notification.HasErrors())
            {
                return notification;
            }

            Result<Email, Notification> resultFromEmail = Email.Create(request.FromAddress!);

            if (resultFromEmail.IsFailure)
                return resultFromEmail.Error;

            Result<Email, Notification> resultToEmail = Email.Create(request.ToAddress!);

            if (resultToEmail.IsFailure)
                return resultToEmail.Error;

            return notification;
        }
        
        public Notification ValidateRegister(RegisterEmailTemplateRequest request)
        {
            Notification notification = new();
            if(request.OccupationalHealthId == null)
                notification.AddError(EmailContentStatic.OccupationalHealthIdMsgErrorRequiered);

            if (request.ToAddress?.Address == null)            
                notification.AddError(EmailContentStatic.ToAddressMsgErrorRequiered);                 
            
            if(request.ToAddress?.Name == null)            
                notification.AddError(EmailContentStatic.ToAddressMsgErrorRequiered);
                    
            if (notification.HasErrors())            
                return notification;            

            ValidatorString(notification, request.ToAddress!.Address, CommonStatic.EmailContentMaxLength, EmailContentStatic.EmailContentMsgErrorMaxLength, EmailContentStatic.NameMsgErrorRequiered, true);            

            if (notification.HasErrors())            
                return notification;                      
            
            Result<Email, Notification> resultToEmail = Email.Create(request.ToAddress.Address!);
            
            if (resultToEmail.IsFailure)
                return resultToEmail.Error;
            
            return notification;
        }
    }
}

using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Validators;
using AnaPrevention.GeneralMasterData.Api.Common.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.CreditTimes.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.CreditTimes.Application.Static;
using AnaPrevention.GeneralMasterData.Api.CreditTimes.Infrastructure.Repositories;

namespace AnaPrevention.GeneralMasterData.Api.CreditTimes.Application.Validators
{
    public class EditCreditTimeValidator: Validator
    {
        private readonly CreditTimeRepository _creditTimeRepository;

        public EditCreditTimeValidator(CreditTimeRepository creditTimeRepository)
        {
            _creditTimeRepository = creditTimeRepository;
        }

        public Notification Validate(EditCreditTimeRequest request)
        {
            Notification notification = new();

            if (request.NumberDay < 0)
                notification.AddError(CreditTimeStatic.NumberDayMsgErrorFormat);

            if (request.Id == Guid.Empty)
                notification.AddError(CommonStatic.IdMsgErrorRequiered);

            ValidatorString(notification, request.Description, CommonStatic.DescriptionMaxLength, CommonStatic.DescriptionMsgErrorMaxLength, CommonStatic.DescriptionMsgErrorRequiered, true);
            ValidatorString(notification, request.Code, CommonStatic.CodeMaxLength, CommonStatic.CodeMsgErrorMaxLength, CommonStatic.CodeMsgErrorRequiered, true);

            if (notification.HasErrors())
            {
                return notification;
            }

            bool descriptionTakenForEdit = _creditTimeRepository.DescriptionTakenForEdit(request.Id, request.Description);

            if (descriptionTakenForEdit)
                notification.AddError(CommonStatic.DescriptionMsgErrorDuplicate);

            bool CodeTakenForEdit = _creditTimeRepository.CodeTakenForEdit(request.Id, request.Code);

            if (CodeTakenForEdit)
                notification.AddError(CommonStatic.CodeMsgErrorDuplicate);


            return notification;
        }
    }
}

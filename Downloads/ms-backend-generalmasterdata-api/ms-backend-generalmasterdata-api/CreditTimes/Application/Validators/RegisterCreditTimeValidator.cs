using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Validators;
using AnaPrevention.GeneralMasterData.Api.Common.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.CreditTimes.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.CreditTimes.Application.Static;
using AnaPrevention.GeneralMasterData.Api.CreditTimes.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.CreditTimes.Infrastructure.Repositories;

namespace AnaPrevention.GeneralMasterData.Api.CreditTimes.Application.Validators
{
    public class RegisterCreditTimeValidator: Validator
    {
        private readonly CreditTimeRepository _creditTimeRepository;

        public RegisterCreditTimeValidator(CreditTimeRepository creditTimeRepository)
        {
            _creditTimeRepository = creditTimeRepository;
        }

        public Notification Validate(RegisterCreditTimeRequest request)
        {
            Notification notification = new();

            if (request.NumberDay < 0)
                notification.AddError(CreditTimeStatic.NumberDayMsgErrorFormat);

            ValidatorString(notification, request.Description, CommonStatic.DescriptionMaxLength, CommonStatic.DescriptionMsgErrorMaxLength, CommonStatic.DescriptionMsgErrorRequiered, true);
            ValidatorString(notification, request.Code, CommonStatic.CodeMaxLength, CommonStatic.CodeMsgErrorMaxLength, CommonStatic.CodeMsgErrorRequiered, true);

            if (notification.HasErrors())
            {
                return notification;
            }


            CreditTime? creditTime = _creditTimeRepository.GetbyDescription(request.Description);
            //if (creditTime != null)
            //    notification.AddError(CommonStatic.DescriptionMsgErrorDuplicate);

             creditTime = _creditTimeRepository.GetbyCode(request.Code);
            if (creditTime != null)
                notification.AddError(CommonStatic.CodeMsgErrorDuplicate);

            return notification;
        }
    }
}

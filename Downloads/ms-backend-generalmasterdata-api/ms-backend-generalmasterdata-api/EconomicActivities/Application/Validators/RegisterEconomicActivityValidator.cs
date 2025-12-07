using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Validators;
using AnaPrevention.GeneralMasterData.Api.Common.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.EconomicActivities.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.EconomicActivities.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.EconomicActivities.Infrastructure.Repositories;

namespace AnaPrevention.GeneralMasterData.Api.EconomicActivities.Application.Validators
{
    public class RegisterEconomicActivityValidator: Validator
    {
        private readonly EconomicActivityRepository _economicActivityRepository;

        public RegisterEconomicActivityValidator(EconomicActivityRepository economicActivityRepository)
        {
            _economicActivityRepository = economicActivityRepository;
        }

        public Notification Validate(RegisterEconomicActivityRequest request)
        {
            Notification notification = new();

            ValidatorString(notification, request.Description, CommonStatic.DescriptionMaxLength, CommonStatic.DescriptionMsgErrorMaxLength, CommonStatic.DescriptionMsgErrorRequiered, true);
            


            if (notification.HasErrors())
            {
                return notification;
            }


            EconomicActivity? economicActivity = _economicActivityRepository.GetbyDescription(request.Description);
            if (economicActivity != null)
                notification.AddError(CommonStatic.DescriptionMsgErrorDuplicate);

             economicActivity = _economicActivityRepository.GetbyCode(request.Code);
            if (economicActivity != null)
                notification.AddError(CommonStatic.CodeMsgErrorDuplicate);

            return notification;
        }
    }
}

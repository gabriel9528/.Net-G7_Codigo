using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Validators;
using AnaPrevention.GeneralMasterData.Api.Common.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.EconomicActivities.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.EconomicActivities.Infrastructure.Repositories;

namespace AnaPrevention.GeneralMasterData.Api.EconomicActivities.Application.Validators
{
    public class EditEconomicActivityValidator: Validator
    {
        private readonly EconomicActivityRepository _economicActivityRepository;

        public EditEconomicActivityValidator(EconomicActivityRepository economicActivityRepository)
        {
            _economicActivityRepository = economicActivityRepository;
        }

        public Notification Validate(EditEconomicActivityRequest request)
        {
            Notification notification = new();

            if (request.Id == Guid.Empty)
                notification.AddError(CommonStatic.IdMsgErrorRequiered);

            ValidatorString(notification, request.Description, CommonStatic.DescriptionMaxLength, CommonStatic.DescriptionMsgErrorMaxLength, CommonStatic.DescriptionMsgErrorRequiered, true);
            ValidatorString(notification, request.Code, CommonStatic.CodeMaxLength, CommonStatic.CodeMsgErrorMaxLength, CommonStatic.CodeMsgErrorRequiered, true);

            if (notification.HasErrors())
            {
                return notification;
            }

            bool descriptionTakenForEdit = _economicActivityRepository.DescriptionTakenForEdit(request.Id, request.Description);

            if (descriptionTakenForEdit)
                notification.AddError(CommonStatic.DescriptionMsgErrorDuplicate);

            bool CodeTakenForEdit = _economicActivityRepository.CodeTakenForEdit(request.Id, request.Code);

            if (CodeTakenForEdit)
                notification.AddError(CommonStatic.CodeMsgErrorDuplicate);


            return notification;
        }
    }
}

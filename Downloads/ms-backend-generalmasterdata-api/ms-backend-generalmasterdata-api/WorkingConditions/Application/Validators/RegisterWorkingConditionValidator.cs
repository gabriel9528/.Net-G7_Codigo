using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Validators;
using AnaPrevention.GeneralMasterData.Api.Common.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.WorkingConditions.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.WorkingConditions.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.WorkingConditions.Infrastructure.Repositories;

namespace AnaPrevention.GeneralMasterData.Api.WorkingConditions.Application.Validators
{
    public class RegisterWorkingConditionValidator:Validator
    {
        private readonly WorkingConditionRepository _workingConditionRepository;

        public RegisterWorkingConditionValidator(WorkingConditionRepository workingConditionRepository)
        {
            _workingConditionRepository = workingConditionRepository;
        }

        public Notification Validate(RegisterWorkingConditionRequest request)
        {
            Notification notification = new();

            ValidatorString(notification, request.Description, CommonStatic.DescriptionMaxLength, CommonStatic.DescriptionMsgErrorMaxLength, CommonStatic.DescriptionMsgErrorRequiered, true);



            if (notification.HasErrors())
            {
                return notification;
            }


            WorkingCondition? workingCondition = _workingConditionRepository.GetbyDescription(request.Description);
            if (workingCondition != null)
                notification.AddError(CommonStatic.DescriptionMsgErrorDuplicate);

             workingCondition = _workingConditionRepository.GetbyCode(request.Code);
            if (workingCondition != null)
                notification.AddError(CommonStatic.CodeMsgErrorDuplicate);

            return notification;
        }
    }
}

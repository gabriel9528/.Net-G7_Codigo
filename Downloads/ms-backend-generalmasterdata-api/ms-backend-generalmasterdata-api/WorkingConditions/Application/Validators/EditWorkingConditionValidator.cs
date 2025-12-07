using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Validators;
using AnaPrevention.GeneralMasterData.Api.Common.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.WorkingConditions.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.WorkingConditions.Application.Static;
using AnaPrevention.GeneralMasterData.Api.WorkingConditions.Infrastructure.Repositories;

namespace AnaPrevention.GeneralMasterData.Api.WorkingConditions.Application.Validators
{
    public class EditWorkingConditionValidator :Validator
    {
        private readonly WorkingConditionRepository _workingConditionRepository;

        public EditWorkingConditionValidator(WorkingConditionRepository workingConditionRepository)
        {
            _workingConditionRepository = workingConditionRepository;
        }

        public Notification Validate(EditWorkingConditionRequest request)
        {
            Notification notification = new();

            if (request.Id == Guid.Empty)
                notification.AddError(CommonStatic.IdMsgErrorRequiered);

            ValidatorString(notification, request.Description, CommonStatic.DescriptionMaxLength, CommonStatic.DescriptionMsgErrorMaxLength, CommonStatic.DescriptionMsgErrorRequiered, true);


            if (notification.HasErrors())
            {
                return notification;
            }

            bool descriptionTakenForEdit = _workingConditionRepository.DescriptionTakenForEdit(request.Id, request.Description);

            if (descriptionTakenForEdit)
                notification.AddError(CommonStatic.DescriptionMsgErrorDuplicate);

            bool CodeTakenForEdit = _workingConditionRepository.CodeTakenForEdit(request.Id, request.Code);

            if (CodeTakenForEdit)
                notification.AddError(CommonStatic.CodeMsgErrorDuplicate);


            return notification;
        }
    }
}

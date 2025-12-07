using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Validators;
using AnaPrevention.GeneralMasterData.Api.Common.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Dimensions.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.Dimensions.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Dimensions.Infrastructure.Repositories;

namespace AnaPrevention.GeneralMasterData.Api.CostCenters.Application.Validators
{
    public class EditCostCenterValidator : Validator
    {
        private readonly CostCenterRepository _CostCenterRepository;

        public EditCostCenterValidator(CostCenterRepository costCenterRepository)
        {
            _CostCenterRepository = costCenterRepository;
        }

        public Notification Validate(EditCostCenterRequest request)
        {
            Notification notification = new();

            if (request.Id == Guid.Empty)
            {
                notification.AddError(CostCenterStatic.IdMsgErrorRequiered);
                return notification;
            }

            ValidatorString(notification, request.Description, CostCenterStatic.DescriptionMaxLength, CostCenterStatic.DescriptionMsgErrorMaxLength, CostCenterStatic.DescriptionMsgErrorRequiered, true);
            ValidatorString(notification, request.Code, CommonStatic.CodeMaxLength, CommonStatic.CodeMsgErrorMaxLength, CommonStatic.CodeMsgErrorRequiered, true);


            if (notification.HasErrors())
            {
                return notification;
            }

            bool descriptionTakenForEdit = _CostCenterRepository.DescriptionTakenForEdit((Guid)request.Id, request.Description);

            if (descriptionTakenForEdit)
                notification.AddError(CostCenterStatic.DescriptionMsgErrorDuplicate);

            bool codeTakenForEdit = _CostCenterRepository.CodeTakenForEdit((Guid)request.Id, request.Code);

            if (codeTakenForEdit)
                notification.AddError(CostCenterStatic.CodeMsgErrorDuplicate);

            return notification;
        }
    }
}

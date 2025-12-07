using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Validators;
using AnaPrevention.GeneralMasterData.Api.Common.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Dimensions.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.Dimensions.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Dimensions.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Dimensions.Infrastructure.Repositories;

namespace AnaPrevention.GeneralMasterData.Api.Dimensions.Application.Validators
{
    public class RegisterCostCenterValidator : Validator
    {
        private readonly CostCenterRepository _costCenterRepository;

        public RegisterCostCenterValidator(CostCenterRepository costCenterRepository)
        {
            _costCenterRepository = costCenterRepository;
        }

        public Notification Validate(RegisterCostCenterRequest request)
        {
            Notification notification = new();

            ValidatorString(notification, request.Description, CommonStatic.DescriptionMaxLength, CostCenterStatic.DescriptionMsgErrorMaxLength, CostCenterStatic.DescriptionMsgErrorRequiered, true);
            ValidatorString(notification, request.Code, CommonStatic.CodeMaxLength, CostCenterStatic.CodeMsgErrorMaxLength, CostCenterStatic.CodeMsgErrorRequiered, true);



            if (notification.HasErrors())
            {
                return notification;
            }

            CostCenter? costCenter = _costCenterRepository.GetbyDescription(request.Description);
            if (costCenter != null)
                notification.AddError(CostCenterStatic.DescriptionMsgErrorDuplicate);

            costCenter = _costCenterRepository.GetbyCode(request.Code);
            if (costCenter != null)
                notification.AddError(CostCenterStatic.CodeMsgErrorDuplicate);

            return notification;
        }
    }
}

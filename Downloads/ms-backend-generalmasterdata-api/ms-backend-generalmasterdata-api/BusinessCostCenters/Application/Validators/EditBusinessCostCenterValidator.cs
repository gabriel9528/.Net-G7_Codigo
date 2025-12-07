using AnaPrevention.GeneralMasterData.Api.Common.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.BusinessCostCenters.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.BusinessCostCenters.Application.Static;
using AnaPrevention.GeneralMasterData.Api.BusinessCostCenters.Infrastructure.Repositories;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Validators;

namespace AnaPrevention.GeneralMasterData.Api.BusinessCostCenters.Application.Validators
{
    public class EditBusinessCostCenterValidator : Validator
    {
        private readonly BusinessCostCenterRepository _businessCostCenterRepository;

        public EditBusinessCostCenterValidator(BusinessCostCenterRepository businessCostCenterRepository)
        {
            _businessCostCenterRepository = businessCostCenterRepository;
        }

        public Notification Validate(EditBusinessCostCenterRequest request)
        {
            Notification notification = new();

            if (request.Id == Guid.Empty)
                notification.AddError(CommonStatic.IdMsgErrorRequiered);

            ValidatorString(notification, request.Description, CommonStatic.DescriptionMaxLength, CommonStatic.DescriptionMsgErrorMaxLength, CommonStatic.DescriptionMsgErrorRequiered, true);


            if (notification.HasErrors())
            {
                return notification;
            }

            bool descriptionTakenForEdit = _businessCostCenterRepository.DescriptionTakenForEdit(request.Id, request.Description);

            if (descriptionTakenForEdit)
                notification.AddError(CommonStatic.DescriptionMsgErrorDuplicate);


            return notification;
        }
    }
}

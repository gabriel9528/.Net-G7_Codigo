using AnaPrevention.GeneralMasterData.Api.BusinessCostCenters.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.BusinessCostCenters.Application.Static;
using AnaPrevention.GeneralMasterData.Api.BusinessCostCenters.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.BusinessCostCenters.Infrastructure.Repositories;
using AnaPrevention.GeneralMasterData.Api.Businesses.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Businesses.Infrastructure.Repositories;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Validators;
using AnaPrevention.GeneralMasterData.Api.Common.Domain.Entities;

namespace AnaPrevention.GeneralMasterData.Api.BusinessCostCenters.Application.Validators
{
    public class RegisterListBusinessCostCenterValidator : Validator
    {
        private readonly BusinessCostCenterRepository _businessCostCenterRepository;
        private readonly BusinessRepository _businessRepository;

        public RegisterListBusinessCostCenterValidator(
            BusinessCostCenterRepository businessCostCenterRepository,
            BusinessRepository businessRepositor)
        {
            _businessCostCenterRepository = businessCostCenterRepository;
            _businessRepository = businessRepositor;
        }
        public Notification Validate(RegisterListBusinessCostCenterRequest request)
        {
            Notification notification = new();


            Business? business = _businessRepository.GetById(request.BusinessId);
            if (business == null)
                notification.AddError(BusinessCostCenterStatic.BusinessIdMsgErrorNotFound);

            if (notification.HasErrors())
                return notification;
            foreach (string Description in request.ListDescription)
            {

                ValidatorString(notification, Description, CommonStatic.DescriptionMaxLength, CommonStatic.DescriptionMsgErrorMaxLength, CommonStatic.DescriptionMsgErrorRequiered, true);
                if (notification.HasErrors())
                    return notification;

                BusinessCostCenter? businessCostCenter = _businessCostCenterRepository.GetbyDescription(Description, request.BusinessId);
                if (businessCostCenter != null)
                    notification.AddError(String.Format(BusinessCostCenterStatic.ListDescriptionMsgErrorDuplicate, Description));
            }
            return notification;
        }
    }
}


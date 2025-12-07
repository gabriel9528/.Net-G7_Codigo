using AnaPrevention.GeneralMasterData.Api.Common.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.BusinessCostCenters.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.BusinessCostCenters.Application.Static;
using AnaPrevention.GeneralMasterData.Api.BusinessCostCenters.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.BusinessCostCenters.Infrastructure.Repositories;
using AnaPrevention.GeneralMasterData.Api.Businesses.Infrastructure.Repositories;
using AnaPrevention.GeneralMasterData.Api.Businesses.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Validators;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;

namespace AnaPrevention.GeneralMasterData.Api.BusinessCostCenters.Application.Validators
{
    public class RegisterBusinessCostCenterValidator : Validator
    {
        private readonly BusinessCostCenterRepository _businessCostCenterRepository;
        private readonly BusinessRepository _businessRepository;

        public RegisterBusinessCostCenterValidator(
            BusinessCostCenterRepository businessCostCenterRepository,
            BusinessRepository businessRepositor)
        {
            _businessCostCenterRepository = businessCostCenterRepository;
            _businessRepository = businessRepositor;
        }

        public Notification Validate(RegisterBusinessCostCenterRequest request)
        {
            Notification notification = new();


            ValidatorString(notification, request.Description, CommonStatic.DescriptionMaxLength, CommonStatic.DescriptionMsgErrorMaxLength, CommonStatic.DescriptionMsgErrorRequiered, true);

            if (notification.HasErrors())
            {
                return notification;
            }

            BusinessCostCenter? businessCostCenter = _businessCostCenterRepository.GetbyDescription(request.Description, request.BusinessId);
            if (businessCostCenter != null)
                notification.AddError(CommonStatic.DescriptionMsgErrorDuplicate);

            Business? business = _businessRepository.GetById(request.BusinessId);
            if (business == null)
                notification.AddError(BusinessCostCenterStatic.BusinessIdMsgErrorNotFound);

            return notification;
        }
    }
}

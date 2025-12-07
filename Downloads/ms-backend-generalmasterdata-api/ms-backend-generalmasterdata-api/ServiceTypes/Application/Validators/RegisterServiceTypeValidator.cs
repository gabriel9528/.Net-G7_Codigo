using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Validators;
using AnaPrevention.GeneralMasterData.Api.Common.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.ServiceTypes.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.ServiceTypes.Application.Static;
using AnaPrevention.GeneralMasterData.Api.ServiceTypes.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.ServiceTypes.Domain.Enums;
using AnaPrevention.GeneralMasterData.Api.ServiceTypes.Infrastructure.Repositories;

namespace AnaPrevention.GeneralMasterData.Api.ServiceTypes.Application.Validators
{
    public class RegisterServiceTypeValidator : Validator
    {

        private readonly ServiceTypeRepository _serviceTypeRepository;

        public RegisterServiceTypeValidator(ServiceTypeRepository serviceTypeRepository)
        {
            _serviceTypeRepository = serviceTypeRepository;
        }

        public Notification Validate(RegisterServiceTypeRequest request, Guid companyId)
        {
            Notification notification = new();

            ValidatorString(notification, request.Description, CommonStatic.DescriptionMaxLength, CommonStatic.DescriptionMsgErrorMaxLength, CommonStatic.DescriptionMsgErrorRequiered, true);

            if (request.Code is not ServiceTypeEnum.ASASSISTENTIAL and ServiceTypeEnum.OCCUPATIONAL)
            {
                notification.AddError(ServiceTypeStatic.CodeMsgErrorFormat);
            }

            if (notification.HasErrors())
            {
                return notification;
            }

            ServiceType? serviceType = _serviceTypeRepository.GetbyDescription(request.Description, companyId);
            if (serviceType != null)
                notification.AddError(CommonStatic.DescriptionMsgErrorDuplicate);

            serviceType = _serviceTypeRepository.GetbyCode(request.Code, companyId);
            if (serviceType != null)
                notification.AddError(CommonStatic.CodeMsgErrorDuplicate);

            return notification;
        }
    }
}



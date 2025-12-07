using AnaPrevention.GeneralMasterData.Api.Common.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.BusinessAreas.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.BusinessAreas.Application.Static;
using AnaPrevention.GeneralMasterData.Api.BusinessAreas.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.BusinessAreas.Infrastructure.Repositories;
using AnaPrevention.GeneralMasterData.Api.Businesses.Infrastructure.Repositories;
using AnaPrevention.GeneralMasterData.Api.Businesses.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Validators;

namespace AnaPrevention.GeneralMasterData.Api.BusinessAreas.Application.Validators
{
    public class RegisterBusinessAreaValidator : Validator
    {
        private readonly BusinessAreaRepository _businessAreaRepository;
        private readonly BusinessRepository _businessRepository;

        public RegisterBusinessAreaValidator(
            BusinessAreaRepository businessAreaRepository,
            BusinessRepository businessRepositor)
        {
            _businessAreaRepository = businessAreaRepository;
            _businessRepository = businessRepositor;
        }

        public Notification Validate(RegisterBusinessAreaRequest request)
        {
            Notification notification = new();


            ValidatorString(notification, request.Description, CommonStatic.DescriptionMaxLength, CommonStatic.DescriptionMsgErrorMaxLength, CommonStatic.DescriptionMsgErrorRequiered, true);


            if (notification.HasErrors())
                return notification;

            BusinessArea? businessArea = _businessAreaRepository.GetbyDescription(request.Description, request.BusinessId);
            if (businessArea != null)
                notification.AddError(CommonStatic.DescriptionMsgErrorDuplicate);

            Business? business = _businessRepository.GetById(request.BusinessId);
            if (business == null)
                notification.AddError(BusinessAreaStatic.BusinessIdMsgErrorNotFound);

            return notification;
        }
    }
}

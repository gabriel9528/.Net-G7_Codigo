using AnaPrevention.GeneralMasterData.Api.Common.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.BusinessProfiles.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.BusinessProfiles.Application.Static;
using AnaPrevention.GeneralMasterData.Api.BusinessProfiles.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.BusinessProfiles.Infrastructure.Repositories;
using AnaPrevention.GeneralMasterData.Api.Businesses.Infrastructure.Repositories;
using AnaPrevention.GeneralMasterData.Api.Businesses.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Validators;

namespace AnaPrevention.GeneralMasterData.Api.BusinessProfiles.Application.Validators
{
    public class RegisterBusinessProfileValidator : Validator
    {
        private readonly BusinessProfileRepository _businessProfileRepository;
        private readonly BusinessRepository _businessRepository;

        public RegisterBusinessProfileValidator(
            BusinessProfileRepository businessProfileRepository,
            BusinessRepository businessRepositor)
        {
            _businessProfileRepository = businessProfileRepository;
            _businessRepository = businessRepositor;
        }

        public Notification Validate(RegisterBusinessProfileRequest request)
        {
            Notification notification = new();


            ValidatorString(notification, request.Description, CommonStatic.DescriptionMaxLength, CommonStatic.DescriptionMsgErrorMaxLength, CommonStatic.DescriptionMsgErrorRequiered, true);

            if (notification.HasErrors())
            {
                return notification;
            }

            BusinessProfile? businessProfile = _businessProfileRepository.GetbyDescription(request.Description, request.BusinessId);
            if (businessProfile != null)
                notification.AddError(CommonStatic.DescriptionMsgErrorDuplicate);

            Business? business = _businessRepository.GetById(request.BusinessId);
            if (business == null)
                notification.AddError(BusinessProfileStatic.BusinessIdMsgErrorNotFound);

            return notification;
        }
    }
}

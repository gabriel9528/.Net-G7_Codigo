using AnaPrevention.GeneralMasterData.Api.BusinessProfiles.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.BusinessProfiles.Application.Static;
using AnaPrevention.GeneralMasterData.Api.BusinessProfiles.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.BusinessProfiles.Infrastructure.Repositories;
using AnaPrevention.GeneralMasterData.Api.Businesses.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Businesses.Infrastructure.Repositories;
using AnaPrevention.GeneralMasterData.Api.Common.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Validators;

namespace AnaPrevention.GeneralMasterData.Api.BusinessProfiles.Application.Validators
{
    public class RegisterListBusinessProfileValidator : Validator
    {
        private readonly BusinessProfileRepository _businessProfileRepository;
        private readonly BusinessRepository _businessRepository;

        public RegisterListBusinessProfileValidator(
            BusinessProfileRepository businessProfileRepository,
            BusinessRepository businessRepositor)
        {
            _businessProfileRepository = businessProfileRepository;
            _businessRepository = businessRepositor;
        }
        public Notification Validate(RegisterListBusinessProfileRequest request)
        {
            Notification notification = new();


            Business? business = _businessRepository.GetById(request.BusinessId);
            if (business == null)
                notification.AddError(BusinessProfileStatic.BusinessIdMsgErrorNotFound);

            if (notification.HasErrors())
                return notification;
            foreach (string Description in request.ListDescription)
            {

                ValidatorString(notification, Description, CommonStatic.DescriptionMaxLength, CommonStatic.DescriptionMsgErrorMaxLength, CommonStatic.DescriptionMsgErrorRequiered, true);

                if (notification.HasErrors())
                    return notification;

                BusinessProfile? businessProfile = _businessProfileRepository.GetbyDescription(Description, request.BusinessId);
                if (businessProfile != null)
                    notification.AddError(String.Format(BusinessProfileStatic.ListDescriptionMsgErrorDuplicate, Description));
            }
            return notification;
        }
    }
}

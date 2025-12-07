using AnaPrevention.GeneralMasterData.Api.BusinessAreas.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.BusinessAreas.Application.Static;
using AnaPrevention.GeneralMasterData.Api.BusinessAreas.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.BusinessAreas.Infrastructure.Repositories;
using AnaPrevention.GeneralMasterData.Api.Businesses.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Businesses.Infrastructure.Repositories;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Validators;
using AnaPrevention.GeneralMasterData.Api.Common.Domain.Entities;

namespace AnaPrevention.GeneralMasterData.Api.BusinessAreas.Application.Validators
{
    public class RegisterListBusinessAreaValidator : Validator
    {
        private readonly BusinessAreaRepository _businessAreaRepository;
        private readonly BusinessRepository _businessRepository;

        public RegisterListBusinessAreaValidator(
            BusinessAreaRepository businessAreaRepository,
            BusinessRepository businessRepositor)
        {
            _businessAreaRepository = businessAreaRepository;
            _businessRepository = businessRepositor;
        }
        public Notification Validate(RegisterListBusinessAreaRequest request)
        {
            Notification notification = new();


            Business? business = _businessRepository.GetById(request.BusinessId);
            if (business == null)
                notification.AddError(BusinessAreaStatic.BusinessIdMsgErrorNotFound);

            if (notification.HasErrors())
                return notification;
            foreach (string Description in request.ListDescription)
            {

                ValidatorString(notification, Description, CommonStatic.DescriptionMaxLength, CommonStatic.DescriptionMsgErrorMaxLength, CommonStatic.DescriptionMsgErrorRequiered, true);

                if (notification.HasErrors())
                    return notification;

                //BusinessArea? businessArea = _businessAreaRepository.GetbyDescription(Description);
                //if (businessArea != null)
                //    notification.AddError(String.Format(BusinessAreaStatic.ListDescriptionMsgErrorDuplicate, Description));
            }
            return notification;
        }
    }
}

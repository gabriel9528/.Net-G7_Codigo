using AnaPrevention.GeneralMasterData.Api.BusinessPositions.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.BusinessPositions.Application.Static;
using AnaPrevention.GeneralMasterData.Api.BusinessPositions.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.BusinessPositions.Infrastructure.Repositories;
using AnaPrevention.GeneralMasterData.Api.Common.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.BusinessAreas.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.BusinessAreas.Infrastructure.Repositories;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Validators;

namespace AnaPrevention.GeneralMasterData.Api.BusinessPositions.Application.Validators
{
    public class RegisterListBusinessPositionValidator : Validator
    {
        private readonly BusinessPositionRepository _businessPositionRepository;
        private readonly BusinessAreaRepository _businessAreaRepository;

        public RegisterListBusinessPositionValidator(
            BusinessPositionRepository businessPositionRepository,
            BusinessAreaRepository businessAreaRepository)
        {
            _businessPositionRepository = businessPositionRepository;
            _businessAreaRepository = businessAreaRepository;
        }
        public Notification Validate(RegisterListBusinessPositionRequest request)
        {
            Notification notification = new();

            if (request.BusinessAreaId == Guid.Empty)
                notification.AddError(BusinessPositionStatic.BusinessAreaIdMsgErrorRequiered);

            BusinessArea? business = _businessAreaRepository.GetById(request.BusinessAreaId);
            if (business == null)
                notification.AddError(BusinessPositionStatic.BusinessAreaIdMsgErrorNotFound);

            if (notification.HasErrors())
                return notification;
            foreach (string Description in request.ListDescription)
            {

                ValidatorString(notification, Description, CommonStatic.DescriptionMaxLength, CommonStatic.DescriptionMsgErrorMaxLength, CommonStatic.DescriptionMsgErrorRequiered, true);

                if (notification.HasErrors())
                    return notification;

                BusinessPosition? businessPosition = _businessPositionRepository.GetbyDescription(Description);
                if (businessPosition != null)
                    notification.AddError(String.Format(BusinessPositionStatic.ListDescriptionMsgErrorDuplicate, Description));
            }
            return notification;
        }
    }
}

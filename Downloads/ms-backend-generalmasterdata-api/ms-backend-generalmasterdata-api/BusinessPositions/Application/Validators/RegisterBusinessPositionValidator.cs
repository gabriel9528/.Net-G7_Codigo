using AnaPrevention.GeneralMasterData.Api.Common.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.BusinessPositions.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.BusinessPositions.Application.Static;
using AnaPrevention.GeneralMasterData.Api.BusinessPositions.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.BusinessPositions.Infrastructure.Repositories;
using AnaPrevention.GeneralMasterData.Api.Businesses.Infrastructure.Repositories;
using AnaPrevention.GeneralMasterData.Api.BusinessAreas.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.BusinessAreas.Infrastructure.Repositories;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Validators;

namespace AnaPrevention.GeneralMasterData.Api.BusinessPositions.Application.Validators
{
    public class RegisterBusinessPositionValidator : Validator
    {
        private readonly BusinessPositionRepository _businessPositionRepository;
        private readonly BusinessAreaRepository _businessAreaRepository;

        public RegisterBusinessPositionValidator(
            BusinessPositionRepository businessPositionRepository,
            BusinessAreaRepository businessAreaRepository)
        {
            _businessPositionRepository = businessPositionRepository;
            _businessAreaRepository = businessAreaRepository;
        }

        public Notification Validate(RegisterBusinessPositionRequest request)
        {
            Notification notification = new();

            if (request.BusinessAreaId == Guid.Empty)
                notification.AddError(BusinessPositionStatic.BusinessAreaIdMsgErrorRequiered);

            ValidatorString(notification, request.Description, CommonStatic.DescriptionMaxLength, CommonStatic.DescriptionMsgErrorMaxLength, CommonStatic.DescriptionMsgErrorRequiered, true);


            if (notification.HasErrors())
            {
                return notification;
            }

            BusinessPosition? businessPosition = _businessPositionRepository.GetbyDescription(request.Description, request.BusinessAreaId);
            if (businessPosition != null)
                notification.AddError(CommonStatic.DescriptionMsgErrorDuplicate);

            BusinessArea? businessArea = _businessAreaRepository.GetById(request.BusinessAreaId);
            if (businessArea == null)
                notification.AddError(BusinessPositionStatic.BusinessAreaIdMsgErrorNotFound);

            return notification;
        }
    }
}

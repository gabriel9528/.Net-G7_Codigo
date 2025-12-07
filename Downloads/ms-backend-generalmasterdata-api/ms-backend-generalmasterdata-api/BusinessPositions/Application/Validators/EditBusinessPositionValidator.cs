using AnaPrevention.GeneralMasterData.Api.Common.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.BusinessPositions.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.BusinessPositions.Infrastructure.Repositories;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Validators;

namespace AnaPrevention.GeneralMasterData.Api.BusinessPositions.Application.Validators
{
    public class EditBusinessPositionValidator : Validator
    {
        private readonly BusinessPositionRepository _businessPositionRepository;

        public EditBusinessPositionValidator(BusinessPositionRepository businessPositionRepository)
        {
            _businessPositionRepository = businessPositionRepository;
        }

        public Notification Validate(EditBusinessPositionRequest request)
        {
            Notification notification = new();

            if (request.Id == Guid.Empty)
                notification.AddError(CommonStatic.IdMsgErrorRequiered);

            ValidatorString(notification, request.Description, CommonStatic.DescriptionMaxLength, CommonStatic.DescriptionMsgErrorMaxLength, CommonStatic.DescriptionMsgErrorRequiered, true);



            if (notification.HasErrors())
            {
                return notification;
            }

            bool descriptionTakenForEdit = _businessPositionRepository.DescriptionTakenForEdit(request.Id, request.Description);

            if (descriptionTakenForEdit)
                notification.AddError(CommonStatic.DescriptionMsgErrorDuplicate);


            return notification;
        }
    }
}

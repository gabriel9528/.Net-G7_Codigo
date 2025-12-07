using AnaPrevention.GeneralMasterData.Api.Common.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.BusinessAreas.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.BusinessAreas.Application.Static;
using AnaPrevention.GeneralMasterData.Api.BusinessAreas.Infrastructure.Repositories;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Validators;

namespace AnaPrevention.GeneralMasterData.Api.BusinessAreas.Application.Validators
{
    public class EditBusinessAreaValidator : Validator
    {
        private readonly BusinessAreaRepository _businessAreaRepository;

        public EditBusinessAreaValidator(BusinessAreaRepository businessAreaRepository)
        {
            _businessAreaRepository = businessAreaRepository;
        }

        public Notification Validate(EditBusinessAreaRequest request)
        {
            Notification notification = new();

            if (request.Id == Guid.Empty)
                notification.AddError(CommonStatic.IdMsgErrorRequiered);

            ValidatorString(notification, request.Description, CommonStatic.DescriptionMaxLength, CommonStatic.DescriptionMsgErrorMaxLength, CommonStatic.DescriptionMsgErrorRequiered, true);


            if (notification.HasErrors())
                return notification;

            bool descriptionTakenForEdit = _businessAreaRepository.DescriptionTakenForEdit(request.Id, request.Description);

            if (descriptionTakenForEdit)
                notification.AddError(CommonStatic.DescriptionMsgErrorDuplicate);


            return notification;
        }
    }
}

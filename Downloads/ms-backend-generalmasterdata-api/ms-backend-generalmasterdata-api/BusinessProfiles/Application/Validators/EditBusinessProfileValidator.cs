using AnaPrevention.GeneralMasterData.Api.Common.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.BusinessProfiles.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.BusinessProfiles.Infrastructure.Repositories;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Validators;

namespace AnaPrevention.GeneralMasterData.Api.BusinessProfiles.Application.Validators
{
    public class EditBusinessProfileValidator : Validator
    {
        private readonly BusinessProfileRepository _businessProfileRepository;

        public EditBusinessProfileValidator(BusinessProfileRepository businessProfileRepository)
        {
            _businessProfileRepository = businessProfileRepository;
        }

        public Notification Validate(EditBusinessProfileRequest request)
        {
            Notification notification = new();

            if (request.Id == Guid.Empty)
                notification.AddError(CommonStatic.IdMsgErrorRequiered);

            ValidatorString(notification, request.Description, CommonStatic.DescriptionMaxLength, CommonStatic.DescriptionMsgErrorMaxLength, CommonStatic.DescriptionMsgErrorRequiered, true);


            if (notification.HasErrors())
            {
                return notification;
            }

            var profile = _businessProfileRepository.GetById(request.Id);

            if (profile != null)
            {
                bool descriptionTakenForEdit = _businessProfileRepository.DescriptionTakenForEdit(request.Id, request.Description, profile.BusinessId);

                if (descriptionTakenForEdit)
                    notification.AddError(CommonStatic.DescriptionMsgErrorDuplicate);
            }
            return notification;
        }
    }
}

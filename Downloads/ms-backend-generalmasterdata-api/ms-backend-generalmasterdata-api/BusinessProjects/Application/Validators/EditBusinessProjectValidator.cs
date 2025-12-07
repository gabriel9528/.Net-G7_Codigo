using AnaPrevention.GeneralMasterData.Api.Common.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.BusinessProjects.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.BusinessProjects.Infrastructure.Repositories;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Validators;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;

namespace AnaPrevention.GeneralMasterData.Api.BusinessProjects.Application.Validators
{
    public class EditBusinessProjectValidator : Validator
    {
        private readonly BusinessProjectRepository _businessProjectRepository;

        public EditBusinessProjectValidator(BusinessProjectRepository businessProjectRepository)
        {
            _businessProjectRepository = businessProjectRepository;
        }

        public Notification Validate(EditBusinessProjectRequest request)
        {
            Notification notification = new();

            if (request.Id == Guid.Empty)
                notification.AddError(CommonStatic.IdMsgErrorRequiered);

            ValidatorString(notification, request.Description, CommonStatic.DescriptionMaxLength, CommonStatic.DescriptionMsgErrorMaxLength, CommonStatic.DescriptionMsgErrorRequiered, true);



            if (notification.HasErrors())
            {
                return notification;
            }

            bool descriptionTakenForEdit = _businessProjectRepository.DescriptionTakenForEdit(request.Id, request.Description);

            if (descriptionTakenForEdit)
                notification.AddError(CommonStatic.DescriptionMsgErrorDuplicate);


            return notification;
        }
    }
}

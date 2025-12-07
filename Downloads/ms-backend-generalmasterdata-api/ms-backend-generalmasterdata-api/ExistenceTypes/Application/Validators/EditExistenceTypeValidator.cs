using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Validators;
using AnaPrevention.GeneralMasterData.Api.Common.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.ExistenceTypes.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.ExistenceTypes.Application.Static;
using AnaPrevention.GeneralMasterData.Api.ExistenceTypes.Infrastructure.Repositories;

namespace AnaPrevention.GeneralMasterData.Api.ExistenceTypes.Application.Validators
{
    public class EditExistenceTypeValidator: Validator
    {
         private readonly ExistenceTypeRepository _existenceTypeRepository;

        public EditExistenceTypeValidator(ExistenceTypeRepository existenceTypeRepository)
        {
            _existenceTypeRepository = existenceTypeRepository;
        }

        public Notification Validate(EditExistenceTypeRequest request)
        {
            Notification notification = new();

            if (request.Id == Guid.Empty)
                notification.AddError(CommonStatic.IdMsgErrorRequiered);

            ValidatorString(notification, request.Description, CommonStatic.DescriptionMaxLength, CommonStatic.DescriptionMsgErrorMaxLength, CommonStatic.DescriptionMsgErrorRequiered, true);
            ValidatorString(notification, request.Code, CommonStatic.CodeMaxLength, CommonStatic.CodeMsgErrorMaxLength, CommonStatic.CodeMsgErrorRequiered, true);

            if (notification.HasErrors())
            {
                return notification;
            }

            bool descriptionTakenForEdit = _existenceTypeRepository.DescriptionTakenForEdit(request.Id, request.Description);

            if (descriptionTakenForEdit)
                notification.AddError(CommonStatic.DescriptionMsgErrorDuplicate);

            bool CodeTakenForEdit = _existenceTypeRepository.CodeTakenForEdit(request.Id, request.Code);

            if (CodeTakenForEdit)
                notification.AddError(CommonStatic.CodeMsgErrorDuplicate);


            return notification;
        }
    }
}

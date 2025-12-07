using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Validators;
using AnaPrevention.GeneralMasterData.Api.Common.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.ItemTypes.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.ItemTypes.Infrastructure.Repositories;

namespace AnaPrevention.GeneralMasterData.Api.ItemTypes.Application.Validators
{
    public class EditItemTypeValidator : Validator
    {
        private readonly ItemTypeRepository _itemTypeRepository;

        public EditItemTypeValidator(ItemTypeRepository itemTypeRepository)
        {
            _itemTypeRepository = itemTypeRepository;
        }

        public Notification Validate(EditItemTypeRequest request)
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

            bool descriptionTakenForEdit = _itemTypeRepository.DescriptionTakenForEdit(request.Id, request.Description);

            if (descriptionTakenForEdit)
                notification.AddError(CommonStatic.DescriptionMsgErrorDuplicate);

            bool CodeTakenForEdit = _itemTypeRepository.CodeTakenForEdit(request.Id, request.Code);

            if (CodeTakenForEdit)
                notification.AddError(CommonStatic.CodeMsgErrorDuplicate);


            return notification;
        }
    }
}

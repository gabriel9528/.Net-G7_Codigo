using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Validators;
using AnaPrevention.GeneralMasterData.Api.Common.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.ItemTypes.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.ItemTypes.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.ItemTypes.Infrastructure.Repositories;

namespace AnaPrevention.GeneralMasterData.Api.ItemTypes.Application.Validators
{
    public class RegisterItemTypeValidator : Validator
    {
        private readonly ItemTypeRepository _itemTypeRepository;

        public RegisterItemTypeValidator(ItemTypeRepository itemTypeRepository)
        {
            _itemTypeRepository = itemTypeRepository;
        }

        public Notification Validate(RegisterItemTypeRequest request)
        {
            Notification notification = new();

            ValidatorString(notification, request.Description, CommonStatic.DescriptionMaxLength, CommonStatic.DescriptionMsgErrorMaxLength, CommonStatic.DescriptionMsgErrorRequiered, true);


            if (notification.HasErrors())
            {
                return notification;
            }


            ItemType? itemType = _itemTypeRepository.GetbyDescription(request.Description);
            if (itemType != null)
                notification.AddError(CommonStatic.DescriptionMsgErrorDuplicate);

            itemType = _itemTypeRepository.GetbyCode(request.Code);
            if (itemType != null)
                notification.AddError(CommonStatic.CodeMsgErrorDuplicate);

            return notification;
        }
    }
}

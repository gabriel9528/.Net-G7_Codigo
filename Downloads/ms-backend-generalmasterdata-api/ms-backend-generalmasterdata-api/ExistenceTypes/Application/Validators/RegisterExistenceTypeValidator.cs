using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Validators;
using AnaPrevention.GeneralMasterData.Api.Common.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.ExistenceTypes.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.ExistenceTypes.Application.Static;
using AnaPrevention.GeneralMasterData.Api.ExistenceTypes.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.ExistenceTypes.Infrastructure.Repositories;

namespace AnaPrevention.GeneralMasterData.Api.ExistenceTypes.Application.Validators
{
    public class RegisterExistenceTypeValidator: Validator
    {
        private readonly ExistenceTypeRepository _existenceTypeRepository;

        public RegisterExistenceTypeValidator(ExistenceTypeRepository existenceTypeRepository)
        {
            _existenceTypeRepository = existenceTypeRepository;
        }

        public Notification Validate(RegisterExistenceTypeRequest request)
        {
            Notification notification = new();

            ValidatorString(notification, request.Description, CommonStatic.DescriptionMaxLength, CommonStatic.DescriptionMsgErrorMaxLength, CommonStatic.DescriptionMsgErrorRequiered, true);
           

            if (notification.HasErrors())
            {
                return notification;
            }


            ExistenceType? existenceType = _existenceTypeRepository.GetbyDescription(request.Description);
            if (existenceType != null)
                notification.AddError(CommonStatic.DescriptionMsgErrorDuplicate);

            existenceType = _existenceTypeRepository.GetbyCode(request.Code);
            if (existenceType != null)
                notification.AddError(CommonStatic.CodeMsgErrorDuplicate);

            return notification;
        }
    }
}

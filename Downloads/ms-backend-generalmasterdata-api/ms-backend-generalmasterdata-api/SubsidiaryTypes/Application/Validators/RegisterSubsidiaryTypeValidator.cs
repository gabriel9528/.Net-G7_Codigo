using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Validators;
using AnaPrevention.GeneralMasterData.Api.Common.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.SubsidiaryTypes.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.SubsidiaryTypes.Infrastructure.Repositories;

namespace AnaPrevention.GeneralMasterData.Api.SubsidiaryTypes.Application.Validators
{
    public class RegisterSubsidiaryTypeValidator: Validator
    {

        private readonly SubsidiaryTypeRepository _subsidiaryTypeRepository;

        public RegisterSubsidiaryTypeValidator(SubsidiaryTypeRepository subsidiaryTypeRepository)
        {
            _subsidiaryTypeRepository = subsidiaryTypeRepository;
        }

        public Notification Validate(RegisterSubsidiaryTypeRequest request)
        {
            Notification notification = new();

            ValidatorString(notification, request.Description, CommonStatic.DescriptionMaxLength, CommonStatic.DescriptionMsgErrorMaxLength, CommonStatic.DescriptionMsgErrorRequiered, true);
           

            if (notification.HasErrors())
            {
                return notification;
            }


            SubsidiaryType? subsidiaryType = _subsidiaryTypeRepository.GetbyDescription(request.Description);
            if (subsidiaryType != null)
                notification.AddError(CommonStatic.DescriptionMsgErrorDuplicate);

            subsidiaryType = _subsidiaryTypeRepository.GetbyCode(request.Code);
            if (subsidiaryType != null)
                notification.AddError(CommonStatic.CodeMsgErrorDuplicate);

            return notification;
        }
    }
}

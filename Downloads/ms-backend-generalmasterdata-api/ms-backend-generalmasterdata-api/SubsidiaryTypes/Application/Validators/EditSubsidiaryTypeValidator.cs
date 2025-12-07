using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Validators;
using AnaPrevention.GeneralMasterData.Api.Common.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api   .SubsidiaryTypes.Infrastructure.Repositories;

namespace AnaPrevention.GeneralMasterData.Api.SubsidiaryTypes.Application.Validators
{
    public class EditSubsidiaryTypeValidator(SubsidiaryTypeRepository subsidiaryTypeRepository) : Validator
    {

        private readonly SubsidiaryTypeRepository _SubsidiaryTypeRepository = subsidiaryTypeRepository;

        public Notification Validate(EditSubsidiaryTypeRequest request)
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

            bool descriptionTakenForEdit = _SubsidiaryTypeRepository.DescriptionTakenForEdit(request.Id, request.Description);

            if (descriptionTakenForEdit)
                notification.AddError(CommonStatic.DescriptionMsgErrorDuplicate);

            bool codeTakenForEdit = _SubsidiaryTypeRepository.CodeTakenForEdit(request.Id, request.Code);

            if (codeTakenForEdit)
                notification.AddError(CommonStatic.CodeMsgErrorDuplicate);

            return notification;
        }
    }
}

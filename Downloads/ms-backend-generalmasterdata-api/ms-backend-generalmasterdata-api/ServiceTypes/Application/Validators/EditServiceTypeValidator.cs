using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Validators;
using AnaPrevention.GeneralMasterData.Api.Common.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.ServiceTypes.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.ServiceTypes.Application.Static;
using AnaPrevention.GeneralMasterData.Api.ServiceTypes.Domain.Enums;
using AnaPrevention.GeneralMasterData.Api.ServiceTypes.Infrastructure.Repositories;

namespace AnaPrevention.GeneralMasterData.Api.ServiceTypes.Application.Validators
{
    public class EditServiceTypeValidator : Validator
    {

        private readonly ServiceTypeRepository _ServiceTypeRepository;

        public EditServiceTypeValidator(ServiceTypeRepository ServiceTypeRepository)
        {
            _ServiceTypeRepository = ServiceTypeRepository;
        }

        public Notification Validate(EditServiceTypeRequest request, Guid companyId)
        {
            Notification notification = new();

            if (request.Id == Guid.Empty)
                notification.AddError(CommonStatic.IdMsgErrorRequiered);

            ValidatorString(notification, request.Description, CommonStatic.DescriptionMaxLength, CommonStatic.DescriptionMsgErrorMaxLength, CommonStatic.DescriptionMsgErrorRequiered, true);

            if (request.Code is not ServiceTypeEnum.ASASSISTENTIAL and ServiceTypeEnum.OCCUPATIONAL)
            {
                notification.AddError(ServiceTypeStatic.CodeMsgErrorFormat);
            }

            if (notification.HasErrors())
            {
                return notification;
            }


            bool descriptionTakenForEdit = _ServiceTypeRepository.DescriptionTakenForEdit(request.Id, request.Description, companyId);

            if (descriptionTakenForEdit)
                notification.AddError(CommonStatic.DescriptionMsgErrorDuplicate);

            bool codeakenForEdit = _ServiceTypeRepository.CodeTakenForEdit(request.Id, request.Code, companyId);

            if (codeakenForEdit)
                notification.AddError(CommonStatic.CodeMsgErrorDuplicate);

            return notification;
        }
    }
}


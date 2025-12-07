using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Validators;
using AnaPrevention.GeneralMasterData.Api.Common.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Dimensions.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.Dimensions.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Dimensions.Infrastructure.Repositories;

namespace AnaPrevention.GeneralMasterData.Api.Dimensions.Application.Validators
{
    public class EditDimensionValidator : Validator
    {
        private readonly DimensionRepository _DimensionRepository;

        public EditDimensionValidator(DimensionRepository dimensionRepository)
        {
            _DimensionRepository = dimensionRepository;
        }

        public Notification Validate(EditDimensionRequest request, Guid companyId)
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

            bool descriptionTakenForEdit = _DimensionRepository.DescriptionTakenForEdit(request.Id, request.Description, companyId);

            if (descriptionTakenForEdit)
                notification.AddError(CommonStatic.DescriptionMsgErrorDuplicate);

            bool codeTakenForEdit = _DimensionRepository.CodeTakenForEdit(request.Id, request.Code, companyId);

            if (codeTakenForEdit)
                notification.AddError(CommonStatic.CodeMsgErrorDuplicate);

            return notification;
        }
    }
}

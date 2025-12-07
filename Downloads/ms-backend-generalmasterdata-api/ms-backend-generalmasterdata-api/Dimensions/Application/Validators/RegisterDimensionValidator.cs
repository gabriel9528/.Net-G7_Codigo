using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Validators;
using AnaPrevention.GeneralMasterData.Api.Common.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Dimensions.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.Dimensions.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Dimensions.Infrastructure.Repositories;

namespace AnaPrevention.GeneralMasterData.Api.Dimensions.Application.Validators
{
    public class RegisterDimensionValidator : Validator
    {
        private readonly DimensionRepository _dimensionRepository;

        public RegisterDimensionValidator(DimensionRepository dimensionRepository)
        {
            _dimensionRepository = dimensionRepository;
        }

        public Notification Validate(RegisterDimensionRequest request, Guid companyId)
        {
            Notification notification = new();

            ValidatorString(notification, request.Description, CommonStatic.DescriptionMaxLength, CommonStatic.DescriptionMsgErrorMaxLength, CommonStatic.DescriptionMsgErrorRequiered, true);

            if (notification.HasErrors())
            {
                return notification;
            }


            Dimension? dimension = _dimensionRepository.GetbyDescription(request.Description, companyId);
            if (dimension != null)
                notification.AddError(CommonStatic.DescriptionMsgErrorDuplicate);

            dimension = _dimensionRepository.GetbyCode(request.Code);
            if (dimension != null)
                notification.AddError(CommonStatic.CodeMsgErrorDuplicate);

            return notification;
        }
    }
}

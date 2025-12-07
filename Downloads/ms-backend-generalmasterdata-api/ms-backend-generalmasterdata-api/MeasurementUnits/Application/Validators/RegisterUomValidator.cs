using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Validators;
using AnaPrevention.GeneralMasterData.Api.Common.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.MeasurementUnits.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.MeasurementUnits.Application.Static;
using AnaPrevention.GeneralMasterData.Api.MeasurementUnits.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.MeasurementUnits.Infrastructure.Repositories;

namespace AnaPrevention.GeneralMasterData.Api.MeasurementUnits.Application.Validators
{
    public class RegisterUomValidator: Validator
    {
        private readonly UomRepository _uomRepository;

        public RegisterUomValidator(UomRepository uomRepository)
        {
            _uomRepository = uomRepository;
        }

        public Notification Validate(RegisterUomRequest request)
        {
            Notification notification = new();

            ValidatorString(notification, request.Description, CommonStatic.DescriptionMaxLength, CommonStatic.DescriptionMsgErrorMaxLength, CommonStatic.DescriptionMsgErrorRequiered, true);
           


            if (notification.HasErrors())
            {
                return notification;
            }


            Uom? uom = _uomRepository.GetbyDescription(request.Description);
            if (uom != null)
                notification.AddError(CommonStatic.DescriptionMsgErrorDuplicate);

             uom = _uomRepository.GetbyCode(request.Code);
            if (uom != null)
                notification.AddError(CommonStatic.CodeMsgErrorDuplicate);

            uom = _uomRepository.GetbyFiscalCode(request.FiscalCode);
            if (uom != null)
                notification.AddError(UomStatic.FiscalCodeMsgErrorDuplicate);

            return notification;
        }
    }
}

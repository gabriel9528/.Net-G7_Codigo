using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Validators;
using AnaPrevention.GeneralMasterData.Api.Common.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.MeasurementUnits.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.MeasurementUnits.Application.Static;
using AnaPrevention.GeneralMasterData.Api.MeasurementUnits.Infrastructure.Repositories;

namespace AnaPrevention.GeneralMasterData.Api.MeasurementUnits.Application.Validators
{
    public class EditUomValidator: Validator
    {
        private readonly UomRepository _uomRepository;

        public EditUomValidator(UomRepository uomRepository)
        {
            _uomRepository = uomRepository;
        }

        public Notification Validate(EditUomRequest request)
        {
            Notification notification = new();

            if (request.Id == Guid.Empty)
                notification.AddError(CommonStatic.IdMsgErrorRequiered);

            ValidatorString(notification, request.Description, CommonStatic.DescriptionMaxLength, CommonStatic.DescriptionMsgErrorMaxLength, CommonStatic.DescriptionMsgErrorRequiered, true);
            ValidatorString(notification, request.Code, CommonStatic.CodeMaxLength, CommonStatic.CodeMsgErrorMaxLength, CommonStatic.CodeMsgErrorRequiered, true);

            string Fiscalcode = string.IsNullOrWhiteSpace(request.Code) ? "" : request.Code.Trim();

          

            if (Fiscalcode.Length > UomStatic.FiscalCodeMaxLength)
                notification.AddError(String.Format(UomStatic.FiscalCodeMsgErrorMaxLength, UomStatic.FiscalCodeMaxLength.ToString()));

            if (notification.HasErrors())
            {
                return notification;
            }

            bool descriptionTakenForEdit = _uomRepository.DescriptionTakenForEdit(request.Id, request.Description);

            if (descriptionTakenForEdit)
                notification.AddError(CommonStatic.DescriptionMsgErrorDuplicate);

            bool CodeTakenForEdit = _uomRepository.CodeTakenForEdit(request.Id, request.Code);

            if (CodeTakenForEdit)
                notification.AddError(CommonStatic.CodeMsgErrorDuplicate);


            if (_uomRepository.FiscalCodeTakenForEdit(request.Id, request.FiscalCode))
                notification.AddError(UomStatic.FiscalCodeMsgErrorDuplicate);

            return notification;
        }
    }
}

using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Validators;
using AnaPrevention.GeneralMasterData.Api.Common.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Diagnostics.Domain.Dtos;
using AnaPrevention.GeneralMasterData.Api.Diagnostics.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Diagnostics.Domain.Static;
using AnaPrevention.GeneralMasterData.Api.Diagnostics.Infrastructure.Repositories;

namespace AnaPrevention.GeneralMasterData.Api.Diagnostics.Domain.Validators
{
    public class RegisterDiagnosticValidator: Validator
    {
        private readonly DiagnosticRepository _diagnosticRepository;

        public RegisterDiagnosticValidator(DiagnosticRepository diagnosticRepository)
        {
            _diagnosticRepository = diagnosticRepository;
        }

        public Notification Validate(RegisterDiagnosticRequest request)
        {
            Notification notification = new();

            ValidatorString(notification, request.Description, CommonStatic.DescriptionMaxLength, CommonStatic.DescriptionMsgErrorMaxLength, CommonStatic.DescriptionMsgErrorRequiered, true);

            string cie10 = string.IsNullOrWhiteSpace(request.Cie10) ? "" : request.Cie10.Trim();

            if (string.IsNullOrWhiteSpace(cie10))
                notification.AddError(DiagnosticStatic.Cie10MsgErrorRequiered);

            if (cie10.Length > DiagnosticStatic.Cie10MaxLength)
                notification.AddError(String.Format(DiagnosticStatic.Cie10MsgErrorMaxLength, DiagnosticStatic.Cie10MaxLength.ToString()));

            string description2 = string.IsNullOrWhiteSpace(request.Description2) ? "" : request.Description2.Trim();

            if (description2.Length > DiagnosticStatic.Description2MaxLength)
                notification.AddError(String.Format(DiagnosticStatic.Description2MsgErrorMaxLength, DiagnosticStatic.Description2MaxLength.ToString()));

            if (notification.HasErrors())
            {
                return notification;
            }


            Diagnostic? diagnostic = _diagnosticRepository.GetbyDescription(request.Description);
            if (diagnostic != null)
                notification.AddError(CommonStatic.DescriptionMsgErrorDuplicate);

            diagnostic = _diagnosticRepository.GetbyCie10(request.Cie10);
            if (diagnostic != null)
                notification.AddError(DiagnosticStatic.Cie10MsgErrorDuplicate);

            return notification;
        }
    }
}

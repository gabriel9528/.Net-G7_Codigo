using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Validators;
using AnaPrevention.GeneralMasterData.Api.Common.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Diagnostics.Domain.Dtos;
using AnaPrevention.GeneralMasterData.Api.Diagnostics.Domain.Static;
using AnaPrevention.GeneralMasterData.Api.Diagnostics.Infrastructure.Repositories;

namespace AnaPrevention.GeneralMasterData.Api.Diagnostics.Domain.Validators
{
    public class EditDiagnosticValidator: Validator
    {
        private readonly DiagnosticRepository _diagnosticRepository;

        public EditDiagnosticValidator(DiagnosticRepository diagnosticRepository)
        {
            _diagnosticRepository = diagnosticRepository;
        }

        public Notification Validate(EditDiagnosticRequest request)
        {
            Notification notification = new();

            if (request.Id == Guid.Empty)
                notification.AddError(DiagnosticStatic.IdMsgErrorRequiered);

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

            bool descriptionTakenForEdit = _diagnosticRepository.DescriptionTakenForEdit(request.Id, request.Description);

            if (descriptionTakenForEdit)
                notification.AddError(CommonStatic.DescriptionMsgErrorDuplicate);
            
            return notification;
        }
    }
}

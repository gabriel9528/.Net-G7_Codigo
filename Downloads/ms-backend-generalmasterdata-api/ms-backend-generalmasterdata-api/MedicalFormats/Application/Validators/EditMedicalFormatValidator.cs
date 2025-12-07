using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Validators;
using AnaPrevention.GeneralMasterData.Api.Common.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.ServiceCatalogs.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.ServiceCatalogs.Infrastructure.Repositories;

namespace AnaPrevention.GeneralMasterData.Api.ServiceCatalogs.Application.Validators
{
    public class EditMedicalFormatValidator : Validator
    {
        private readonly MedicalFormatRepository _medicalFormatRepository;

        public EditMedicalFormatValidator(MedicalFormatRepository medicalFormatRepository)
        {
            _medicalFormatRepository = medicalFormatRepository;
        }

        public Notification Validate(EditMedicalFormatRequest request)
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

            bool descriptionTakenForEdit = _medicalFormatRepository.DescriptionTakenForEdit(request.Id, request.Description);

            if (descriptionTakenForEdit)
                notification.AddError(CommonStatic.DescriptionMsgErrorDuplicate);

            bool CodeTakenForEdit = _medicalFormatRepository.CodeTakenForEdit(request.Id, request.Code);

            if (CodeTakenForEdit)
                notification.AddError(CommonStatic.CodeMsgErrorDuplicate);


            return notification;
        }
    }
}

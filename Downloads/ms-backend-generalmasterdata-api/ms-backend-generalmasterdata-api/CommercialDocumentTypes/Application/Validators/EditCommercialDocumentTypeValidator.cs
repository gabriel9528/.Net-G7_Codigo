using AnaPrevention.GeneralMasterData.Api.Common.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.CommercialDocumentTypes.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.CommercialDocumentTypes.Infrastructure.Repositories;
using AnaPrevention.GeneralMasterData.Api.CommercialDocumentTypes.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Validators;

namespace AnaPrevention.GeneralMasterData.Api.CommercialDocumentTypes.Application.Validators
{
    public class EditCommercialDocumentTypeValidator: Validator
    {
        private readonly CommercialDocumentTypeRepository _commercialDocumentTypeRepository;

        public EditCommercialDocumentTypeValidator(CommercialDocumentTypeRepository commercialDocumentTypeRepository)
        {
            _commercialDocumentTypeRepository = commercialDocumentTypeRepository;
        }

        public Notification Validate(EditCommercialDocumentTypeRequest request)
        {
            Notification notification = new();

            if (request.Id == Guid.Empty)
                notification.AddError(CommonStatic.IdMsgErrorRequiered);

            ValidatorString(notification, request.Description, CommonStatic.DescriptionMaxLength, CommonStatic.DescriptionMsgErrorMaxLength, CommonStatic.DescriptionMsgErrorRequiered, true);
            ValidatorString(notification, request.Code, CommonStatic.CodeMaxLength, CommonStatic.CodeMsgErrorMaxLength, CommonStatic.CodeMsgErrorRequiered, true);

            string abbreviation = string.IsNullOrWhiteSpace(request.Abbreviation) ? "" : request.Abbreviation.Trim();

            if (string.IsNullOrWhiteSpace(abbreviation))
                notification.AddError(CommercialDocumentTypeStatic.AbbreviationMsgErrorRequiered);

            if (abbreviation.Length > CommercialDocumentTypeStatic.AbbreviationMaxLength)
                notification.AddError(String.Format(CommercialDocumentTypeStatic.AbbreviationMsgErrorMaxLength, CommercialDocumentTypeStatic.AbbreviationMaxLength.ToString()));


            if (notification.HasErrors())
            {
                return notification;
            }


            bool descriptionTakenForEdit = _commercialDocumentTypeRepository.DescriptionTakenForEdit(request.Id, request.Description);

            if (descriptionTakenForEdit)
                notification.AddError(CommonStatic.DescriptionMsgErrorDuplicate);

            if (_commercialDocumentTypeRepository.CodeTakenForEdit(request.Id, request.Code))
                notification.AddError(CommonStatic.CodeMsgErrorDuplicate);

            if (_commercialDocumentTypeRepository.AbbreviationTakenForEdit(request.Id, request.Abbreviation))
                notification.AddError(CommercialDocumentTypeStatic.AbbreviationMsgErrorDuplicate);

           
            return notification;
        }
    }
}

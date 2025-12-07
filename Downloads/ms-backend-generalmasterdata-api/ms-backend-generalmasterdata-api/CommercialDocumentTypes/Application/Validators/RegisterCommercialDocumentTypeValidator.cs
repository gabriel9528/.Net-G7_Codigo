using AnaPrevention.GeneralMasterData.Api.Common.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.CommercialDocumentTypes.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.CommercialDocumentTypes.Application.Entities;
using AnaPrevention.GeneralMasterData.Api.CommercialDocumentTypes.Infrastructure.Repositories;
using AnaPrevention.GeneralMasterData.Api.CommercialDocumentTypes.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Validators;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;

namespace AnaPrevention.GeneralMasterData.Api.CommercialDocumentTypes.Application.Validators
{
    public class RegisterCommercialDocumentTypeValidator: Validator
    {

        private readonly CommercialDocumentTypeRepository _commercialDocumentTypeRepository;

        public RegisterCommercialDocumentTypeValidator(CommercialDocumentTypeRepository commercialDocumentTypeRepository)
        {
            _commercialDocumentTypeRepository = commercialDocumentTypeRepository;
        }

        public Notification Validate(RegisterCommercialDocumentTypeRequest request)
        {
            Notification notification = new();

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

            CommercialDocumentType? commercialDocumentType = _commercialDocumentTypeRepository.GetbyDescription(request.Description);
            if (commercialDocumentType != null)
                notification.AddError(CommonStatic.DescriptionMsgErrorDuplicate);

            commercialDocumentType = _commercialDocumentTypeRepository.GetbyCode(request.Code);
            if (commercialDocumentType != null)
                notification.AddError(CommonStatic.CodeMsgErrorDuplicate);

            commercialDocumentType = _commercialDocumentTypeRepository.GetbyAbbreviation(request.Abbreviation);
            if (commercialDocumentType != null)
                notification.AddError(CommercialDocumentTypeStatic.AbbreviationMsgErrorDuplicate);

            return notification;
        }
    }
}

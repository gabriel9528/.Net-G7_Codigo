using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Validators;
using AnaPrevention.GeneralMasterData.Api.Common.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.IdentityDocumentTypes.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.IdentityDocumentTypes.Application.Static;
using AnaPrevention.GeneralMasterData.Api.IdentityDocumentTypes.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.IdentityDocumentTypes.Infrastructure.Repositories;

namespace AnaPrevention.GeneralMasterData.Api.IdentityDocumentTypes.Application.Validators
{
    public class RegisterIdentityDocumentTypeValidator : Validator
    {
        private readonly IdentityDocumentTypeRepository _identityDocumentTypeRepository;

        public RegisterIdentityDocumentTypeValidator(IdentityDocumentTypeRepository identityDocumentTypeRepository)
        {
            _identityDocumentTypeRepository = identityDocumentTypeRepository;
        }

        public Notification Validate(RegisterIdentityDocumentTypeRequest request)
        {
            Notification notification = new();

            ValidatorString(notification, request.Description, CommonStatic.DescriptionMaxLength, CommonStatic.DescriptionMsgErrorMaxLength, CommonStatic.DescriptionMsgErrorRequiered, true);
            ValidatorString(notification, request.Code, IdentityDocumentTypeStatic.CodeMaxLength, CommonStatic.CodeMsgErrorMaxLength, CommonStatic.CodeMsgErrorRequiered, true);
            ValidatorString(notification, request.Abbreviation, IdentityDocumentTypeStatic.AbbreviationMaxLength, IdentityDocumentTypeStatic.AbbreviationMsgErrorMaxLength, IdentityDocumentTypeStatic.AbbreviationMsgErrorRequiered, true);


            if (notification.HasErrors())
                return notification;

            IdentityDocumentType? identityDocumentType = _identityDocumentTypeRepository.GetbyDescription(request.Description);
            if (identityDocumentType != null)
                notification.AddError(CommonStatic.DescriptionMsgErrorDuplicate);

            identityDocumentType = _identityDocumentTypeRepository.GetbyCode(request.Code);
            if (identityDocumentType != null)
                notification.AddError(CommonStatic.CodeMsgErrorDuplicate);

            return notification;
        }

    }
}

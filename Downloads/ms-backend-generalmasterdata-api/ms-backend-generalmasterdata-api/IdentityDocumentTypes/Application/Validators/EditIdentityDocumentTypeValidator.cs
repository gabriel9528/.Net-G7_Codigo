using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Validators;
using AnaPrevention.GeneralMasterData.Api.Common.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.IdentityDocumentTypes.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.IdentityDocumentTypes.Application.Static;
using AnaPrevention.GeneralMasterData.Api.IdentityDocumentTypes.Infrastructure.Repositories;

namespace AnaPrevention.GeneralMasterData.Api.IdentityDocumentTypes.Application.Validators
{
    public class EditIdentityDocumentTypeValidator : Validator
    {
        private readonly IdentityDocumentTypeRepository _identityDocumentTypeRepository;

        public EditIdentityDocumentTypeValidator(IdentityDocumentTypeRepository identityDocumentTypeRepository)
        {
            _identityDocumentTypeRepository = identityDocumentTypeRepository;
        }

        public Notification Validate(EditIdentityDocumentTypeRequest request)
        {
            Notification notification = new();

            if (request.Id == Guid.Empty)
                notification.AddError(CommonStatic.IdMsgErrorRequiered);

            ValidatorString(notification, request.Description, CommonStatic.DescriptionMaxLength, CommonStatic.DescriptionMsgErrorMaxLength, CommonStatic.DescriptionMsgErrorRequiered, true);
            ValidatorString(notification, request.Code, CommonStatic.CodeMaxLength, CommonStatic.CodeMsgErrorMaxLength, CommonStatic.CodeMsgErrorRequiered, true);
            ValidatorString(notification, request.Abbreviation, IdentityDocumentTypeStatic.AbbreviationMaxLength, IdentityDocumentTypeStatic.AbbreviationMsgErrorMaxLength, IdentityDocumentTypeStatic.AbbreviationMsgErrorRequiered, true);

            if (notification.HasErrors())
            {
                return notification;
            }

            bool descriptionTakenForEdit = _identityDocumentTypeRepository.DescriptionTakenForEdit(request.Id, request.Description);

            if (descriptionTakenForEdit)
                notification.AddError(CommonStatic.DescriptionMsgErrorDuplicate);

            bool codeTakenForEdit = _identityDocumentTypeRepository.CodeTakenForEdit(request.Id, request.Code);

            if (codeTakenForEdit)
                notification.AddError(CommonStatic.CodeMsgErrorDuplicate);

            return notification;
        }

    }
}

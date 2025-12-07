using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Validators;
using AnaPrevention.GeneralMasterData.Api.Common.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Families.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.Families.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Families.Infrastructure.Repositories;
using AnaPrevention.GeneralMasterData.Api.Lines.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.Lines.Infrastructure.Repositories;

namespace AnaPrevention.GeneralMasterData.Api.Families.Application.Validators
{
    public class EditFamilyValidator: Validator
    {
        private readonly FamilyRepository _familyRepository;
        private readonly LineRepository _lineRepository;
        public EditFamilyValidator(FamilyRepository FamilyRepository, LineRepository lineRepository)
        {
            _familyRepository = FamilyRepository;
            _lineRepository = lineRepository;
        }

        public Notification Validate(EditFamilyRequest request, Guid companyId)
        {
            Notification notification = new();

            if (request.Id == Guid.Empty)
                notification.AddError(CommonStatic.IdMsgErrorRequiered);

            if (request.LineId == Guid.Empty)
                notification.AddError(FamilyStatic.LineMsgErrorRequiered);

            ValidatorString(notification, request.Description, CommonStatic.DescriptionMaxLength, CommonStatic.DescriptionMsgErrorMaxLength, CommonStatic.DescriptionMsgErrorRequiered, true);
            ValidatorString(notification, request.Code, CommonStatic.CodeMaxLength, CommonStatic.CodeMsgErrorMaxLength, CommonStatic.CodeMsgErrorRequiered, true);

            if (notification.HasErrors())
            {
                return notification;
            }

            LineDto? line = _lineRepository.GetDtoById(request.LineId,companyId);
            if (line == null)
                notification.AddError(FamilyStatic.LineMsgErrorNotFound);

            bool descriptionTakenForEdit = _familyRepository.DescriptionTakenForEdit(request.Id, request.Description, companyId, request.LineId);

            if (descriptionTakenForEdit)
                notification.AddError(CommonStatic.DescriptionMsgErrorDuplicate);

            bool codeTakenForEdit = _familyRepository.CodeTakenForEdit(request.Id, request.Code, companyId, request.LineId);

            if (codeTakenForEdit)
                notification.AddError(CommonStatic.CodeMsgErrorDuplicate);

            return notification;
        }
    }
}


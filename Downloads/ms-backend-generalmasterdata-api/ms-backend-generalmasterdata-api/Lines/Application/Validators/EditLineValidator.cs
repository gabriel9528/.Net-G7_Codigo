using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Validators;
using AnaPrevention.GeneralMasterData.Api.Common.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Lines.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.Lines.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Lines.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Lines.Infrastructure.Repositories;

namespace AnaPrevention.GeneralMasterData.Api.Lines.Application.Validators
{
    public class EditLineValidator: Validator
    {
        private readonly LineRepository _LineRepository;
        private readonly LineTypeRepository _lineTypeRepository;

        public EditLineValidator(LineRepository lineRepository, LineTypeRepository lineTypeRepository)
        {
            _LineRepository = lineRepository;
            _lineTypeRepository = lineTypeRepository;
        }

        public Notification Validate(EditLineRequest request, Guid companyId)
        {
            Notification notification = new();

            if (request.LineTypeId == Guid.Empty)
                notification.AddError(LineStatic.LineTypeIdMsgErrorRequiered);


            if (request.Id == Guid.Empty)
                notification.AddError(CommonStatic.IdMsgErrorRequiered);

            ValidatorString(notification, request.Description, CommonStatic.DescriptionMaxLength, CommonStatic.DescriptionMsgErrorMaxLength, CommonStatic.DescriptionMsgErrorRequiered, true);
            ValidatorString(notification, request.Code, CommonStatic.CodeMaxLength, CommonStatic.CodeMsgErrorMaxLength, CommonStatic.CodeMsgErrorRequiered, true);

            LineType? lineType = _lineTypeRepository.GetById(request.LineTypeId);
            if (lineType == null)
                notification.AddError(LineStatic.LineTypeIdMsgErrorNotFound);

            if (notification.HasErrors())
                return notification;

            bool descriptionTakenForEdit = _LineRepository.DescriptionTakenForEdit(request.Id, request.Description, companyId);

            if (descriptionTakenForEdit)
                notification.AddError(CommonStatic.DescriptionMsgErrorDuplicate);

            bool CodeTakenForEdit = _LineRepository.CodeTakenForEdit(request.Id, request.Code, companyId);

            if (CodeTakenForEdit)
                notification.AddError(CommonStatic.CodeMsgErrorDuplicate);

            return notification;
        }
    }
}


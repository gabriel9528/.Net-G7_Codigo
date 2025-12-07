using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Validators;
using AnaPrevention.GeneralMasterData.Api.Common.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Families.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Lines.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.Lines.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Lines.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Lines.Infrastructure.Repositories;

namespace AnaPrevention.GeneralMasterData.Api.Lines.Application.Validators
{
    public class RegisterLineValidator: Validator
    {
        private readonly LineRepository _lineRepository;
        private readonly LineTypeRepository _lineTypeRepository;

        public RegisterLineValidator(LineRepository lineRepository, LineTypeRepository lineTypeRepository)
        {
            _lineRepository = lineRepository;
            _lineTypeRepository = lineTypeRepository;
        }

        public Notification Validate(RegisterLineRequest request, Guid companyId)
        {
            Notification notification = new();

            if(request.LineTypeId == Guid.Empty)
                notification.AddError(LineStatic.LineTypeIdMsgErrorRequiered);

            ValidatorString(notification, request.Description, CommonStatic.DescriptionMaxLength, CommonStatic.DescriptionMsgErrorMaxLength, CommonStatic.DescriptionMsgErrorRequiered, true);
           

            if (notification.HasErrors())
                return notification; 

            LineType? lineType = _lineTypeRepository.GetById(request.LineTypeId);
            if (lineType == null)
                notification.AddError(LineStatic.LineTypeIdMsgErrorNotFound);

            if (notification.HasErrors())
                return notification;

            Line? line = _lineRepository.GetbyDescription(request.Description, companyId);
            if (line != null)
                notification.AddError(CommonStatic.DescriptionMsgErrorDuplicate);

            line = _lineRepository.GetbyCode(request.Code, companyId);
            if (line != null)
                notification.AddError(CommonStatic.CodeMsgErrorDuplicate);

            return notification;
        }
    }
}


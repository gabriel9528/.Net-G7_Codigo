using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Validators;
using AnaPrevention.GeneralMasterData.Api.Common.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Families.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.Families.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Families.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Families.Infrastructure.Repositories;
using AnaPrevention.GeneralMasterData.Api.Lines.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.Lines.Infrastructure.Repositories;

namespace AnaPrevention.GeneralMasterData.Api.Families.Application.Validators
{
    public class RegisterFamilyValidator: Validator
    {
        private readonly FamilyRepository _familyRepository;
        private readonly LineRepository _lineRepository;

        public RegisterFamilyValidator(FamilyRepository familyRepository, LineRepository lineRepository)
        {
            _familyRepository = familyRepository;
            _lineRepository = lineRepository;
        }

        public Notification Validate(RegisterFamilyRequest request, Guid companyId)
        {
            Notification notification = new();

            

            if (request.LineId == Guid.Empty)
                notification.AddError(FamilyStatic.LineMsgErrorRequiered);

            ValidatorString(notification, request.Description, CommonStatic.DescriptionMaxLength, CommonStatic.DescriptionMsgErrorMaxLength, CommonStatic.DescriptionMsgErrorRequiered, true);
           

            if (notification.HasErrors())
            {
                return notification;
            }

            LineDto? line = _lineRepository.GetDtoById(request.LineId, companyId);
            if (line == null)
                notification.AddError(FamilyStatic.LineMsgErrorNotFound);

            Family? family = _familyRepository.GetbyDescription(request.Description, companyId,request.LineId);
            if (family != null)
                notification.AddError(CommonStatic.DescriptionMsgErrorDuplicate);

            family = _familyRepository.GetbyCode(request.Code, companyId);
            if (family != null)
                notification.AddError(CommonStatic.CodeMsgErrorDuplicate);

            return notification;
        }
    }
}


using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Validators;
using AnaPrevention.GeneralMasterData.Api.Common.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Families.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Families.Infrastructure.Repositories;
using AnaPrevention.GeneralMasterData.Api.SubFamilies.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.SubFamilies.Application.Static;
using AnaPrevention.GeneralMasterData.Api.SubFamilies.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.SubFamilies.Infrastructure.Repositories;

namespace AnaPrevention.GeneralMasterData.Api.SubFamilies.Application.Validators
{
    public class RegisterSubFamilyValidator: Validator
    {
        private readonly SubFamilyRepository _subFamilyRepository;
        private readonly FamilyRepository _familyRepository;
        public RegisterSubFamilyValidator(SubFamilyRepository subFamilyRepository, FamilyRepository familyRepository)
        {
            _subFamilyRepository = subFamilyRepository;
            _familyRepository = familyRepository;
        }

        public Notification Validate(RegisterSubFamilyRequest request, Guid companyId)
        {
            Notification notification = new();

            

            if(request.FamilyId == Guid.Empty)
                notification.AddError(SubFamilyStatic.FamilyMsgErrorRequiered);

            ValidatorString(notification, request.Description, CommonStatic.DescriptionMaxLength, CommonStatic.DescriptionMsgErrorMaxLength, CommonStatic.DescriptionMsgErrorRequiered, true);
            


            if (notification.HasErrors())
            {
                return notification;
            }


            SubFamily? subFamily = _subFamilyRepository.GetbyDescription(request.Description, companyId,request.FamilyId);
            if (subFamily != null)
                notification.AddError(CommonStatic.DescriptionMsgErrorDuplicate);

            subFamily = _subFamilyRepository.GetbyCode(request.Code,companyId);
            if (subFamily != null)
                notification.AddError(CommonStatic.CodeMsgErrorDuplicate);

            Family? family = _familyRepository.GetById(request.FamilyId);
            if (family == null)
                notification.AddError(SubFamilyStatic.FamilyMsgErrorNotFound);

            return notification;
        }
    }
}


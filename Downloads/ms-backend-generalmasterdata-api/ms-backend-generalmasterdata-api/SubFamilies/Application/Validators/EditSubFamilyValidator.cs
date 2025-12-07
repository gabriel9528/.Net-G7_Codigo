using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Validators;
using AnaPrevention.GeneralMasterData.Api.Common.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Families.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Families.Infrastructure.Repositories;
using AnaPrevention.GeneralMasterData.Api.SubFamilies.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.SubFamilies.Application.Static;
using AnaPrevention.GeneralMasterData.Api.SubFamilies.Infrastructure.Repositories;

namespace AnaPrevention.GeneralMasterData.Api.SubFamilies.Application.Validators
{
    public class EditSubFamilyValidator: Validator
    {
        private readonly SubFamilyRepository _subFamilyRepository;
        private readonly FamilyRepository _familyRepository;

        public EditSubFamilyValidator(SubFamilyRepository SubFamilyRepository, FamilyRepository familyRepository)
        {
            _subFamilyRepository = SubFamilyRepository;
            _familyRepository = familyRepository;
        }

        public Notification Validate(EditSubFamilyRequest request, Guid companyId)
        {
            Notification notification = new();

            if (request.Id == Guid.Empty)
                notification.AddError(CommonStatic.IdMsgErrorRequiered);

            if (request.FamilyId == Guid.Empty)
                notification.AddError(SubFamilyStatic.FamilyMsgErrorRequiered);

            ValidatorString(notification, request.Description, CommonStatic.DescriptionMaxLength, CommonStatic.DescriptionMsgErrorMaxLength, CommonStatic.DescriptionMsgErrorRequiered, true);
            ValidatorString(notification, request.Code, CommonStatic.CodeMaxLength, CommonStatic.CodeMsgErrorMaxLength, CommonStatic.CodeMsgErrorRequiered, true);


            if (notification.HasErrors())
            {
                return notification;
            }

            bool descriptionTakenForEdit = _subFamilyRepository.DescriptionTakenForEdit(request.Id, request.Description, companyId,request.FamilyId);

            if (descriptionTakenForEdit)
                notification.AddError(CommonStatic.DescriptionMsgErrorDuplicate);

            bool codeTakenForEdit = _subFamilyRepository. CodeTakenForEdit(request.Id, request.Code, companyId);

            if (codeTakenForEdit)
                notification.AddError(CommonStatic.CodeMsgErrorDuplicate);

            Family? family = _familyRepository.GetById(request.FamilyId);
            if (family == null)
                notification.AddError(SubFamilyStatic.FamilyMsgErrorNotFound);

            return notification;
        }
    }
}


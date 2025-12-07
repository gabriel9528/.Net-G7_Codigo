using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Validators;
using AnaPrevention.GeneralMasterData.Api.Common.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Specialties.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.Specialties.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Specialties.Infrastructure.Repositories;

namespace AnaPrevention.GeneralMasterData.Api.Specialties.Application.Validators
{
    public class EditSpecialtyValidator: Validator
    {
        private readonly SpecialtyRepository _specialtieRepository;

        public EditSpecialtyValidator(SpecialtyRepository specialtieRepository)
        {
            _specialtieRepository = specialtieRepository;
        }

        public Notification Validate(EditSpecialtyRequest request)
        {
            Notification notification = new();

            if (request.Id == Guid.Empty)
                notification.AddError(CommonStatic.IdMsgErrorRequiered);

            ValidatorString(notification, request.Description, CommonStatic.DescriptionMaxLength, CommonStatic.DescriptionMsgErrorMaxLength, CommonStatic.DescriptionMsgErrorRequiered, true);
            ValidatorString(notification, request.Code, CommonStatic.CodeMaxLength, CommonStatic.CodeMsgErrorMaxLength, CommonStatic.CodeMsgErrorRequiered, true);

            if (notification.HasErrors())
            {
                return notification;
            }

            bool descriptionTakenForEdit = _specialtieRepository.DescriptionTakenForEdit(request.Id, request.Description);

            if (descriptionTakenForEdit)
                notification.AddError(CommonStatic.DescriptionMsgErrorDuplicate);

            bool codeTakenForEdit = _specialtieRepository.CodeTakenForEdit(request.Id, request.Code);

            if (codeTakenForEdit)
                notification.AddError(CommonStatic.CodeMsgErrorDuplicate);

            return notification;
        }
    }
}

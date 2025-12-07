using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Validators;
using AnaPrevention.GeneralMasterData.Api.Common.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Specialties.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.Specialties.Entities;
using AnaPrevention.GeneralMasterData.Api.Specialties.Infrastructure.Repositories;

namespace AnaPrevention.GeneralMasterData.Api.Specialties.Application.Validators
{
    public class RegisterSpecialtyValidator: Validator
    {
        private readonly SpecialtyRepository _specialtyRepository;

        public RegisterSpecialtyValidator(SpecialtyRepository specialtieRepository)
        {
            _specialtyRepository = specialtieRepository;
        }

        public Notification Validate(RegisterSpecialtyRequest request)
        {
            Notification notification = new();
            ValidatorString(notification, request.Description, CommonStatic.DescriptionMaxLength, CommonStatic.DescriptionMsgErrorMaxLength, CommonStatic.DescriptionMsgErrorRequiered, true);
           

            if (notification.HasErrors())
            {
                return notification;
            }


            Specialty? specialty = _specialtyRepository.GetbyDescription(request.Description);
            if (specialty != null)
                notification.AddError(CommonStatic.DescriptionMsgErrorDuplicate);

            specialty = _specialtyRepository.GetbyCode(request.Code);
            if (specialty != null)
                notification.AddError(CommonStatic.CodeMsgErrorDuplicate);

            return notification;
        }
    }
}


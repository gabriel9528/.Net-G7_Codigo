using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Validators;
using AnaPrevention.GeneralMasterData.Api.Common.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.MedicalAreas.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.MedicalAreas.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.MedicalAreas.Infrastructure.Repositories;

namespace AnaPrevention.GeneralMasterData.Api.MedicalAreas.Application.Validators
{
    public class RegisterMedicalAreaValidator : Validator
    {
        private readonly MedicalAreaRepository _medicalAreaRepository;

        public RegisterMedicalAreaValidator(MedicalAreaRepository medicalAreaRepository)
        {
            _medicalAreaRepository = medicalAreaRepository;
        }

        public Notification Validate(RegisterMedicalAreaRequest request)
        {
            Notification notification = new();

            ValidatorString(notification, request.Description, CommonStatic.DescriptionMaxLength, CommonStatic.DescriptionMsgErrorMaxLength, CommonStatic.DescriptionMsgErrorRequiered, true);


            if (notification.HasErrors())
            {
                return notification;
            }


            MedicalArea? medicalArea = _medicalAreaRepository.GetbyDescription(request.Description);
            if (medicalArea != null)
                notification.AddError(CommonStatic.DescriptionMsgErrorDuplicate);

            medicalArea = _medicalAreaRepository.GetbyCode(request.Code);
            if (medicalArea != null)
                notification.AddError(CommonStatic.CodeMsgErrorDuplicate);

            return notification;
        }
    }
}

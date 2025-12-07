using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Validators;
using AnaPrevention.GeneralMasterData.Api.Common.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.ServiceCatalogs.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.ServiceCatalogs.Application.Static;
using AnaPrevention.GeneralMasterData.Api.ServiceCatalogs.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.ServiceCatalogs.Infrastructure.Repositories;

namespace AnaPrevention.GeneralMasterData.Api.ServiceCatalogs.Application.Validators
{
    public class RegisterMedicalFormatValidator : Validator
    {
        private readonly MedicalFormatRepository _medicalFormatRepository;

        public RegisterMedicalFormatValidator(MedicalFormatRepository medicalFormatRepository)
        {
            _medicalFormatRepository = medicalFormatRepository;
        }

        public Notification Validate(RegisterMedicalFormatRequest request)
        {
            Notification notification = new();

            ValidatorString(notification, request.Description, CommonStatic.DescriptionMaxLength, CommonStatic.DescriptionMsgErrorMaxLength, CommonStatic.DescriptionMsgErrorRequiered, true);



            if (notification.HasErrors())
            {
                return notification;
            }


            MedicalFormat? medicalFormat = _medicalFormatRepository.GetbyDescription(request.Description);
            if (medicalFormat != null)
                notification.AddError(CommonStatic.DescriptionMsgErrorDuplicate);

            medicalFormat = _medicalFormatRepository.GetbyCode(request.Code);
            if (medicalFormat != null)
                notification.AddError(CommonStatic.CodeMsgErrorDuplicate);

            return notification;
        }
    }
}

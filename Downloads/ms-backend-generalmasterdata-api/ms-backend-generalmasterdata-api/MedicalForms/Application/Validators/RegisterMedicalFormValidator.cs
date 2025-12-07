using AnaPrevention.GeneralMasterData.Api.Common.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Validators;
using AnaPrevention.GeneralMasterData.Api.ServiceCatalogs.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.ServiceCatalogs.Application.Static;
using AnaPrevention.GeneralMasterData.Api.ServiceCatalogs.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.ServiceCatalogs.Infrastructure.Repositories;
using AnaPrevention.GeneralMasterData.Api.ServiceTypes.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.MedicalAreas.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.ServiceTypes.Infrastructure.Repositories;
using AnaPrevention.GeneralMasterData.Api.MedicalAreas.Infrastructure.Repositories;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;

namespace AnaPrevention.GeneralMasterData.Api.ServiceCatalogs.Application.Validators
{
    public class RegisterMedicalFormValidator : Validator
    {
        private readonly MedicalFormRepository _medicalFormRepository;
        private readonly ServiceTypeRepository _serviceTypeRepository;
        private readonly MedicalAreaRepository _medicalAreaRepository;

        public RegisterMedicalFormValidator(MedicalFormRepository medicalFormRepository, ServiceTypeRepository serviceTypeRepository, MedicalAreaRepository medicalAreaRepository)
        {
            _medicalFormRepository = medicalFormRepository;
            _serviceTypeRepository = serviceTypeRepository;
            _medicalAreaRepository = medicalAreaRepository;
        }

        public Notification Validate(RegisterMedicalFormRequest request)
        {
            Notification notification = new();

            if (request.ServiceTypeId == Guid.Empty)
                notification.AddError(MedicalFormStatic.ServiceTypeIdMsgErrorRequiered);
            if (request.MedicalAreaId == Guid.Empty)
                notification.AddError(MedicalFormStatic.MedicalAreaIdMsgErrorRequiered);

            ValidatorString(notification, request.Description, CommonStatic.DescriptionMaxLength, CommonStatic.DescriptionMsgErrorMaxLength, CommonStatic.DescriptionMsgErrorRequiered, true);

            ServiceType? serviceType = _serviceTypeRepository.GetById(request.ServiceTypeId);
            if (serviceType == null)
                notification.AddError(MedicalFormStatic.ServiceTypeIdMsgErrorNotFound);

            MedicalArea? medicalArea = _medicalAreaRepository.GetById(request.MedicalAreaId);
            if (medicalArea == null)
                notification.AddError(MedicalFormStatic.MedicalAreaIdMsgErrorNotFound);


            if (notification.HasErrors())
                return notification;

            MedicalForm? medicalForm = _medicalFormRepository.GetbyDescription(request.Description);
            if (medicalForm != null)
                notification.AddError(CommonStatic.DescriptionMsgErrorDuplicate);

            ;

            return notification;
        }
    }
}

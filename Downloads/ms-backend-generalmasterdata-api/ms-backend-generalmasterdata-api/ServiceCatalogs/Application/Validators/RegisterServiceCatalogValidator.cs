using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Validators;
using AnaPrevention.GeneralMasterData.Api.Common.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.ExistenceTypes.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.ExistenceTypes.Infrastructure.Repositories;
using AnaPrevention.GeneralMasterData.Api.MeasurementUnits.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.MeasurementUnits.Infrastructure.Repositories;
using AnaPrevention.GeneralMasterData.Api.MedicalAreas.Application.Static;
using AnaPrevention.GeneralMasterData.Api.MedicalAreas.Infrastructure.Repositories;
using AnaPrevention.GeneralMasterData.Api.ServiceCatalogs.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.ServiceCatalogs.Application.Static;
using AnaPrevention.GeneralMasterData.Api.ServiceCatalogs.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.ServiceCatalogs.Domain.Enums;
using AnaPrevention.GeneralMasterData.Api.ServiceCatalogs.Infrastructure.Repositories;
using AnaPrevention.GeneralMasterData.Api.ServiceTypes.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.ServiceTypes.Infrastructure.Repositories;
using AnaPrevention.GeneralMasterData.Api.SubFamilies.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.SubFamilies.Infrastructure.Repositories;
using AnaPrevention.GeneralMasterData.Api.Taxes.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Taxes.Infrastructure.Repositories;

namespace AnaPrevention.GeneralMasterData.Api.ServiceCatalogs.Application.Validators
{
    public class RegisterServiceCatalogValidator : Validator
    {
        private readonly ServiceCatalogRepository _serviceCatalogRepository;
        private readonly UomRepository _uomRepository;
        private readonly ExistenceTypeRepository _existenceTypeRepository;
        private readonly TaxRepository _taxRepository;
        private readonly SubFamilyRepository _subFamilyRepository;
        private readonly ServiceTypeRepository _serviceTypeRepository;
        private readonly MedicalFormRepository _medicalFormRepository;
        private readonly MedicalAreaRepository _medicalAreaRepository;

        public RegisterServiceCatalogValidator(
            ServiceCatalogRepository serviceCatalogRepository,
            UomRepository uomRepository,
            ExistenceTypeRepository existenceTypeRepository,
            TaxRepository taxRepository,
            SubFamilyRepository subFamilyRepository,
            ServiceTypeRepository serviceTypeRepository
,
            MedicalFormRepository medicalFormRepository,
            MedicalAreaRepository medicalAreaRepository)
        {
            _serviceCatalogRepository = serviceCatalogRepository;
            _uomRepository = uomRepository;
            _existenceTypeRepository = existenceTypeRepository;
            _taxRepository = taxRepository;
            _subFamilyRepository = subFamilyRepository;
            _serviceTypeRepository = serviceTypeRepository;
            _medicalFormRepository = medicalFormRepository;
            _medicalAreaRepository = medicalAreaRepository;
        }

        public Notification Validate(RegisterServiceCatalogRequest request)
        {
            Notification notification = new();

            if (request.ExistenceTypeId == Guid.Empty)
                notification.AddError(ServiceCatalogStatic.ExistenceTypeIdMsgErrorRequiered);
            if (request.SubFamilyId == Guid.Empty)
                notification.AddError(ServiceCatalogStatic.SubFamilyIdMsgErrorRequiered);
            if (request.TaxId == Guid.Empty)
                notification.AddError(ServiceCatalogStatic.TaxIdMsgErrorRequiered);
            if (request.UomId == Guid.Empty)
                notification.AddError(ServiceCatalogStatic.UomIdMsgErrorRequiered);
            if (request.UomSecondId == Guid.Empty)
                notification.AddError(ServiceCatalogStatic.UomSecondIdMsgErrorRequiered);

            if (notification.HasErrors())
            {
                return notification;
            }
            ValidatorString(notification, request.Description, CommonStatic.DescriptionMaxLength, CommonStatic.DescriptionMsgErrorMaxLength, CommonStatic.DescriptionMsgErrorRequiered, true);


            string codeSecond = string.IsNullOrWhiteSpace(request.CodeSecond) ? "" : request.CodeSecond.Trim();


            if (codeSecond.Length > ServiceCatalogStatic.CodeSecondMaxLength)
                notification.AddError(String.Format(ServiceCatalogStatic.CodeSecondMsgErrorMaxLength, ServiceCatalogStatic.CodeSecondMaxLength.ToString()));

            if (request.ListServiceTypes != null)
            {
                foreach (Guid serviceTypeId in request.ListServiceTypes)
                {
                    ServiceType? serviceType = _serviceTypeRepository.GetById(serviceTypeId);
                    if (serviceType == null)
                        notification.AddError(ServiceCatalogStatic.ServiceTypemsgErrorNoFound);
                }
            }


            if (notification.HasErrors())
            {
                return notification;
            }

            ServiceCatalog? serviceCatalog = _serviceCatalogRepository.GetbyDescription(request.Description);
            if (serviceCatalog != null)
                notification.AddError(CommonStatic.DescriptionMsgErrorDuplicate);

            serviceCatalog = _serviceCatalogRepository.GetbyCode(request.Code);
            if (serviceCatalog != null)
                notification.AddError(CommonStatic.CodeMsgErrorDuplicate);

            Uom? uom = _uomRepository.GetById(request.UomId);
            if (uom == null)
                notification.AddError(ServiceCatalogStatic.UomMsgErrorNoFound);

            Uom? uomSecond = _uomRepository.GetById(request.UomSecondId);
            if (uomSecond == null)
                notification.AddError(ServiceCatalogStatic.UomSecondMsgErrorNoFound);

            ExistenceType? existenceType = _existenceTypeRepository.GetById(request.ExistenceTypeId);
            if (existenceType == null)
                notification.AddError(ServiceCatalogStatic.ExistenceTypeMsgErrorNoFound);

            Tax? tax = _taxRepository.GetById(request.TaxId);
            if (tax == null)
                notification.AddError(ServiceCatalogStatic.TaxMsgErrorNoFound);

            SubFamily? subFamily = _subFamilyRepository.GetById(request.SubFamilyId);
            if (subFamily == null)
                notification.AddError(ServiceCatalogStatic.SubFamilyMsgErrorNoFound);

            if (request.ListMedicalFormIds != null)
            {
                var medicalForm = _medicalFormRepository.GetbyMedicalFormsType(MedicalFormsType.OCCUPATIONAL_LABORATORY);
                if (medicalForm != null)
                {
                    Guid? medicalFormLaboratoryId = request.ListMedicalFormIds.Where(t1 => t1 == medicalForm.Id).SingleOrDefault();
                    if (medicalFormLaboratoryId != null && medicalFormLaboratoryId != Guid.Empty)
                    {
                        notification.AddError(ServiceCatalogStatic.MedicalFormLaboratoryMsgError);
                    }
                }
            }

            if (request.MedicalAreaId != null)
            {
                var medicalArea = _medicalAreaRepository.GetById((Guid)request.MedicalAreaId);

                if (medicalArea == null)
                {
                    notification.AddError(MedicalAreaStatic.MedicalAreaIdMsgErrorNotFound);
                }
            }

            return notification;
        }
    }
}
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
using AnaPrevention.GeneralMasterData.Api.ServiceCatalogs.Infrastructure.Repositories;
using AnaPrevention.GeneralMasterData.Api.ServiceTypes.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.ServiceTypes.Infrastructure.Repositories;
using AnaPrevention.GeneralMasterData.Api.SubFamilies.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.SubFamilies.Infrastructure.Repositories;
using AnaPrevention.GeneralMasterData.Api.Taxes.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Taxes.Infrastructure.Repositories;

namespace AnaPrevention.GeneralMasterData.Api.ServiceCatalogs.Application.Validators
{
    public class EditServiceCatalogValidator : Validator
    {
        private readonly ServiceCatalogRepository _serviceCatalogRepository;
        private readonly UomRepository _uomRepository;
        private readonly ExistenceTypeRepository _existenceTypeRepository;
        private readonly TaxRepository _taxRepository;
        private readonly SubFamilyRepository _subFamilyRepository;
        private readonly ServiceTypeRepository _serviceTypeRepository;
        private readonly MedicalAreaRepository _medicalAreaRepository;
        public EditServiceCatalogValidator(
             ServiceCatalogRepository serviceCatalogRepository,
            UomRepository uomRepository,
            ExistenceTypeRepository existenceTypeRepository,
            TaxRepository taxRepository,
            SubFamilyRepository subFamilyRepository,
            ServiceTypeRepository serviceTypeRepository
,
            MedicalAreaRepository medicalAreaRepository)
        {
            _serviceCatalogRepository = serviceCatalogRepository;
            _uomRepository = uomRepository;
            _existenceTypeRepository = existenceTypeRepository;
            _taxRepository = taxRepository;
            _subFamilyRepository = subFamilyRepository;
            _serviceTypeRepository = serviceTypeRepository;
            _medicalAreaRepository = medicalAreaRepository;
        }

        public Notification Validate(EditServiceCatalogRequest request)
        {
            Notification notification = new();

            if (request.Id == Guid.Empty)
                notification.AddError(CommonStatic.IdMsgErrorRequiered);

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
            ValidatorString(notification, request.Code, CommonStatic.CodeMaxLength, CommonStatic.CodeMsgErrorMaxLength, CommonStatic.CodeMsgErrorRequiered, true);

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

            bool descriptionTakenForEdit = _serviceCatalogRepository.DescriptionTakenForEdit(request.Id, request.Description);

            if (descriptionTakenForEdit)
                notification.AddError(CommonStatic.DescriptionMsgErrorDuplicate);



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

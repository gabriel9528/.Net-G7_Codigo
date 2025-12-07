using CSharpFunctionalExtensions;
using AnaPrevention.GeneralMasterData.Api.Common.API;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Common.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Common.Infrastructure.EF;
using AnaPrevention.GeneralMasterData.Api.ServiceCatalogs.Infrastructure.Repositories;
using AnaPrevention.GeneralMasterData.Api.ServiceCatalogs.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.ServiceCatalogs.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.ServiceCatalogs.Application.Validators;
using System.Transactions;
using AnaPrevention.GeneralMasterData.Api.Fields.Application.Dtos.Fields;
using AnaPrevention.GeneralMasterData.Api.ServiceTypes.Infrastructure.Repositories;
using AnaPrevention.GeneralMasterData.Api.ServiceTypes.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Lines.Infrastructure.Repositories;
using AnaPrevention.GeneralMasterData.Api.Families.Infrastructure.Repositories;
using AnaPrevention.GeneralMasterData.Api.SubFamilies.Infrastructure.Repositories;
using AnaPrevention.GeneralMasterData.Api.MedicalAreas.Infrastructure.Repositories;

namespace AnaPrevention.GeneralMasterData.Api.ServiceCatalogs.Application.Services
{
    public class ServiceCatalogApplicationService
    {
        private readonly AnaPreventionContext _context;
        private readonly RegisterServiceCatalogValidator _registerServiceCatalogValidator;
        private readonly EditServiceCatalogValidator _editServiceCatalogValidator;
        private readonly ServiceCatalogRepository _serviceCatalogRepository;
        private readonly ServiceCatalogServiceTypeRepository _serviceCatalogServiceTypeRepository;
        private readonly ServiceCatalogMedicalFormRepository _serviceCatalogMedicalFormRepository;
        private readonly ServiceCatalogFieldRepository _serviceCatalogFieldRepository;
        private readonly MedicalFormRepository _medicalFormRepository;
        private readonly ServiceTypeRepository _serviceTypeRepository;
        private readonly LineRepository _lineRepository;
        private readonly FamilyRepository _familyRepository;
        private readonly MedicalAreaRepository _medicalArealRepository;
        private readonly SubFamilyRepository _subFamilyRepository;

        public ServiceCatalogApplicationService(
       AnaPreventionContext context,
       RegisterServiceCatalogValidator registerServiceCatalogValidator,
       EditServiceCatalogValidator editServiceCatalogValidator,
       ServiceCatalogRepository serviceCatalogRepository,
       ServiceCatalogServiceTypeRepository serviceCatalogServiceTypeRepository, ServiceCatalogMedicalFormRepository serviceCatalogMedicalFormRepository, ServiceCatalogFieldRepository serviceCatalogFieldRepository, MedicalFormRepository medicalFormRepository, ServiceTypeRepository serviceTypeRepository, LineRepository lineRepository, FamilyRepository familyRepository, SubFamilyRepository subFamilyRepository, MedicalAreaRepository medicalArealRepository)
        {
            _context = context;
            _registerServiceCatalogValidator = registerServiceCatalogValidator;
            _editServiceCatalogValidator = editServiceCatalogValidator;
            _serviceCatalogRepository = serviceCatalogRepository;
            _serviceCatalogServiceTypeRepository = serviceCatalogServiceTypeRepository;
            _serviceCatalogMedicalFormRepository = serviceCatalogMedicalFormRepository;
            _serviceCatalogFieldRepository = serviceCatalogFieldRepository;
            _medicalFormRepository = medicalFormRepository;
            _serviceTypeRepository = serviceTypeRepository;
            _lineRepository = lineRepository;
            _familyRepository = familyRepository;
            _subFamilyRepository = subFamilyRepository;
            _medicalArealRepository = medicalArealRepository;
        }

        public Result<RegisterServiceCatalogResponse, Notification> RegisterServiceCatalog(RegisterServiceCatalogRequest request, Guid userId)
        {
            Notification notification = _registerServiceCatalogValidator.Validate(request);

            if (notification.HasErrors())
                return notification;


            string description = request.Description.Trim();
            string code = GenerateCode(request.SubFamilyId);
            string codeSecond = request.CodeSecond.Trim();
            Guid subFamilyId = request.SubFamilyId;
            Guid uomId = request.UomId;
            Guid uomSecondId = request.UomSecondId;
            Guid existenceTypeId = request.ExistenceTypeId;
            Guid? medicalAreaId = request.MedicalAreaId;
            Guid taxId = request.TaxId;
            bool isActive = request.IsActive;
            bool isSales = request.IsSales;
            bool isBuy = request.IsBuy;
            bool isInventory = request.IsInventory;
            bool isRetention = request.IsRetention;
            string comment = request.Comment.Trim();
            var ListServiceTypeId = request.ListServiceTypes;
            var listMedicalFormIds = request.ListMedicalFormIds;
            var lisFieldIds = request.LisFieldIds;
            int orderRow = request.OrderRow;
            int orderRowTourSheet = request.OrderRowTourSheet;

            ServiceCatalog serviceCatalog = new(
                description,
                code,
                codeSecond,
                subFamilyId,
                uomId,
                uomSecondId,
                existenceTypeId,
                taxId,
                isBuy,
                isSales,
                isActive,
                isInventory,
                isRetention,
                comment,
                Guid.NewGuid(),
                orderRow,
                orderRowTourSheet,
                medicalAreaId);

            using (var scope = new TransactionScope())
            {
                _serviceCatalogRepository.Save(serviceCatalog);
                _context.SaveChangesNoScope(userId);

                if (ListServiceTypeId != null)
                {
                    foreach (var serviceTypeId in ListServiceTypeId)
                    {
                        _serviceCatalogServiceTypeRepository.Save(new ServiceCatalogServiceType(serviceCatalog.Id, serviceTypeId, Guid.NewGuid()));
                    }
                }

                if (listMedicalFormIds != null)
                {
                    foreach (var medicalFormId in listMedicalFormIds)
                    {
                        _serviceCatalogMedicalFormRepository.Save(new ServiceCatalogMedicalForm(medicalFormId, serviceCatalog.Id, Guid.NewGuid()));
                    }
                }

                if (lisFieldIds != null)
                {
                    foreach (var fieldId in lisFieldIds)
                    {
                        _serviceCatalogFieldRepository.Save(new ServiceCatalogField(serviceCatalog.Id, fieldId, Guid.NewGuid()));
                    }
                }

                _context.SaveChangesNoScope(userId);
                scope.Complete();
            }
            var response = new RegisterServiceCatalogResponse
            {
                Id = serviceCatalog.Id,
                Description = serviceCatalog.Description,
                Code = serviceCatalog.Code,
                CodeSecond = serviceCatalog.CodeSecond,
                SubFamilyId = serviceCatalog.SubFamilyId,
                UomId = serviceCatalog.UomId,
                UomSecondId = serviceCatalog.UomSecondId,
                ExistenceTypeId = serviceCatalog.ExistenceTypeId,
                TaxId = serviceCatalog.TaxId,
                IsActive = serviceCatalog.IsActive,
                IsSales = serviceCatalog.IsSales,
                IsBuy = serviceCatalog.IsBuy,
                IsInventory = serviceCatalog.IsInventory,
                IsRetention = serviceCatalog.IsRetention,
                Comment = serviceCatalog.Comment,
                Status = serviceCatalog.Status,
            };

            return response;
        }

        public string GenerateCode(Guid subFamilyId)
        {
            return _serviceCatalogRepository.GenerateCode(subFamilyId);
        }

        public EditServiceCatalogResponse EditServiceCatalog(EditServiceCatalogRequest request, ServiceCatalog serviceCatalog, Guid userId)
        {
            serviceCatalog.Description = request.Description.Trim();
            serviceCatalog.Code = request.Code.Trim();
            serviceCatalog.CodeSecond = request.CodeSecond.Trim();
            serviceCatalog.SubFamilyId = request.SubFamilyId;
            serviceCatalog.UomId = request.UomId;
            serviceCatalog.UomSecondId = request.UomSecondId;
            serviceCatalog.ExistenceTypeId = request.ExistenceTypeId;
            serviceCatalog.TaxId = request.TaxId;
            serviceCatalog.IsActive = request.IsActive;
            serviceCatalog.IsSales = request.IsSales;
            serviceCatalog.MedicalAreaId = request.MedicalAreaId;
            serviceCatalog.IsBuy = request.IsBuy;
            serviceCatalog.IsInventory = request.IsInventory;
            serviceCatalog.IsRetention = request.IsRetention;
            serviceCatalog.Comment = request.Comment.Trim();

            var ListServiceTypeId = request.ListServiceTypes;
            var ListMedicalFormIds = request.ListMedicalFormIds;
            var lisFieldIds = request.LisFieldIds;

            if (ListServiceTypeId != null)
            {
                List<ServiceType>? ListServiceType = _serviceTypeRepository.GetListServiceTypeByServiceCatalogId(serviceCatalog.Id);
                if (ListServiceType != null)
                {
                    List<ServiceCatalogServiceType>? ListSubsidiaries = _serviceCatalogServiceTypeRepository.GetServiceTypesByServiceCatalog(serviceCatalog.Id);

                    if (ListSubsidiaries?.Count > 0)
                        _serviceCatalogServiceTypeRepository.RemoveServicCatalogServiceTypeRange(ListSubsidiaries, userId);
                    foreach (var serviceTypeId in ListServiceTypeId)
                    {
                        ServiceType? serviceType = ListServiceType.Find(x => x.Id == serviceTypeId);

                        if (serviceType == null)
                            _serviceCatalogServiceTypeRepository.Save(new ServiceCatalogServiceType(serviceCatalog.Id, serviceTypeId, Guid.NewGuid()));
                    }
                }
            }

            if (ListMedicalFormIds != null)
            {
                List<MedicalForm>? ListMedicalForm = _medicalFormRepository.GetListMedicalFormByServiceCatalogId(serviceCatalog.Id);
                if (ListMedicalForm != null)
                {
                    foreach (MedicalForm medicalForm in ListMedicalForm)
                    {
                        Guid? medicalFormId = ListMedicalFormIds.Find(x => x == medicalForm.Id);

                        if (medicalFormId == null || medicalFormId == Guid.Empty)
                        {
                            var serviceCatalogMedicalForm = _serviceCatalogMedicalFormRepository.GetByServicesCatalogAndMedicalFormId(serviceCatalog.Id, medicalForm.Id);
                            if (serviceCatalogMedicalForm != null)
                                _serviceCatalogMedicalFormRepository.Remove(serviceCatalogMedicalForm);
                        }
                    }

                    foreach (var medicalFormId in ListMedicalFormIds)
                    {
                        MedicalForm? medicalForm = ListMedicalForm.Find(x => x.Id == medicalFormId);

                        if (medicalForm == null)
                            _serviceCatalogMedicalFormRepository.Save(new ServiceCatalogMedicalForm(medicalFormId, serviceCatalog.Id, Guid.NewGuid()));
                    }
                }
            }

            if (lisFieldIds != null)
            {
                List<FieldLaboratoryDto>? ListfieldLaboratoryDto = _serviceCatalogFieldRepository.GetFieldLaboratoryByServiceCatalogId(serviceCatalog.Id);
                if (ListfieldLaboratoryDto != null)
                {
                    foreach (FieldLaboratoryDto fieldLaboratoryDto in ListfieldLaboratoryDto)
                    {
                        Guid? fieldId = lisFieldIds.Find(x => x == fieldLaboratoryDto.Id);

                        if (fieldId == null)
                        {
                            var serviceCatalogField = _serviceCatalogFieldRepository.GetByServicesCatalogAndFieldId(serviceCatalog.Id, fieldLaboratoryDto.Id);
                            if (serviceCatalogField != null)
                                _serviceCatalogFieldRepository.Remove(serviceCatalogField);
                        }
                    }

                    foreach (var fieldId in lisFieldIds)
                    {
                        FieldLaboratoryDto? fieldLaboratory = ListfieldLaboratoryDto.Find(x => x.Id == fieldId);

                        if (fieldLaboratory == null)
                            _serviceCatalogFieldRepository.Save(new ServiceCatalogField(serviceCatalog.Id, fieldId, Guid.NewGuid()));
                    }
                }
            }

            _context.SaveChanges(userId);

            var response = new EditServiceCatalogResponse
            {
                Id = serviceCatalog.Id,
                Description = serviceCatalog.Description,
                Code = serviceCatalog.Code,
                CodeSecond = serviceCatalog.CodeSecond,
                SubFamilyId = serviceCatalog.SubFamilyId,
                UomId = serviceCatalog.UomId,
                UomSecondId = serviceCatalog.UomSecondId,
                ExistenceTypeId = serviceCatalog.ExistenceTypeId,
                TaxId = serviceCatalog.TaxId,
                IsActive = serviceCatalog.IsActive,
                IsSales = serviceCatalog.IsSales,
                IsBuy = serviceCatalog.IsBuy,
                IsInventory = serviceCatalog.IsInventory,
                IsRetention = serviceCatalog.IsRetention,
                Comment = serviceCatalog.Comment,
                Status = serviceCatalog.Status,
            };

            return response;
        }

        public EditServiceCatalogResponse ActiveServiceCatalog(ServiceCatalog serviceCatalog, Guid userId)
        {
            serviceCatalog.Status = true;

            _context.SaveChanges(userId);

            var response = new EditServiceCatalogResponse
            {
                Id = serviceCatalog.Id,
                Description = serviceCatalog.Description,
                Code = serviceCatalog.Code,
                CodeSecond = serviceCatalog.CodeSecond,
                SubFamilyId = serviceCatalog.SubFamilyId,
                UomId = serviceCatalog.UomId,
                UomSecondId = serviceCatalog.UomSecondId,
                ExistenceTypeId = serviceCatalog.ExistenceTypeId,
                TaxId = serviceCatalog.TaxId,
                IsActive = serviceCatalog.IsActive,
                IsSales = serviceCatalog.IsSales,
                IsBuy = serviceCatalog.IsBuy,
                IsInventory = serviceCatalog.IsInventory,
                IsRetention = serviceCatalog.IsRetention,
                Comment = serviceCatalog.Comment,
                Status = serviceCatalog.Status,
            };

            return response;
        }
        public Notification ValidateEditServiceCatalogRequest(EditServiceCatalogRequest request)
        {
            return _editServiceCatalogValidator.Validate(request);
        }

        public EditServiceCatalogResponse RemoveServiceCatalog(ServiceCatalog serviceCatalog, Guid userId)
        {
            serviceCatalog.Status = false;
            _context.SaveChanges(userId);

            var response = new EditServiceCatalogResponse
            {
                Id = serviceCatalog.Id,
                Description = serviceCatalog.Description,
                Code = serviceCatalog.Code,
                CodeSecond = serviceCatalog.CodeSecond,
                SubFamilyId = serviceCatalog.SubFamilyId,
                UomId = serviceCatalog.UomId,
                UomSecondId = serviceCatalog.UomSecondId,
                ExistenceTypeId = serviceCatalog.ExistenceTypeId,
                TaxId = serviceCatalog.TaxId,
                IsActive = serviceCatalog.IsActive,
                IsSales = serviceCatalog.IsSales,
                IsBuy = serviceCatalog.IsBuy,
                IsInventory = serviceCatalog.IsInventory,
                IsRetention = serviceCatalog.IsRetention,
                Comment = serviceCatalog.Comment,
                Status = serviceCatalog.Status,
            };

            return response;
        }

        public bool RegisterOrderRowTourSheet(List<RegisterServiceCatalogOrderRowRequest> medicalAreas, Guid userId)
        {
            foreach (var medicalArea in medicalAreas)
            {
                RegisterOrderRowAll(medicalArea);
                var ServiceCatalogs = medicalArea.Sons;
                if (ServiceCatalogs != null)
                {
                    foreach (var ServiceCatalog in ServiceCatalogs)
                    {
                        RegisterOrderRowAll(ServiceCatalog, true);
                    }
                }
            }
            _context.SaveChanges(userId);

            return true;
        }

        public bool RegisterOrderRow(List<RegisterServiceCatalogOrderRowRequest> lines, Guid userId)
        {
            if (lines != null)
            {
                foreach (var line in lines)
                {
                    RegisterOrderRowAll(line);
                    var families = line.Sons;
                    if (families != null)
                    {
                        foreach (var family in families)
                        {
                            RegisterOrderRowAll(family);
                            var subFamilies = family.Sons;
                            if (subFamilies != null)
                            {
                                foreach (var subFamily in subFamilies)
                                {
                                    RegisterOrderRowAll(subFamily);
                                    var ServiceCatalogs = subFamily.Sons;
                                    if (ServiceCatalogs != null)
                                    {
                                        foreach (var ServiceCatalog in ServiceCatalogs)
                                        {
                                            RegisterOrderRowAll(ServiceCatalog);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            _context.SaveChanges(userId);
            return true;
        }

        private bool RegisterOrderRowAll(RegisterServiceCatalogOrderRowRequest request, bool isOrderTourSheet = false)
        {

            switch (request.OrderEntityType)
            {
                case Domain.Enums.OrderEntityType.LINE:
                    {
                        var line = _lineRepository.GetById(request.Id);
                        if (line != null)
                            line.OrderRow = request.OrderRow;
                        break;
                    }

                case Domain.Enums.OrderEntityType.FAMILY:
                    {
                        var family = _familyRepository.GetById(request.Id);
                        if (family != null)
                            family.OrderRow = request.OrderRow;
                        break;
                    }

                case Domain.Enums.OrderEntityType.SUBFAMILY:
                    {
                        var subfamily = _subFamilyRepository.GetById(request.Id);
                        if (subfamily != null)
                            subfamily.OrderRow = request.OrderRow;
                        break;
                    }

                case Domain.Enums.OrderEntityType.SERVICE_CATALOG:
                    {
                        var serviceCatalog = _serviceCatalogRepository.GetById(request.Id);
                        if (serviceCatalog != null)
                        {
                            if (isOrderTourSheet)
                                serviceCatalog.OrderRowTourSheet = request.OrderRow;
                            else
                                serviceCatalog.OrderRow = request.OrderRow;

                        }

                        break;
                    }

                case Domain.Enums.OrderEntityType.MEDICAL_AREA:
                    {
                        var medicalArea = _medicalArealRepository.GetById(request.Id);
                        if (medicalArea != null)
                            medicalArea.OrderRowTourSheet = request.OrderRow;
                        break;
                    }
            }

            return true;
        }

        public ServiceCatalog? GetById(Guid id)
        {
            return _serviceCatalogRepository.GetById(id);
        }

        public ServiceCatalogDto? GetDtoById(Guid id)
        {
            return _serviceCatalogRepository.GetDtoById(id);

        }

        public List<ServiceCatalogDto> GetListAllBySubFamilyId(Guid familyId)
        {
            return _serviceCatalogRepository.GetListAllBySubFamilyId(familyId);
        }

        public List<ServiceCatalogDto> GetListAll(string description = "")
        {
            return _serviceCatalogRepository.GetListAll(description);
        }
        public List<ServiceCatalogBasicDto> GetListAllExams()
        {
            return _serviceCatalogRepository.GetListAllExams();
        }

        public List<ItemOrdenRowDto> GetOrderRowTourSheet()
        {
            return _serviceCatalogRepository.GetOrderRowTourSheet();
        }

        public List<ItemOrdenRowDto> GetOrderRow()
        {
            return _serviceCatalogRepository.GetOrderRow();
        }

        public List<ServiceCatalogMinDto>? GetMinDtoForLaboratoryFilter(string? description)
        {
            return _serviceCatalogRepository.GetMinDtoForLaboratoryFilter(description);
        }
        public Tuple<IEnumerable<ServiceCatalogDto>, PaginationMetadata> GetList(int pageNumber, int pageSize, bool status, string descriptionSearch = "", string codeSearch = "")
        {
            return _serviceCatalogRepository.GetList(pageNumber, pageSize, status, descriptionSearch, codeSearch);
        }
    }
}

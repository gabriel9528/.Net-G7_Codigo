using CSharpFunctionalExtensions;
using AnaPrevention.GeneralMasterData.Api.Common.API;
using AnaPrevention.GeneralMasterData.Api.Common.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Common.Infrastructure.EF;
using AnaPrevention.GeneralMasterData.Api.Fields.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.Fields.Application.Dtos.Fields;
using AnaPrevention.GeneralMasterData.Api.Fields.Application.Validators;
using AnaPrevention.GeneralMasterData.Api.Fields.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Fields.Domain.Enums;
using AnaPrevention.GeneralMasterData.Api.Fields.Infrastructure.Repositories;
using AnaPrevention.GeneralMasterData.Api.ServiceCatalogs.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.ServiceCatalogs.Infrastructure.Repositories;
using System.Text.Json;
using System.Transactions;
using AnaPrevention.GeneralMasterData.Api.ServiceCatalogs.Domain.Enums;

namespace AnaPrevention.GeneralMasterData.Api.Fields.Application.Services
{
    public class FieldApplicationService
    {
        private readonly AnaPreventionContext _context;
        private readonly FieldRepository _fieldRepository;
        private readonly RegisterFieldValidator _registerFieldValidator;
        private readonly EditFieldValidator _editFieldValidator;
        private readonly ServiceCatalogFieldRepository _serviceCatalogFieldRepository;
        

        public FieldApplicationService(FieldRepository fieldRepository, RegisterFieldValidator registerFieldValidator, AnaPreventionContext context, EditFieldValidator editFieldValidator, ServiceCatalogFieldRepository serviceCatalogFieldRepository)
        {
            _fieldRepository = fieldRepository;
            _registerFieldValidator = registerFieldValidator;
            _context = context;
            _editFieldValidator = editFieldValidator;
            
            _serviceCatalogFieldRepository = serviceCatalogFieldRepository;
            
        }

        public Result<RegisterFieldResponse, Notification> RegisterField(RegisterFieldRequest request, Guid userId)
        {
            Notification notification = _registerFieldValidator.Validate(request);

            if (notification.HasErrors())
                return notification;

            var countFields = 0;//_laboratoryRepository.GetLaboratoryItemAllOrderCount();

            string description = request.Description;
            string code = _fieldRepository.GenerateCode();
            string name = Guid.NewGuid().ToString();
            string uom = request.Uom;
            string legend = request.Legend;
            FieldType fieldType = request.FieldType;
            int orderRow = countFields;
            FieldLabelType isTittle = request.IsTittle;
            List<OptionFieldDto>? options = request.Options;

            string optionsJson = "";
            if (options != null)
                optionsJson = JsonSerializer.Serialize(options);


            var referenceValues = request.ReferenceValues;
            var secondCode = request.SecondCode;

            string referenceValuesJson = "";
            if (referenceValues != null)
                referenceValuesJson = JsonSerializer.Serialize(referenceValues);


            List<Guid>? listServiceCatalogIds = request.ListServiceCatalogIds;

            Field field = new(description, code, name, uom, legend, fieldType, orderRow, optionsJson, referenceValuesJson, Guid.NewGuid(), secondCode, true, isTittle);

            using (var scope = new TransactionScope())
            {
                _fieldRepository.Save(field);

                _context.SaveChangesNoScope(userId);

                if (listServiceCatalogIds != null)
                {
                    countFields++;
                    foreach (var serviceCatalogId in listServiceCatalogIds)
                    {
                        _serviceCatalogFieldRepository.Save(new ServiceCatalogField(serviceCatalogId, field.Id, Guid.NewGuid(), countFields));
                    }
                }

                _context.SaveChangesNoScope(userId);
                scope.Complete();
            }


            var response = new RegisterFieldResponse
            {
                Id = field.Id,
                Description = field.Description,
                Code = field.Code,
                Name = field.Name,
                Uom = field.Uom,
                Legend = field.Legend,
                FieldType = field.FieldType,
                OrderRow = field.OrderRow,
            };
            return response;
        }
        public EditFieldResponse EditField(EditFieldRequest request, Field field, Guid userId)
        {
            field.Description = request.Description;
            field.Uom = request.Uom;
            field.Legend = request.Legend;
            field.FieldType = request.FieldType;
            field.SecondCode = request.SecondCode;
            field.IsTittle = request.IsTittle;

            List<OptionFieldDto>? options = request.Options;

            string optionsJson = "";
            if (options != null)
            {
                optionsJson = JsonSerializer.Serialize(options);
            }
            field.OptionsJson = optionsJson;

            var referenceValues = request.ReferenceValues;

            string referenceValuesJson = "";
            if (referenceValues != null)
                referenceValuesJson = JsonSerializer.Serialize(referenceValues);

            field.ReferenceValuesJson = referenceValuesJson;


            List<Guid>? requestServiceCatalogIds = request.ListServiceCatalogIds;


            List<ServiceCatalogField> listServiceCatFields = _serviceCatalogFieldRepository.GetServiceCatalogFieldByFieldId(field.Id);

            if (listServiceCatFields != null)
            {
                foreach (ServiceCatalogField serviceCatalogField in listServiceCatFields)
                {
                    bool existService = requestServiceCatalogIds.Where(id => id == serviceCatalogField.ServiceCatalogId).Any();

                    if (existService == false)
                    {
                        _context.Remove(serviceCatalogField);
                    }
                }
            }

            if (requestServiceCatalogIds != null)
            {
                var countFields = 0;//_laboratoryRepository.GetLaboratoryItemAllOrderCount();
                foreach (Guid serviceCatalogId in requestServiceCatalogIds)
                {
                    bool? existService = listServiceCatFields?.Where(t1 => t1.ServiceCatalogId == serviceCatalogId).Any();
                    if (existService != true)
                    {
                        _serviceCatalogFieldRepository.Save(new ServiceCatalogField(serviceCatalogId, field.Id, Guid.NewGuid(), countFields));
                        countFields++;
                    }
                }
            }


            _context.SaveChanges(userId);

            var response = new EditFieldResponse
            {
                Id = field.Id,
                Description = field.Description,
                Code = field.Code,
                Name = field.Name,
                Uom = field.Uom,
                Legend = field.Legend,
                FieldType = field.FieldType,
                OrderRow = field.OrderRow,
                SecondCode = field.SecondCode ?? "",

            };

            return response;
        }
        public List<FieldDto> GetListByMedicalFormId(Guid medicalFormId)
        {
            return _fieldRepository.GetListByMedicalFormId(medicalFormId);
        }

        public EditFieldResponse ActiveField(Field field, Guid userId)
        {
            field.Status = true;

            _context.SaveChanges(userId);

            var response = new EditFieldResponse
            {
                Id = field.Id,
                Description = field.Description,
                Code = field.Code,
                Name = field.Name,
                Uom = field.Uom,
                Legend = field.Legend,
                FieldType = field.FieldType,
                OrderRow = field.OrderRow,
            };

            return response;
        }
        public Notification ValidateEditFieldRequest(EditFieldRequest request)
        {
            return _editFieldValidator.Validate(request);
        }

        public EditFieldResponse RemoveField(Field field, Guid userId)
        {
            field.Status = false;
            _context.SaveChanges(userId);

            var response = new EditFieldResponse
            {
                Id = field.Id,
                Description = field.Description,
                Code = field.Code,
                Name = field.Name,
                Uom = field.Uom,
                Legend = field.Legend,
                FieldType = field.FieldType,
                OrderRow = field.OrderRow,
            };

            return response;
        }

        public Field? GetById(Guid id)
        {
            return _fieldRepository.GetById(id);
        }
        public FieldLaboratoryDto? GetFieldLaboratoryDtoById(Guid id)
        {
            return _fieldRepository.GetFieldLaboratoryDtoById(id);
        }

        public List<Field> GetListAll()
        {
            return _fieldRepository.GetListAll();
        }

        public async Task<Tuple<IEnumerable<FieldLaboratoryDto>, PaginationMetadata>> GetListLaboratory(int pageNumber, int pageSize, bool status, string? descriptionSearch, string? codeSearch, int? typeFields, string? typeFieldDesc)
        {
            return await _fieldRepository.GetListLaboratory(pageNumber, pageSize, status, descriptionSearch, codeSearch, typeFields, typeFieldDesc);
        }
        public List<FieldLaboratoryDto>? GetListLaboratoryByGetListCatalog(Guid serviceCatalogId)
        {
            return _serviceCatalogFieldRepository.GetFieldLaboratoryByServiceCatalogId(serviceCatalogId);
        }

    }
}

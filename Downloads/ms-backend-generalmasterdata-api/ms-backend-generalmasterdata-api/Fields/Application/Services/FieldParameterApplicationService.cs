using CSharpFunctionalExtensions;
using AnaPrevention.GeneralMasterData.Api.Common.API;
using AnaPrevention.GeneralMasterData.Api.Common.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Common.Infrastructure.EF;
using AnaPrevention.GeneralMasterData.Api.Fields.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.Fields.Application.Validators;
using AnaPrevention.GeneralMasterData.Api.Fields.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Fields.Infrastructure.Repositories;
using System.Text.Json;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.ServiceCatalogs.Domain.Enums;

namespace AnaPrevention.GeneralMasterData.Api.Fields.Application.Services
{
    public class FieldParameterApplicationService
    {
        private readonly AnaPreventionContext _context;
        private readonly RegisterFieldParameterValidator _registerFieldParameterValidator;
        private readonly EditFieldParameterValidator _editFieldParameterValidator;
        private readonly FieldParameterRepository _fieldParameterRepository;

        public FieldParameterApplicationService(
       AnaPreventionContext context,
       RegisterFieldParameterValidator registerFieldParameterValidator,
       EditFieldParameterValidator editFieldParameterValidator,
       FieldParameterRepository fieldParameterRepository)
        {
            _context = context;
            _registerFieldParameterValidator = registerFieldParameterValidator;
            _editFieldParameterValidator = editFieldParameterValidator;
            _fieldParameterRepository = fieldParameterRepository;
        }

        public Result<RegisterFieldParameterResponse, Notification> RegisterFieldParameter(RegisterFieldParameterRequest request, Guid userId)
        {
            Notification notification = _registerFieldParameterValidator.Validate(request);

            if (notification.HasErrors())
                return notification;

            string defaultValue = request.DefaultValue.Trim();
            string? legend = request.Legend;
            string? uom = request.Uom;
            bool isMandatory = request.IsMandatory;
            bool show = request.Show;
            Guid? genderId = request.GenderId;
            Guid fieldId = request.FieldId;


            string rangeJson = "";
            if (request.Range != null)
                rangeJson = JsonSerializer.Serialize(request.Range);

            string OptionsJson = "";
            if (request.Options != null)
                OptionsJson = JsonSerializer.Serialize(request.Options);

            FieldParameter fieldParameter = new(defaultValue, legend, rangeJson, uom, isMandatory, fieldId, genderId, show, OptionsJson, Guid.NewGuid());
            _fieldParameterRepository.Save(fieldParameter);

            _context.SaveChanges(userId);


            var response = new RegisterFieldParameterResponse
            {
                Id = fieldParameter.Id,
                Status = fieldParameter.Status,
            };

            return response;
        }

        public EditFieldParameterResponse EditFieldParameter(EditFieldParameterRequest request, FieldParameter fieldParameter, Guid userId)
        {
            fieldParameter.GenderId = request.GenderId;
            fieldParameter.DefaultValue = request.DefaultValue.Trim();
            fieldParameter.Legend = request.Legend;
            fieldParameter.Uom = request.Uom;
            fieldParameter.IsMandatory = request.IsMandatory;
            fieldParameter.Show = request.Show;

            _context.SaveChanges(userId);

            var response = new EditFieldParameterResponse
            {
                Id = fieldParameter.Id,
                Status = fieldParameter.Status,
                Uom = fieldParameter.Uom,
                DefaultValue = fieldParameter.DefaultValue,
                Legend = fieldParameter.Legend,
                IsMandatory = fieldParameter.IsMandatory,
                Show = fieldParameter.Show,
                FieldId = fieldParameter.FieldId,
                GenderId = fieldParameter.GenderId
            };

            return response;
        }

        public EditFieldParameterResponse ActiveFieldParameter(FieldParameter fieldParameter, Guid userId)
        {
            fieldParameter.Status = true;

            _context.SaveChanges(userId);

            var response = new EditFieldParameterResponse
            {
                Id = fieldParameter.Id,
                Status = fieldParameter.Status,
                Uom = fieldParameter.Uom,
                DefaultValue = fieldParameter.DefaultValue,
                Legend = fieldParameter.Legend,
                IsMandatory = fieldParameter.IsMandatory,
                Show = fieldParameter.Show,
                FieldId = fieldParameter.FieldId,
                GenderId = fieldParameter.GenderId
            };

            return response;
        }
        public Notification ValidateEditFieldParameterRequest(EditFieldParameterRequest request)
        {
            return _editFieldParameterValidator.Validate(request);
        }

        public EditFieldParameterResponse RemoveFieldParameter(FieldParameter fieldParameter, Guid userId)
        {
            fieldParameter.Status = false;
            _context.SaveChanges(userId);

            var response = new EditFieldParameterResponse
            {
                Id = fieldParameter.Id,
                Status = fieldParameter.Status,
                Uom = fieldParameter.Uom,
                DefaultValue = fieldParameter.DefaultValue,
                Legend = fieldParameter.Legend,
                IsMandatory = fieldParameter.IsMandatory,
                Show = fieldParameter.Show,
                FieldId = fieldParameter.FieldId,
                GenderId = fieldParameter.GenderId
            };

            return response;
        }

        public FieldParameter? GetById(Guid id)
        {
            return _fieldParameterRepository.GetById(id);
        }

        public FieldParameterDto? GetDtoById(Guid id)
        {
            return _fieldParameterRepository.GetDtoById(id);
        }

        public List<FieldParameterDto> GetListByMedicalFormsType(MedicalFormsType medicalFormsType, MedicalFormSubType medicalFormSubType = MedicalFormSubType.NONE)
        {
            return _fieldParameterRepository.GetListByMedicalFormsType(medicalFormsType, medicalFormSubType);
        }

        public List<FieldFullDto>? GetListByMedicalFormId(Guid medicalFormId)
        {
            List<FieldFullDto> fieldFullDtos = [];
            List<FieldParameterDto> ListfieldParameterDtos = _fieldParameterRepository.GetListByMedicalFormId(medicalFormId);
            if (ListfieldParameterDtos != null)
            {
                foreach (var fieldParameterDto in ListfieldParameterDtos)
                {

                    var fieldFullDto = fieldFullDtos.Find(x => x.Id == fieldParameterDto.FieldId);

                    if (fieldFullDto == null)
                    {
                        fieldFullDto = new FieldFullDto()
                        {
                            Id = fieldParameterDto.FieldId,
                            Description = fieldParameterDto.Description,
                            Name = fieldParameterDto.Name,
                            Code = fieldParameterDto.Code,
                            Legend = fieldParameterDto.Legend,
                            Uom = fieldParameterDto.Uom,
                            FieldType = fieldParameterDto.FieldType,
                            MedicalFormId = medicalFormId,
                            Status = fieldParameterDto.Status,
                            MedicalForm = fieldParameterDto.MedicalForm,
                            ListFieldParameters = new List<FieldParameterMinDto>()
                        };
                        fieldFullDtos.Add(fieldFullDto);
                    }
                    if (fieldParameterDto.Id != null)
                    {
                        fieldFullDto.ListFieldParameters.Add(new FieldParameterMinDto()
                        {
                            Id = (Guid)fieldParameterDto.Id,
                            DefaultValue = fieldParameterDto.DefaultValue,
                            Legend = fieldParameterDto.Legend,
                            Range = fieldParameterDto.Range,
                            Uom = fieldParameterDto.Uom,
                            IsMandatory = fieldParameterDto.IsMandatory,
                            Show = fieldParameterDto.Show,
                            GenderId = fieldParameterDto.GenderId,
                            Gender = fieldParameterDto.Gender,
                            Status = fieldParameterDto.Status,
                        });
                    }
                }
            }
            return fieldFullDtos;
        }
        public List<FieldParameterDto>? GetListByFieldId(Guid fieldId)
        {
            return _fieldParameterRepository.GetListByFieldId(fieldId);
        }
        public List<FieldParameterDto> GetListAll()
        {
            return _fieldParameterRepository.GetListAll();
        }

        public Tuple<IEnumerable<FieldParameterDto>, PaginationMetadata> GetList(int pageNumber, int pageSize, bool status, string descriptionSearch = "", string codeSearch = "")
        {
            return _fieldParameterRepository.GetList(pageNumber, pageSize, status, descriptionSearch, codeSearch);
        }
    }
}

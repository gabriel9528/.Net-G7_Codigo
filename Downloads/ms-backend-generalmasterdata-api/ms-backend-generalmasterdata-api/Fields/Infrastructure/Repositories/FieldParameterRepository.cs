using Microsoft.EntityFrameworkCore;
using AnaPrevention.GeneralMasterData.Api.Common.API;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Common.Infrastructure.EF;
using AnaPrevention.GeneralMasterData.Api.Fields.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.Fields.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Persons.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.ServiceCatalogs.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.ServiceCatalogs.Domain.Enums;

namespace AnaPrevention.GeneralMasterData.Api.Fields.Infrastructure.Repositories
{
    public class FieldParameterRepository : Repository<FieldParameter>
    {
        const int maxRowPageSize = CommonStatic.MaxRowPageSize;
        public FieldParameterRepository(AnaPreventionContext context) : base(context)
        {
        }

        public FieldParameterDto? GetDtoById(Guid id)
        {
            return GetDtoQueryable().SingleOrDefault(t1 => t1.Id == id);
        }
        public List<FieldParameterDto> GetListByMedicalFormId(Guid medicalFormId)
        {
            return GetDtoQueryable().Where(t1 => t1.MedicalFormId == medicalFormId).ToList();
        }

        public FieldParameterDto? GetListByMedicalFormId(Guid medicalFormId, string name)
        {
            return GetDtoQueryable().Where(t1 => t1.MedicalFormId == medicalFormId && t1.Name == name).FirstOrDefault();
        }


        public List<FieldParameterDto> GetListByMedicalFormsType(MedicalFormsType medicalFormsType, MedicalFormSubType medicalFormSubType = MedicalFormSubType.NONE)
        {
            return [.. GetDtoQueryable().Where(t1 => t1.MedicalFormsType == medicalFormsType && t1.MedicalFormSubType == medicalFormSubType && t1.Status)];
        }

        public List<FieldParameterDto> GetListByFieldId(Guid fieldId)
        {
            return GetDtoQueryable().Where(t1 => t1.FieldId == fieldId).ToList();
        }
        public List<FieldParameterDto> GetListAll()
        {
            return GetDtoQueryable().Where(t1 => t1.Status).ToList();
        }

        public List<FieldParameterDto> GetListFilter(bool status = true, string descriptionSearch = "", string codeSearch = "")
        {

            var query = GetDtoQueryable().Where(t1 => t1.Status == status);

            if (!string.IsNullOrEmpty(descriptionSearch))
                query = query.Where(t1 => EF.Functions.Like(t1.Description, "%" + descriptionSearch + "%"));

            if (!string.IsNullOrEmpty(codeSearch))
                query = query.Where(t1 => t1.Code.Contains(codeSearch));

            return query.ToList();

        }

        public Tuple<IEnumerable<FieldParameterDto>, PaginationMetadata> GetList(int pageNumber, int pageSize, bool status = true, string descriptionSearch = "", string codeSearch = "")
        {
            if (pageSize > maxRowPageSize)
                pageSize = maxRowPageSize;

            var query = GetDtoQueryable().Where(t1 => t1.Status == status);

            if (!string.IsNullOrEmpty(descriptionSearch))
                query = query.Where(t1 => EF.Functions.Like(t1.Description, "%" + descriptionSearch + "%"));

            if (!string.IsNullOrEmpty(codeSearch))
                query = query.Where(t1 => t1.Code.Contains(codeSearch));

            var listFieldParameterDto = query.OrderBy(t1 => t1.Description).Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList();
            int totalItemCount = query.Count();


            var paginationMetadata = new PaginationMetadata(
              totalItemCount, pageSize, pageNumber);

            return new Tuple<IEnumerable<FieldParameterDto>, PaginationMetadata>
                (listFieldParameterDto, paginationMetadata);
        }

        private IQueryable<FieldParameterDto> GetDtoQueryable()
        {
            return (from t1 in _context.Set<Field>()
                    join t2 in _context.Set<FieldParameter>() on t1.Id equals t2.FieldId into into_t2
                    from t2 in into_t2.DefaultIfEmpty()
                    join t3 in _context.Set<MedicalForm>() on t1.MedicalFormId equals t3.Id
                    join t4 in _context.Set<Gender>() on t2.GenderId equals t4.Id into t4_into
                    from t4 in t4_into.DefaultIfEmpty()
                    orderby t1.OrderRow
                    select new FieldParameterDto()
                    {
                        Id = t2.Id,
                        FieldId = t1.Id,
                        Description = t1.Description,
                        Code = t1.Code,
                        Name = t1.Name,
                        DefaultValue = t2.DefaultValue ?? "",
                        Legend = t2.Legend ?? t1.Legend ?? "",
                        Range = CommonStatic.ConvertJsonToRegisterRangeResponse(t2.RangeJson),
                        Uom = t2.Uom ?? t1.Uom ?? "",
                        FieldType = t1.FieldType,
                        IsMandatory = t2 != null && t2.IsMandatory,
                        Show = t2 != null && t2.Show,
                        MedicalFormId = t1.MedicalFormId,
                        MedicalForm = t3.Description,
                        GenderId = t2.GenderId,
                        Gender = t4.Description,
                        Status = t1.Status,
                        MedicalFormsType = t3.MedicalFormsType,
                        MedicalFormSubType = t1.MedicalFormSubType
                    });
        }
    }
}

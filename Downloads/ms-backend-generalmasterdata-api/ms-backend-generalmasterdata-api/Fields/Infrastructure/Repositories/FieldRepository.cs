using Microsoft.EntityFrameworkCore;
using AnaPrevention.GeneralMasterData.Api.Common.API;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Common.Infrastructure.EF;
using AnaPrevention.GeneralMasterData.Api.Families.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Fields.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.Fields.Application.Dtos.Fields;
using AnaPrevention.GeneralMasterData.Api.Fields.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Lines.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Lines.Domain.Enums;
using AnaPrevention.GeneralMasterData.Api.ServiceCatalogs.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.ServiceCatalogs.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.ServiceCatalogs.Domain.Enum;
using AnaPrevention.GeneralMasterData.Api.ServiceCatalogs.Domain.Enums;
using AnaPrevention.GeneralMasterData.Api.SubFamilies.Domain.Entities;

namespace AnaPrevention.GeneralMasterData.Api.Fields.Infrastructure.Repositories
{
    public class FieldRepository : Repository<Field>
    {
        const int maxRowPageSize = CommonStatic.MaxRowPageSize;
        public FieldRepository(AnaPreventionContext context) : base(context)
        {
        }

        public string GenerateCode()
        {
            var codeStrings = _context.Set<Field>()
                              .Select(t1 => t1.Code)
                              .Where(c => !string.IsNullOrEmpty(c))
                              .ToList();

            var codes = codeStrings
                .Select(c =>
                {
                    bool isValid = int.TryParse(c, out int parsedCode);
                    return new { IsValid = isValid, Code = parsedCode };
                })
                .Where(c => c.IsValid)
                .Select(c => c.Code)
                .ToList();



            var codeMax = codes.Count != 0 ? codes.Max() : 0;

            var newCode = (codeMax + 1).ToString("D" + CommonStatic.numberZerosCode);

            return newCode;
        }


        public Field? GetbyDescription(string description, Guid medicalFormId)
        {
            return _context.Set<Field>().SingleOrDefault(x =>
                                            x.Description == description &&
                                            x.MedicalFormId == medicalFormId);
        }
        public List<Field>? GetbyName(string name, Guid medicalFormId)
        {
            return _context.Set<Field>().Where(x =>
                                            x.Name == name &&
                                            x.MedicalFormId == medicalFormId).ToList();
        }
        public Field? GetbyCode(string code, Guid medicalFormId)
        {
            return _context.Set<Field>().SingleOrDefault(
                                                        x => x.Code == code &&
                                                        x.MedicalFormId == medicalFormId
                                                        );
        }
        public bool DescriptionTakenForEdit(Guid fieldId, string description, Guid medicalFormId)
        {
            return _context.Set<Field>().Any(c => c.Id != fieldId && c.Description == description &&
                                                        c.MedicalFormId == medicalFormId);
        }
        public bool NameTakenForEdit(Guid fieldId, string name, Guid medicalFormId)
        {
            return _context.Set<Field>().Any(c => c.Id != fieldId && c.Name == name &&
                                                        c.MedicalFormId == medicalFormId);
        }
        public bool CodeTakenForEdit(Guid fieldId, string code, Guid medicalFormId)
        {
            return _context.Set<Field>().Any(c => c.Id != fieldId && c.Code == code &&
                                                        c.MedicalFormId == medicalFormId);
        }

        public FieldLaboratoryDto? GetFieldLaboratoryDtoById(Guid id) => GetDtoLaboratoryQueryable().Where(t1 => t1.Id == id).SingleOrDefault();

        public List<FieldDto> GetListByMedicalFormId(Guid medicalFormId)
        {
            return (from t1 in _context.Set<Field>()
                    join t2 in _context.Set<MedicalForm>() on t1.MedicalFormId equals t2.Id
                    where t1.MedicalFormId == medicalFormId && t1.Status
                    orderby t1.Description
                    select new FieldDto()
                    {
                        Id = t1.Id,
                        Description = t1.Description,
                        Code = t1.Code,
                        Name = t1.Name,
                        Legend = t1.Legend,
                        Uom = t1.Uom,
                        FieldType = t1.FieldType,
                        MedicalForm = t2.Description,
                        MedicalFormId = t1.MedicalFormId,
                        Status = t1.Status,
                        MedicalFormsType = t2.MedicalFormsType,
                    }).ToList();
        }

        public List<FieldDto> GetListByMedicalFormAndFormat(MedicalFormsType medicalFormsType, MedicalFormatType medicalFormatType, bool isforAllFormats = true)
        {
            return (from t1 in _context.Set<Field>()
                    join t2 in _context.Set<MedicalForm>() on t1.MedicalFormId equals t2.Id
                    join t3 in _context.Set<FieldMedicalFormat>() on t1.Id equals t3.FieldId into intot3
                    from t3 in intot3.DefaultIfEmpty()
                    join t4 in _context.Set<MedicalFormat>() on t3.MedicalFormatId equals t4.Id into intot4
                    from t4 in intot4.DefaultIfEmpty()

                    where t2.MedicalFormsType == medicalFormsType && t1.Status && ((isforAllFormats && (t1.IsforAllFormats
                    || t4.MedicalFormatType == medicalFormatType)) || (isforAllFormats == false && t4.MedicalFormatType == medicalFormatType))
                    orderby t1.Description
                    select new FieldDto()
                    {
                        Id = t1.Id,
                        Description = t1.Description,
                        Code = t1.Code,
                        Name = t1.Name,
                        Legend = t1.Legend,
                        Uom = t1.Uom,
                        FieldType = t1.FieldType,
                        MedicalForm = t2.Description,
                        MedicalFormId = t1.MedicalFormId,
                        Status = t1.Status,
                        MedicalFormsType = t2.MedicalFormsType,
                    }).ToList();
        }

        public List<FieldDto> GetListLaboratory()
        {
            return (from t1 in _context.Set<ServiceCatalog>()
                    join t2 in _context.Set<ServiceCatalogField>() on t1.Id equals t2.ServiceCatalogId
                    join t3 in _context.Set<Field>() on t2.FieldId equals t3.Id
                    join t4 in _context.Set<SubFamily>() on t1.SubFamilyId equals t4.Id
                    join t5 in _context.Set<Family>() on t4.FamilyId equals t5.Id
                    join t6 in _context.Set<Line>() on t5.LineId equals t6.Id
                    join t7 in _context.Set<LineType>() on t6.LineTypeId equals t7.Id
                    where t1.Status && t7.Code == CodeLineType.LABORATORY_EXAM
                    orderby t1.Description
                    select new FieldDto()
                    {
                        Id = t3.Id,
                        Description = t3.Description,
                        Code = t3.Code,
                        Name = t3.Name,
                        Legend = t3.Legend,
                        Uom = t3.Uom,
                        FieldType = t3.FieldType,
                        MedicalForm = string.Empty,
                        MedicalFormId = t3.MedicalFormId,
                        Status = t3.Status,
                        MedicalFormsType = MedicalFormsType.OCCUPATIONAL_LABORATORY,
                    }).ToList();
        }


        public List<Field> GetListAll()
        {
            return _context.Set<Field>().Where(x => x.Status).ToList();
        }

        public List<FieldLaboratoryDto> GetListFilter(bool status, string? descriptionSearch, string? codeSearch)
        {

            var query = GetDtoLaboratoryQueryable().Where(t1 => t1.Status == status);

            if (!string.IsNullOrEmpty(descriptionSearch))
                query = query.Where(t1 => EF.Functions.Like(t1.Description, "%" + descriptionSearch + "%"));

            if (!string.IsNullOrEmpty(codeSearch))
                query = query.Where(t1 => t1.Code.Contains(codeSearch));


            return query.OrderBy(t1 => t1.Description).ToList();
        }

        public async Task<Tuple<IEnumerable<FieldLaboratoryDto>, PaginationMetadata>> GetListLaboratory(int pageNumber, int pageSize, bool status, string? descriptionSearch, string? codeSearch, int? typeFields, string? typeFieldDesc)
        {
            if (pageSize > maxRowPageSize)
                pageSize = maxRowPageSize;

            var query = GetDtoLaboratoryQueryable().Where(t1 => t1.Status == status);

            if (!string.IsNullOrEmpty(descriptionSearch))
                query = query.Where(t1 => EF.Functions.Like(t1.Description, "%" + descriptionSearch + "%"));

            if (!string.IsNullOrEmpty(codeSearch))
                query = query.Where(t1 => t1.Code.Contains(codeSearch));

            if (!string.IsNullOrEmpty(typeFieldDesc))
            {
                int? fieldType = CommonStatic.GetSimilarityValue(typeFieldDesc);

                if (fieldType != null)
                    typeFields = fieldType;
            }

            if (typeFields != null)
                query = query.Where(t1 => (int)t1.FieldType == typeFields);

            var listField = await Task.FromResult(query.OrderBy(t1 => t1.Description)
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize).ToList());

            int totalItemCount = query.Count();

            var paginationMetadata = new PaginationMetadata(
             totalItemCount, pageSize, pageNumber);

            return await Task.FromResult(new Tuple<IEnumerable<FieldLaboratoryDto>, PaginationMetadata>
                (listField, paginationMetadata));
        }

        //public List<FieldLaboratoryDto> GetFieldLaboratoryDtoByOrder(Guid occupationalHealthOrderId)
        //{
        //    return (from t1 in _context.Set<Field>()
        //            join t2 in _context.Set<OccupationalHealthOrderField>() on t1.Id equals t2.FieldId
        //            where t1.Status && t2.OccupationalHealthOrderId == occupationalHealthOrderId
        //            orderby t1.Description
        //            select new FieldLaboratoryDto()
        //            {
        //                Id = t1.Id,
        //                Description = t1.Description,
        //                Code = t1.Code,
        //                Name = t1.Name,
        //                Legend = t1.Legend,
        //                Uom = t1.Uom,
        //                FieldType = t1.FieldType,
        //                IsTittle = t1.IsTittle,
        //                OrderRow = t1.OrderRow,
        //                FieldExamenType = t1.FieldExamenType,
        //                Options = CommonStatic.ConvertJsonToListOptionFieldDto(t1.OptionsJson),
        //                ReferenceValues = CommonStatic.ConvertJsonToListString(t1.ReferenceValuesJson),
        //                Status = t1.Status
        //            }).Distinct().ToList();
        //}

        private IQueryable<FieldLaboratoryDto> GetDtoLaboratoryQueryable()
        {
            return (from t1 in _context.Set<Field>()
                    where t1.FieldExamenType == CodeLineType.LABORATORY_EXAM
                    orderby t1.Description
                    select new FieldLaboratoryDto()
                    {
                        Id = t1.Id,
                        Description = t1.Description,
                        Code = t1.Code,
                        SecondCode = t1.SecondCode,
                        Name = t1.Name,
                        Legend = t1.Legend,
                        Uom = t1.Uom,
                        FieldType = t1.FieldType,
                        FieldTypeDescription = CommonStatic.ConvertEnumFieldTypeToString(t1.FieldType),
                        OrderRow = t1.OrderRow,
                        FieldExamenType = t1.FieldExamenType,
                        Options = CommonStatic.ConvertJsonToListOptionFieldDto(t1.OptionsJson),
                        ReferenceValues = CommonStatic.ConvertJsonToListString(t1.ReferenceValuesJson),
                        Status = t1.Status,
                        IsTittle = t1.IsTittle,
                        ListServiceCatalog = new((from st1 in _context.Set<ServiceCatalog>()
                                                  join st2 in _context.Set<SubFamily>() on st1.SubFamilyId equals st2.Id
                                                  join st3 in _context.Set<Family>() on st2.FamilyId equals st3.Id
                                                  join st4 in _context.Set<Line>() on st3.LineId equals st4.Id
                                                  join st5 in _context.Set<LineType>() on st4.LineTypeId equals st5.Id
                                                  join st6 in _context.Set<ServiceCatalogField>() on st1.Id equals st6.ServiceCatalogId
                                                  where
                                                    st5.Code == CodeLineType.LABORATORY_EXAM
                                                    && st6.FieldId == t1.Id
                                                  orderby
                                                  st4.Description, st3.Description, st2.Description, st1.Description
                                                  select new ServiceCatalogMinDto()
                                                  {
                                                      Id = st1.Id,
                                                      Description = st1.Description,
                                                      Code = st1.Code,
                                                      SubFamilyId = st1.SubFamilyId,
                                                      SubFamily = st2.Description,
                                                      FamilyId = st2.FamilyId,
                                                      Family = st3.Description,
                                                      Line = st4.Description,
                                                      LineId = st3.LineId,
                                                      CodeLineType = st5.Code,
                                                      Status = st1.Status
                                                  }).ToList())
                    });
        }

    }
}

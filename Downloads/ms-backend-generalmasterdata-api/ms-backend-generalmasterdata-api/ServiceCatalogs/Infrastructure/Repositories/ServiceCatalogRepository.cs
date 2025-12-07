using Microsoft.EntityFrameworkCore;
using AnaPrevention.GeneralMasterData.Api.Common.API;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Common.Infrastructure.EF;
using AnaPrevention.GeneralMasterData.Api.ExistenceTypes.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Families.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Lines.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Lines.Domain.Enums;
using AnaPrevention.GeneralMasterData.Api.MeasurementUnits.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.MedicalAreas.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.ServiceCatalogs.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.ServiceCatalogs.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.ServiceCatalogs.Domain.Enums;
using AnaPrevention.GeneralMasterData.Api.ServiceTypes.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.SubFamilies.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Taxes.Domain.Entities;

namespace AnaPrevention.GeneralMasterData.Api.ServiceCatalogs.Infrastructure.Repositories
{
    public class ServiceCatalogRepository : Repository<ServiceCatalog>
    {
        readonly int maxRowPageSize = CommonStatic.MaxRowPageSize;
        public ServiceCatalogRepository(AnaPreventionContext context) : base(context)
        {
        }
        public List<ServiceCatalog>? GetByIds(List<Guid> ids)
        {
            return _context.Set<ServiceCatalog>().Where(t1 => ids.Contains(t1.Id)).ToList();
        }

        public ServiceCatalogDto? GetDtoById(Guid id)
        {
            return GetDtoQueryable().SingleOrDefault(t1 => t1.Id == id);
        }
        public ServiceCatalog? GetbyDescription(string description)
        {
            return _context.Set<ServiceCatalog>().SingleOrDefault(x => x.Description == description);
        }
        public ServiceCatalog? GetbyCode(string code)
        {
            return _context.Set<ServiceCatalog>().SingleOrDefault(x => x.Code == code);
        }
        public bool DescriptionTakenForEdit(Guid serviceCatalog, string description)
        {
            return _context.Set<ServiceCatalog>().Any(c => c.Id != serviceCatalog && c.Description == description);
        }
        public bool CodeTakenForEdit(Guid serviceCatalog, string code)
        {
            return _context.Set<ServiceCatalog>().Any(c => c.Id != serviceCatalog && c.Code == code);
        }

        public List<ServiceCatalogMinDto>? GetMinDtoForLaboratoryFilter(string? description)
        {
            if (string.IsNullOrWhiteSpace(description))
            {
                description = string.Empty;
            }
            return GetDtoMinLaboratoryQueryable().Where(t1 => t1.Status && EF.Functions.Like(t1.Description, "%" + description + "%")).Take(10).ToList();
        }

        public List<ServiceCatalogDto> GetListAllBySubFamilyId(Guid subFamilyId)
        {
            return GetDtoQueryable().Where(t1 => t1.Status && t1.SubFamilyId == subFamilyId).ToList();
        }

        public List<ServiceCatalogDto> GetListAll(string description = "")
        {
            return GetDtoQueryable().Where(t1 => t1.Status && EF.Functions.Like(t1.Description, "%" + description + "%")).Take(10).ToList();

        }

        public List<ServiceCatalogBasicDto> GetListAllExams()
        {
            return (from serviceCatalog in _context.Set<ServiceCatalog>()
                    join t2 in _context.Set<SubFamily>() on serviceCatalog.SubFamilyId equals t2.Id
                    join t3 in _context.Set<Family>() on t2.FamilyId equals t3.Id
                    join t4 in _context.Set<Line>() on t3.LineId equals t4.Id
                    join lineType in _context.Set<LineType>() on t4.LineTypeId equals lineType.Id
                    join tax in _context.Set<Tax>() on serviceCatalog.TaxId equals tax.Id into t7_into
                    from tax in t7_into.DefaultIfEmpty()
                    where
                    serviceCatalog.Status
                    select new ServiceCatalogBasicDto()
                    {
                        Description = serviceCatalog.Description,
                        Id = serviceCatalog.Id,
                        TaxRate = tax.Rate,
                    }).ToList();

        }

        public string GenerateCode(Guid subFamilyId)
        {

            var resultCode = (
                        from t1 in _context.Set<SubFamily>()
                        join t2 in _context.Set<Family>() on t1.FamilyId equals t2.Id
                        join t3 in _context.Set<Line>() on t2.LineId equals t3.Id
                        where t1.Id == subFamilyId
                        select new
                        {
                            BaseCode = string.Concat(t3.Code.Substring(t3.Code.Length - 2)
                                    , t2.Code.Substring(t2.Code.Length - 2)
                                    , t1.Code.Substring(t1.Code.Length - 2))
                        }
                        ).FirstOrDefault();

            string baseCode = resultCode?.BaseCode ?? Guid.NewGuid().ToString();

            var codeMax = _context.Set<ServiceCatalog>().Where(t1 => t1.SubFamilyId == subFamilyId && t1.Code.StartsWith(baseCode)).Max(t1 => t1.Code);

            if (string.IsNullOrEmpty(codeMax))
            {

                return baseCode + (1).ToString("D" + 4);

            }
            else
            {
                try
                {
                    var newCode = (int.Parse(codeMax[6..]) + 1).ToString("D" + 4);
                    return resultCode != null ? resultCode.BaseCode + newCode : newCode;
                }
                catch (Exception)
                {
                    return resultCode?.BaseCode + (1).ToString("D" + 4);
                }
            }
        }
        public List<ServiceCatalogDto> GetListFilter(bool status = true, string descriptionSearch = "", string codeSearch = "")
        {
            var query = GetDtoQueryable();

            if (!string.IsNullOrEmpty(descriptionSearch))
                query = query.Where(t1 => EF.Functions.Like(t1.Description, "%" + descriptionSearch + "%"));

            if (!string.IsNullOrEmpty(codeSearch))
                query = query.Where(t1 => t1.Code.Contains(codeSearch));


            return query.Where(t1 => t1.Status == status).ToList();

        }
        public Tuple<IEnumerable<ServiceCatalogDto>, PaginationMetadata> GetList(int pageNumber, int pageSize, bool status = true, string descriptionSearch = "", string codeSearch = "")
        {
            if (pageSize > maxRowPageSize)
                pageSize = maxRowPageSize;

            var query = GetDtoQueryable();


            if (!string.IsNullOrEmpty(descriptionSearch))
                query = query.Where(t1 => EF.Functions.Like(t1.Description, "%" + descriptionSearch + "%"));

            if (!string.IsNullOrEmpty(codeSearch))
                query = query.Where(t1 => EF.Functions.Like(t1.Code, "%" + codeSearch + "%"));

            query = query.Where(t1 => t1.Status == status);

            var listServiceCatalogDto = query
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize).ToList();

            int totalItemCount = query.Count();

            var paginationMetadata = new PaginationMetadata(
              totalItemCount, pageSize, pageNumber);

            return new Tuple<IEnumerable<ServiceCatalogDto>, PaginationMetadata>
                (listServiceCatalogDto, paginationMetadata);
        }

        private IQueryable<ServiceCatalogDto> GetDtoQueryable()
        {
            return (from t1 in _context.Set<ServiceCatalog>()
                    join t2 in _context.Set<SubFamily>() on t1.SubFamilyId equals t2.Id
                    join t3 in _context.Set<Family>() on t2.FamilyId equals t3.Id
                    join t4 in _context.Set<Line>() on t3.LineId equals t4.Id
                    join t5 in _context.Set<LineType>() on t4.LineTypeId equals t5.Id
                    join t6 in _context.Set<ExistenceType>() on t1.ExistenceTypeId equals t6.Id into t6_into
                    from t6 in t6_into.DefaultIfEmpty()
                    join t7 in _context.Set<Tax>() on t1.TaxId equals t7.Id into t7_into
                    from t7 in t7_into.DefaultIfEmpty()
                    join t8 in _context.Set<Uom>() on t1.UomId equals t8.Id into t8_into
                    from t8 in t8_into.DefaultIfEmpty()
                    join t9 in _context.Set<Uom>() on t1.UomSecondId equals t9.Id into t9_into
                    from t9 in t9_into.DefaultIfEmpty()
                    join t10 in _context.Set<MedicalArea>() on t1.MedicalAreaId equals t10.Id into t10_into
                    from t10 in t10_into.DefaultIfEmpty()
                    orderby
                    t4.Description, t3.Description, t2.Description, t1.Description
                    select new ServiceCatalogDto()
                    {
                        Id = t1.Id,
                        Description = t1.Description,
                        Code = t1.Code,
                        CodeSecond = t1.CodeSecond,
                        SubFamilyId = t1.SubFamilyId,
                        SubFamily = t2.Description,
                        FamilyId = t2.FamilyId,
                        Family = t3.Description,
                        Line = t4.Description,
                        LineId = t3.LineId,
                        LineType = t5.Description,
                        LineTypeId = t4.LineTypeId,
                        CodeLineType = t5.Code,
                        UomId = t1.UomId,
                        Uom = t8.Description,
                        UomSecondId = t1.UomSecondId,
                        UomAbbreviation = t8.Abbreviation,
                        UomSecond = t9.Description,
                        UomSecondAbbreviation = t9.Abbreviation,
                        ExistenceTypeId = t1.ExistenceTypeId,
                        ExistenceType = t6.Description,
                        TaxId = t1.TaxId,
                        Tax = t7.Description,
                        TaxRate = t7.Rate,
                        IsActive = t1.IsActive,
                        IsSales = t1.IsSales,
                        IsBuy = t1.IsBuy,
                        IsInventory = t1.IsInventory,
                        IsRetention = t1.IsRetention,
                        Comment = t1.Comment,
                        Status = t1.Status,
                        MedicalAreaId = t1.MedicalAreaId,
                        MedicalArea = t10.Description,
                        ListServiceTypes = (from st1 in _context.Set<ServiceCatalogServiceType>()
                                            join st2 in _context.Set<ServiceType>() on st1.ServiceTypeId equals st2.Id
                                            where st1.ServiceCatalogId == t1.Id
                                            select new ServiceType()
                                            {
                                                Id = st2.Id,
                                                Description = st2.Description,
                                                Code = st2.Code,
                                                Status = st2.Status,
                                                CompanyId = st2.CompanyId
                                            }).ToList(),
                        ListMedicalForms = (from st3 in _context.Set<ServiceCatalogMedicalForm>()
                                            join st4 in _context.Set<MedicalForm>() on st3.MedicalFormId equals st4.Id
                                            where st3.ServiceCatalogId == t1.Id
                                            select st4
                                            ).ToList(),
                    });
        }

        private IQueryable<ServiceCatalogMinDto> GetDtoMinLaboratoryQueryable()
        {
            return (from t1 in _context.Set<ServiceCatalog>()
                    join t2 in _context.Set<SubFamily>() on t1.SubFamilyId equals t2.Id
                    join t3 in _context.Set<Family>() on t2.FamilyId equals t3.Id
                    join t4 in _context.Set<Line>() on t3.LineId equals t4.Id
                    join t5 in _context.Set<LineType>() on t4.LineTypeId equals t5.Id
                    join t6 in _context.Set<Tax>() on t1.TaxId equals t6.Id into t6_into
                    from t6 in t6_into.DefaultIfEmpty()
                    where
                     t5.Code == CodeLineType.LABORATORY_EXAM
                    orderby
                    t4.Description, t3.Description, t2.Description, t1.Description
                    select new ServiceCatalogMinDto()
                    {
                        Id = t1.Id,
                        Description = t1.Description,
                        Code = t1.Code,
                        SubFamilyId = t1.SubFamilyId,
                        SubFamily = t2.Description,
                        FamilyId = t2.FamilyId,
                        Family = t3.Description,
                        Line = t4.Description,
                        LineId = t3.LineId,
                        CodeLineType = t5.Code,
                        Status = t1.Status,
                        TaxId = t1.TaxId,
                        Tax = t6.Description,
                        TaxRate = t6.Rate,

                    });
        }

        public List<ItemOrdenRowDto> GetOrderRow()
        {
            return (from t1 in _context.Set<Line>()
                    join t2 in _context.Set<LineType>() on t1.LineTypeId equals t2.Id
                    where
                    t1.Status && (t2.Code == CodeLineType.LABORATORY_EXAM || t2.Code == CodeLineType.MEDICAL_EXAM)
                    select new ItemOrdenRowDto()
                    {
                        OrderRow = t1.OrderRow,
                        Id = t1.Id,
                        Code = t1.Code,
                        Description = t1.Description,
                        OrderEntityType = OrderEntityType.LINE,
                        Sons = (from st1 in _context.Set<Family>()
                                where st1.Status && st1.LineId == t1.Id
                                select new ItemOrdenRowDto()
                                {
                                    OrderRow = st1.OrderRow,
                                    Id = st1.Id,
                                    Code = st1.Code,
                                    Description = st1.Description,
                                    OrderEntityType = OrderEntityType.FAMILY,
                                    Sons = (from tt1 in _context.Set<SubFamily>()
                                            where tt1.Status && tt1.FamilyId == st1.Id
                                            select new ItemOrdenRowDto()
                                            {
                                                OrderRow = tt1.OrderRow,
                                                Id = tt1.Id,
                                                Code = tt1.Code,
                                                Description = tt1.Description,
                                                OrderEntityType = OrderEntityType.SUBFAMILY,
                                                Sons = (from ft1 in _context.Set<ServiceCatalog>()
                                                        where ft1.Status && ft1.SubFamilyId == tt1.Id
                                                        && ft1.IsBuy && ft1.IsActive
                                                        select new ItemOrdenRowDto()
                                                        {
                                                            OrderRow = ft1.OrderRow,
                                                            Id = ft1.Id,
                                                            Code = ft1.Code,
                                                            Description = ft1.Description,
                                                            OrderEntityType = OrderEntityType.SERVICE_CATALOG,
                                                        }).OrderBy(ft1 => ft1.OrderRow).ToList()
                                            }).OrderBy(tt1 => tt1.OrderRow).ToList()
                                }).OrderBy(st1 => st1.OrderRow).ToList()

                    }).OrderBy(t1 => t1.OrderRow).ToList();
        }
        public List<ItemOrdenRowDto> GetOrderRowTourSheet()
        {
            var result = (from t1 in _context.Set<MedicalArea>()
                          where
                          t1.Status
                          select new ItemOrdenRowDto()
                          {
                              OrderRow = t1.OrderRowTourSheet,
                              Id = t1.Id,
                              Code = t1.Code,
                              Description = t1.Description,
                              OrderEntityType = OrderEntityType.MEDICAL_AREA,
                              Sons = (from st1 in _context.Set<ServiceCatalog>()
                                      join st2 in _context.Set<ServiceCatalogMedicalForm>() on st1.Id equals st2.ServiceCatalogId
                                      join st3 in _context.Set<MedicalForm>() on st2.MedicalFormId equals st3.Id
                                      where st1.Status && st3.MedicalAreaId == t1.Id
                                      && st1.IsBuy && st1.IsActive
                                      group st1 by new
                                      {
                                          st1.OrderRowTourSheet,
                                          st1.Id,
                                          st1.Code,
                                          st1.Description
                                      } into st12_group
                                      select new ItemOrdenRowDto()
                                      {
                                          OrderRow = st12_group.Key.OrderRowTourSheet,
                                          Id = st12_group.Key.Id,
                                          Code = st12_group.Key.Code,
                                          Description = st12_group.Key.Description,
                                          OrderEntityType = OrderEntityType.SERVICE_CATALOG,
                                      }).OrderBy(st1 => st1.OrderRow).ToList()
                          }).OrderBy(t1 => t1.OrderRow).ToList();


            return result;
        }
    }
}

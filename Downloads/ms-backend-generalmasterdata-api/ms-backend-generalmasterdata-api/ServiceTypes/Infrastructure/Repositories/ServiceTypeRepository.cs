using Microsoft.EntityFrameworkCore;
using AnaPrevention.GeneralMasterData.Api.Common.API;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Common.Infrastructure.EF;
using AnaPrevention.GeneralMasterData.Api.ServiceTypes.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.ServiceTypes.Domain.Enums;

namespace AnaPrevention.GeneralMasterData.Api.ServiceTypes.Infrastructure.Repositories
{
    public class ServiceTypeRepository : Repository<ServiceType>
    {
        readonly int maxRowPageSize = CommonStatic.MaxRowPageSize;

        public ServiceTypeRepository(AnaPreventionContext context) : base(context)
        {
        }

        public ServiceType? GetByIdAndCompanyId(Guid id, Guid companyId)
        {
            return _context.Set<ServiceType>().SingleOrDefault(t1 => t1.Id == id && t1.CompanyId == companyId);
        }
        public ServiceType? GetbyDescription(string description, Guid companyId)
        {
            return _context.Set<ServiceType>().SingleOrDefault(t1 => t1.Description == description && t1.CompanyId == companyId);
        }
        public ServiceType? GetbyCode(ServiceTypeEnum code, Guid companyId)
        {
            return _context.Set<ServiceType>().SingleOrDefault(t1 => t1.Code == code && t1.CompanyId == companyId);
        }
        public bool DescriptionTakenForEdit(Guid ServiceTypeId, string description, Guid companyId)
        {
            return _context.Set<ServiceType>().Any(
                                                        t1 => t1.Id != ServiceTypeId &&
                                                        t1.Description == description &&
                                                        t1.CompanyId == companyId
                                                        );
        }

        public bool CodeTakenForEdit(Guid ServiceTypeId, ServiceTypeEnum code, Guid companyId)
        {
            return _context.Set<ServiceType>().Any(
                                                        t1 => t1.Id != ServiceTypeId &&
                                                        t1.Code == code &&
                                                        t1.CompanyId == companyId
                                                        );
        }
        public async Task<List<ServiceType>> GetListAll(Guid companyId)
        {
            return await _context.Set<ServiceType>().Where(t1 => t1.CompanyId == companyId && t1.Status).ToListAsync();
        }
        public List<ServiceType> GetListFilter(Guid companyId, bool status, string? descriptionSearch, string? codeSearch)
        {
            var query = _context.Set<ServiceType>().Where(t1 => t1.Status == status && t1.CompanyId == companyId);

            if (!string.IsNullOrEmpty(descriptionSearch))
                query = query.Where(t1 => EF.Functions.Like(t1.Description, "%" + descriptionSearch + "%"));

            if (!string.IsNullOrEmpty(codeSearch) && int.TryParse(codeSearch.ToString(), out int code))
                query = query.Where(t1 => t1.Code == (ServiceTypeEnum)code);

            return query.OrderBy(t1 => t1.Description).ToList();
        }
        public Tuple<IEnumerable<ServiceType>, PaginationMetadata> GetList(int pageNumber, int pageSize, Guid companyId, bool status = true, string descriptionSearch = "", string codeSearch = "")
        {
            if (pageSize > maxRowPageSize)
                pageSize = maxRowPageSize;

            var query = _context.Set<ServiceType>().Where(t1 => t1.Status == status && t1.CompanyId == companyId);


            if (!string.IsNullOrEmpty(descriptionSearch))
                query = query.Where(t1 => EF.Functions.Like(t1.Description, "%" + descriptionSearch + "%"));

            if (int.TryParse(codeSearch.ToString(), out int code))
                query = query.Where(t1 => t1.Code == (ServiceTypeEnum)code);

            var listServiceType = query.OrderBy(t1 => t1.Description)
              .Skip(pageSize * (pageNumber - 1))
              .Take(pageSize).ToList();
            int totalItemCount = query.Count();

            var paginationMetadata = new PaginationMetadata(
              totalItemCount, pageSize, pageNumber);

            return new Tuple<IEnumerable<ServiceType>, PaginationMetadata>
                (listServiceType, paginationMetadata);
        }

        public List<ServiceType>? GetListServiceTypeByServiceCatalogId(Guid serviceCatalogId)
        {
            return (from t1 in _context.Set<ServiceType>()
                    join t2 in _context.Set<ServiceCatalogServiceType>() on t1.Id equals t2.ServiceCatalogId
                    where t2.ServiceCatalogId == serviceCatalogId
                    orderby t1.Description
                    select t1
                    ).ToList();
        }
    }
}

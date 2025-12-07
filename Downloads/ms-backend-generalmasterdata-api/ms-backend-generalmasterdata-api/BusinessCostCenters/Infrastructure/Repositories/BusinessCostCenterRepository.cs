using AnaPrevention.GeneralMasterData.Api.Common.API;
using AnaPrevention.GeneralMasterData.Api.Common.Infrastructure.EF;
using AnaPrevention.GeneralMasterData.Api.BusinessCostCenters.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using Microsoft.EntityFrameworkCore;

namespace AnaPrevention.GeneralMasterData.Api.BusinessCostCenters.Infrastructure.Repositories
{
    public class BusinessCostCenterRepository : Repository<BusinessCostCenter>
    {
        readonly int maxRowPageSize = CommonStatic.MaxRowPageSize;
        public BusinessCostCenterRepository(AnaPreventionContext context) : base(context)
        {
        }

        public BusinessCostCenter? GetDtoById(Guid id)
        {
            return _context.Set<BusinessCostCenter>().SingleOrDefault(x => x.Id == id);
        }
        public BusinessCostCenter? GetbyDescription(string description, Guid businessId)
        {
            return _context.Set<BusinessCostCenter>().SingleOrDefault(x => x.Description == description && x.BusinessId == businessId);
        }

        public bool DescriptionTakenForEdit(Guid businessCostCenter, string description)
        {
            return _context.Set<BusinessCostCenter>().Any(c => c.Id != businessCostCenter && c.Description == description);
        }


        public List<BusinessCostCenter> GetListAll(Guid businessId)
        {
            return _context.Set<BusinessCostCenter>().Where(t1 => t1.Status == true && t1.BusinessId == businessId).OrderBy(t1 => t1.Description).ToList();
        }

        public List<BusinessCostCenter> GetListFilter(Guid businessId, bool status = true, string descriptionSearch = "")
        {
            var query = _context.Set<BusinessCostCenter>().Where(t1 => t1.Status == status && t1.BusinessId == businessId);
            if (!string.IsNullOrEmpty(descriptionSearch))
                query = query.Where(t1 => EF.Functions.Like(t1.Description, "%" + descriptionSearch + "%"));
            return query.OrderBy(t1 => t1.Description).ToList();
        }
        public Tuple<IEnumerable<BusinessCostCenter>, PaginationMetadata> GetList(
            int pageNumber, int pageSize, Guid businessId, bool status = true, string descriptionSearch = "")
        {
            if (pageSize > maxRowPageSize)
                pageSize = maxRowPageSize;

            var query = _context.Set<BusinessCostCenter>().Where(t1 => t1.Status == status && t1.BusinessId == businessId);


            if (!string.IsNullOrEmpty(descriptionSearch))
                query = query.Where(t1 => EF.Functions.Like(t1.Description, "%" + descriptionSearch + "%"));

            var listBusinessCostCenter = query.OrderBy(t1 => t1.Description).Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList();
            int totalItemCount = query.Count();

            var paginationMetadata = new PaginationMetadata(
              totalItemCount, pageSize, pageNumber);

            return new Tuple<IEnumerable<BusinessCostCenter>, PaginationMetadata>
                (listBusinessCostCenter, paginationMetadata);
        }

    }
}

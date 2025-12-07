using AnaPrevention.GeneralMasterData.Api.Common.API;
using AnaPrevention.GeneralMasterData.Api.Common.Infrastructure.EF;
using AnaPrevention.GeneralMasterData.Api.BusinessPositions.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.BusinessAreas.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AnaPrevention.GeneralMasterData.Api.BusinessPositions.Infrastructure.Repositories
{
    public class BusinessPositionRepository : Repository<BusinessPosition>
    {
        readonly int maxRowPageSize = CommonStatic.MaxRowPageSize;

        public BusinessPositionRepository(AnaPreventionContext context) : base(context)
        {
        }

        public BusinessPosition? GetbyDescription(string description)
        {
            return _context.Set<BusinessPosition>().SingleOrDefault(t1 => t1.Description == description);
        }

        public BusinessPosition? GetbyDescription(string description, Guid businessAreaId)
        {
            return _context.Set<BusinessPosition>().SingleOrDefault(t1 => t1.Description == description && t1.BusinessAreaId == businessAreaId);
        }

        public bool DescriptionTakenForEdit(Guid businessPosition, string description)
        {
            return _context.Set<BusinessPosition>().Any(t1 => t1.Id != businessPosition && t1.Description == description);
        }

        public List<BusinessPosition> GetListByBusinessAll(Guid businessId)
        {
            return (from t1 in _context.Set<BusinessPosition>()
                    join t2 in _context.Set<BusinessArea>() on t1.BusinessAreaId equals t2.Id
                    where
                    t2.BusinessId == businessId
                    select t1).ToList();
        }
        public List<BusinessPosition> GetListAll(Guid businessAreaId)
        {
            return _context.Set<BusinessPosition>().Where(t1 => t1.BusinessAreaId == businessAreaId).ToList();
        }


        public List<BusinessPosition> GetListFilter(Guid businessAreaId, bool status = true, string descriptionSearch = "")
        {

            var query = _context.Set<BusinessPosition>().Where(t1 => t1.Status == status && t1.BusinessAreaId == businessAreaId).AsQueryable();

            if (!string.IsNullOrEmpty(descriptionSearch))
                query = query.Where(t1 => EF.Functions.Like(t1.Description, "%" + descriptionSearch + "%"));

            return query.OrderBy(t1 => t1.Description).ToList();
        }
        public Tuple<IEnumerable<BusinessPosition>, PaginationMetadata> GetList(
            int pageNumber, int pageSize, Guid businessAreaId, bool status = true, string descriptionSearch = "")
        {
            if (pageSize > maxRowPageSize)
                pageSize = maxRowPageSize;

            var query = _context.Set<BusinessPosition>().Where(t1 => t1.Status == status && t1.BusinessAreaId == businessAreaId).AsQueryable();

            if (!string.IsNullOrEmpty(descriptionSearch))
                query = query.Where(t1 => EF.Functions.Like(t1.Description, "%" + descriptionSearch + "%"));


            var listBusinessPosition = query.OrderBy(t1 => t1.Description).Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList();
            int totalItemCount = query.Count();

            var paginationMetadata = new PaginationMetadata(
              totalItemCount, pageSize, pageNumber);

            return new Tuple<IEnumerable<BusinessPosition>, PaginationMetadata>
                (listBusinessPosition, paginationMetadata);
        }
    }
}

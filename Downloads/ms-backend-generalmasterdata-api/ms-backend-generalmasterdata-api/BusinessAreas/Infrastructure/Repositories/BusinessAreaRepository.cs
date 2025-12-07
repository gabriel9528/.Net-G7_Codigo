using AnaPrevention.GeneralMasterData.Api.Common.API;
using AnaPrevention.GeneralMasterData.Api.Common.Infrastructure.EF;
using AnaPrevention.GeneralMasterData.Api.BusinessAreas.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using Microsoft.EntityFrameworkCore;

namespace AnaPrevention.GeneralMasterData.Api.BusinessAreas.Infrastructure.Repositories
{
    public class BusinessAreaRepository : Repository<BusinessArea>
    {
        readonly int maxRowPageSize = CommonStatic.MaxRowPageSize;
        public BusinessAreaRepository(AnaPreventionContext context) : base(context)
        {
        }

        public BusinessArea? GetbyDescription(string description)
        {
            return _context.Set<BusinessArea>().SingleOrDefault(x => x.Description == description);
        }

        public BusinessArea? GetbyDescription(string description, Guid businessId)
        {
            return _context.Set<BusinessArea>().SingleOrDefault(x => x.Description == description && x.BusinessId == businessId && x.Status);
        }

        public bool DescriptionTakenForEdit(Guid businessArea, string description)
        {
            return _context.Set<BusinessArea>().Any(c => c.Id != businessArea && c.Description == description);
        }


        public List<BusinessArea> GetListAll(Guid businessId)
        {
            return _context.Set<BusinessArea>().Where(t1 => t1.Status && t1.BusinessId == businessId).OrderBy(t1 => t1.Description).ToList();
        }

        public List<BusinessArea> GetListFilter(Guid businessId, bool status = true, string descriptionSearch = "")
        {
            var query = _context.Set<BusinessArea>().Where(t1 => t1.Status == status && t1.BusinessId == businessId);

            if (!string.IsNullOrEmpty(descriptionSearch))
                query = query.Where(t1 => EF.Functions.Like(t1.Description, "%" + descriptionSearch + "%"));
            return query.OrderBy(t1 => t1.Description).ToList();
        }
        public Tuple<IEnumerable<BusinessArea>, PaginationMetadata> GetList(
            int pageNumber, int pageSize, Guid businessId, bool status = true, string? descriptionSearch = "")
        {
            if (pageSize > maxRowPageSize)
                pageSize = maxRowPageSize;

            var query = _context.Set<BusinessArea>().Where(t1 => t1.Status == status && t1.BusinessId == businessId);

            if (!string.IsNullOrEmpty(descriptionSearch))
                query = query.Where(t1 => EF.Functions.Like(t1.Description, "%" + descriptionSearch + "%"));


            var ListBusinessArea = query.OrderBy(t1 => t1.Description)
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize).ToList();
            int totalItemCount = query.Count();

            var paginationMetadata = new PaginationMetadata(
              totalItemCount, pageSize, pageNumber);

            return new Tuple<IEnumerable<BusinessArea>, PaginationMetadata>
                (ListBusinessArea, paginationMetadata);
        }
    }
}

using AnaPrevention.GeneralMasterData.Api.Common.API;
using AnaPrevention.GeneralMasterData.Api.Common.Infrastructure.EF;
using AnaPrevention.GeneralMasterData.Api.BusinessProjects.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using Microsoft.EntityFrameworkCore;

namespace AnaPrevention.GeneralMasterData.Api.BusinessProjects.Infrastructure.Repositories
{
    public class BusinessProjectRepository : Repository<BusinessProject>
    {
        readonly int maxRowPageSize = CommonStatic.MaxRowPageSize;
        public BusinessProjectRepository(AnaPreventionContext context) : base(context)
        {
        }

        public BusinessProject? GetDtoById(Guid id)
        {
            return _context.Set<BusinessProject>().SingleOrDefault(x => x.Id == id);
        }
        public BusinessProject? GetbyDescription(string description, Guid businessId)
        {
            return _context.Set<BusinessProject>().SingleOrDefault(x => x.Description == description && x.BusinessId == businessId && x.Status);
        }

        public bool DescriptionTakenForEdit(Guid businessProject, string description)
        {
            return _context.Set<BusinessProject>().Any(c => c.Id != businessProject && c.Description == description);
        }


        public List<BusinessProject> GetListAll(Guid businessId)
        {
            return _context.Set<BusinessProject>().Where(t1 => t1.Status && t1.BusinessId == businessId).OrderBy(t1 => t1.Description).ToList();
        }

        public List<BusinessProject> GetListFilter(Guid businessId, bool status = true, string descriptionSearch = "")
        {
            var query = _context.Set<BusinessProject>().Where(t1 => t1.Status == status && t1.BusinessId == businessId);
            if (!string.IsNullOrEmpty(descriptionSearch))
                query = query.Where(t1 => EF.Functions.Like(t1.Description, "%" + descriptionSearch + "%"));
            return query.OrderBy(t1 => t1.Description).ToList();
        }
        public Tuple<IEnumerable<BusinessProject>, PaginationMetadata> GetList(
            int pageNumber, int pageSize, Guid businessId, bool status = true, string descriptionSearch = "")
        {
            if (pageSize > maxRowPageSize)
                pageSize = maxRowPageSize;

            var query = _context.Set<BusinessProject>().Where(t1 => t1.Status == status && t1.BusinessId == businessId);


            if (!string.IsNullOrEmpty(descriptionSearch))
                query = query.Where(t1 => EF.Functions.Like(t1.Description, "%" + descriptionSearch + "%"));

            var listBusinessProject = query.OrderBy(t1 => t1.Description).Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList();
            int totalItemCount = query.Count();

            var paginationMetadata = new PaginationMetadata(
              totalItemCount, pageSize, pageNumber);

            return new Tuple<IEnumerable<BusinessProject>, PaginationMetadata>
                (listBusinessProject, paginationMetadata);
        }
    }
}

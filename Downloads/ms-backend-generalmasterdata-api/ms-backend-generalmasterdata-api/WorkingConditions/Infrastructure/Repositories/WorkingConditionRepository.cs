using Microsoft.EntityFrameworkCore;
using AnaPrevention.GeneralMasterData.Api.Common.API;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Common.Infrastructure.EF;
using AnaPrevention.GeneralMasterData.Api.WorkingConditions.Domain.Entities;

namespace AnaPrevention.GeneralMasterData.Api.WorkingConditions.Infrastructure.Repositories
{
    public class WorkingConditionRepository : Repository<WorkingCondition>
    {
        const int maxRowPageSize = CommonStatic.MaxRowPageSize;

        public WorkingConditionRepository(AnaPreventionContext context) : base(context)
        {
        }

        public WorkingCondition? GetbyDescription(string description)
        {
            return _context.Set<WorkingCondition>().SingleOrDefault(x => x.Description == description);
        }
        public WorkingCondition? GetbyCode(int code)
        {
            return _context.Set<WorkingCondition>().SingleOrDefault(x => x.Code == code);
        }
        public bool DescriptionTakenForEdit(Guid workingCondition, string description)
        {
            return _context.Set<WorkingCondition>().Any(c => c.Id != workingCondition && c.Description == description);
        }
        public bool CodeTakenForEdit(Guid workingCondition, int code)
        {
            return _context.Set<WorkingCondition>().Any(c => c.Id != workingCondition && c.Code == code);
        }

        public List<WorkingCondition> GetListAll()
        {
            return _context.Set<WorkingCondition>().Where(t1 => t1.Status).ToList();
        }

        public List<WorkingCondition> GetListFilter(bool status = true, string descriptionSearch = "", string codeSearch = "")
        {
            var query = _context.Set<WorkingCondition>().Where(t1 => t1.Status == status);
            if (!string.IsNullOrEmpty(descriptionSearch))
                query = query.Where(t1 => EF.Functions.Like(t1.Description, "%"+descriptionSearch+ "%"));
            if (!string.IsNullOrEmpty(codeSearch) && int.TryParse(codeSearch.ToString(), out int code))
                query = query.Where(t1 => t1.Code == code);
            return query.OrderBy(t1 => t1.Description).ToList();
        }

        public Tuple<IEnumerable<WorkingCondition>, PaginationMetadata> GetList(int pageNumber, int pageSize, bool status = true, string descriptionSearch = "", string codeSearch = "")
        {
            if (pageSize > maxRowPageSize)
                pageSize = maxRowPageSize;

            var query = _context.Set<WorkingCondition>().Where(t1 => t1.Status == status);
            if (!string.IsNullOrEmpty(descriptionSearch))
                query = query.Where(t1 => EF.Functions.Like(t1.Description, "%"+descriptionSearch+ "%"));
            if (!string.IsNullOrEmpty(codeSearch) && int.TryParse(codeSearch.ToString(), out int code))
                query = query.Where(t1 => t1.Code == code);

            var listWorkingConditionDto = query.OrderBy(t1 => t1.Description).Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList();
            int totalItemCount = query.Count();

            var paginationMetadata = new PaginationMetadata(
              totalItemCount, pageSize, pageNumber);

            return new Tuple<IEnumerable<WorkingCondition>, PaginationMetadata>
                (listWorkingConditionDto, paginationMetadata);
        }
    }
}

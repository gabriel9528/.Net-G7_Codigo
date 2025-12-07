using Microsoft.EntityFrameworkCore;
using AnaPrevention.GeneralMasterData.Api.Common.API;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Common.Infrastructure.EF;
using AnaPrevention.GeneralMasterData.Api.CreditTimes.Domain.Entities;

namespace AnaPrevention.GeneralMasterData.Api.CreditTimes.Infrastructure.Repositories
{
    public class CreditTimeRepository : Repository<CreditTime>
    {
        readonly int maxRowPageSize = CommonStatic.MaxRowPageSize;

        public CreditTimeRepository(AnaPreventionContext context) : base(context)
        {
        }

        public CreditTime? GetDtoById(Guid id)
        {
            return _context.Set<CreditTime>().SingleOrDefault(x => x.Id == id);
        }
        public CreditTime? GetbyDescription(string description)
        {
            return _context.Set<CreditTime>().SingleOrDefault(x => x.Description == description);
        }
        public CreditTime? GetbyCode(string code)
        {
            return _context.Set<CreditTime>().SingleOrDefault(x => x.Code == code);
        }
        public bool DescriptionTakenForEdit(Guid creditTime, string description)
        {
            return _context.Set<CreditTime>().Any(c => c.Id != creditTime && c.Description == description);
        }
        public bool CodeTakenForEdit(Guid creditTime, string code)
        {
            return _context.Set<CreditTime>().Any(c => c.Id != creditTime && c.Code == code);
        }

        public List<CreditTime> GetListAll()
        {
            return _context.Set<CreditTime>().Where(t1 => t1.Status).OrderBy(t1 => t1.Description).ToList();
        }

        public CreditTime? GetCashId()
        {
            return _context.Set<CreditTime>().FirstOrDefault(t1 => t1.NumberDay == 0 && t1.Status);
        }

        public List<CreditTime> GetListFilter(bool status = true, string descriptionSearch = "", string codeSearch = "")
        {
            var query = _context.Set<CreditTime>().Where(t1 => t1.Status == status);
            if (!string.IsNullOrEmpty(descriptionSearch))
                query = query.Where(t1 => EF.Functions.Like(t1.Description, "%"+descriptionSearch+ "%"));
            if (!string.IsNullOrEmpty(codeSearch))
                query = query.Where(t1 => t1.Description.Contains(codeSearch));
            return query.OrderBy(t1 => t1.Code).ToList();
        }
        public Tuple<IEnumerable<CreditTime>, PaginationMetadata> GetList(int pageNumber, int pageSize, bool status = true, string descriptionSearch = "", string codeSearch = "")
        {
            if (pageSize > maxRowPageSize)
                pageSize = maxRowPageSize;

            var query = _context.Set<CreditTime>().Where(t1 => t1.Status == status);


            if (!string.IsNullOrEmpty(descriptionSearch))
                query = query = query.Where(t1 => EF.Functions.Like(t1.Description, "%"+descriptionSearch+ "%"));
            if (!string.IsNullOrEmpty(codeSearch))
                query = query = query.Where(t1 => t1.Description.Contains(codeSearch));

            var listBusinessProject = query.OrderBy(t1 => t1.Description)
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize).ToList();
            int totalItemCount = query.Count();

            var paginationMetadata = new PaginationMetadata(
              totalItemCount, pageSize, pageNumber);

            return new Tuple<IEnumerable<CreditTime>, PaginationMetadata>
                (listBusinessProject, paginationMetadata);
        }
    }
}

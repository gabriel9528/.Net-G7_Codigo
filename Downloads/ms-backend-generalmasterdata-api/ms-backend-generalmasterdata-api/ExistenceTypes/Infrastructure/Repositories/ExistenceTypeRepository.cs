using Microsoft.EntityFrameworkCore;
using AnaPrevention.GeneralMasterData.Api.Common.API;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Common.Infrastructure.EF;
using AnaPrevention.GeneralMasterData.Api.ExistenceTypes.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.EconomicActivities.Domain.Entities;

namespace AnaPrevention.GeneralMasterData.Api.ExistenceTypes.Infrastructure.Repositories
{
    public class ExistenceTypeRepository : Repository<ExistenceType>
    {
        readonly int maxRowPageSize = CommonStatic.MaxRowPageSize;

        public ExistenceTypeRepository(AnaPreventionContext context) : base(context)
        {
        }

        public ExistenceType? GetDtoById(Guid id)
        {
            return _context.Set<ExistenceType>().SingleOrDefault(x => x.Id == id);
        }
        public ExistenceType? GetbyDescription(string description)
        {
            return _context.Set<ExistenceType>().SingleOrDefault(x => x.Description == description);
        }
        public ExistenceType? GetbyCode(string code)
        {
            return _context.Set<ExistenceType>().SingleOrDefault(x => x.Code == code);
        }
        public bool DescriptionTakenForEdit(Guid existenceType, string description)
        {
            return _context.Set<ExistenceType>().Any(c => c.Id != existenceType && c.Description == description);
        }
        public bool CodeTakenForEdit(Guid existenceType, string code)
        {
            return _context.Set<ExistenceType>().Any(c => c.Id != existenceType && c.Code == code);
        }
        
        public List<ExistenceType> GetListAll()
        {
            return _context.Set<ExistenceType>().Where(t1 => t1.Status).OrderBy(t1 => t1.Description).ToList();
        }

        public string GenerateCode()
        {
            var codeStrings = _context.Set<ExistenceType>()
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
        public List<ExistenceType> GetListFilter(bool status = true, string descriptionSearch = "", string codeSearch = "")
        {
            var query = _context.Set<ExistenceType>().Where(t1 => t1.Status == status);
            if (!string.IsNullOrEmpty(descriptionSearch))
                query = query.Where(t1 => EF.Functions.Like(t1.Description, "%"+descriptionSearch+ "%"));
            if (!string.IsNullOrEmpty(codeSearch))
                query = query.Where(t1 => t1.Code.Contains(codeSearch));
            return query.OrderBy(t1 => t1.Description).ToList();
        }
        public Tuple<IEnumerable<ExistenceType>, PaginationMetadata> GetList(int pageNumber, int pageSize, bool status = true, string descriptionSearch = "", string codeSearch = "")
        {
            if (pageSize > maxRowPageSize)
                pageSize = maxRowPageSize;

            var query = _context.Set<ExistenceType>().Where(t1 => t1.Status == status);


            if (!string.IsNullOrEmpty(descriptionSearch))
                query = query = query.Where(t1 => EF.Functions.Like(t1.Description, "%"+descriptionSearch+ "%"));
            if (!string.IsNullOrEmpty(codeSearch))
                query = query =  query.Where(t1 => t1.Code.Contains(codeSearch));

            var listExistenceType = query.OrderBy(t1 => t1.Description)
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize).ToList();
            int totalItemCount = query.Count();

            var paginationMetadata = new PaginationMetadata(
              totalItemCount, pageSize, pageNumber);

            return new Tuple<IEnumerable<ExistenceType>, PaginationMetadata>
                (listExistenceType, paginationMetadata);
        }
    }
}

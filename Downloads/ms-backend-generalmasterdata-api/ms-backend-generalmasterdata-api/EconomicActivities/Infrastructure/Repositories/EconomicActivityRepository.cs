using Microsoft.EntityFrameworkCore;
using AnaPrevention.GeneralMasterData.Api.Common.API;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Common.Infrastructure.EF;
using AnaPrevention.GeneralMasterData.Api.EconomicActivities.Domain.Entities;

namespace AnaPrevention.GeneralMasterData.Api.EconomicActivities.Infrastructure.Repositories
{
    public class EconomicActivityRepository : Repository<EconomicActivity>
    {
        readonly int maxRowPageSize = CommonStatic.MaxRowPageSize;

        public EconomicActivityRepository(AnaPreventionContext context) : base(context)
        {
        }
        public EconomicActivity? GetbyDescription(string description)
        {
            return _context.Set<EconomicActivity>().SingleOrDefault(x => x.Description == description);
        }
        public EconomicActivity? GetbyCode(string code)
        {
            return _context.Set<EconomicActivity>().SingleOrDefault(x => x.Code == code);
        }
        public bool DescriptionTakenForEdit(Guid economicActivity, string description)
        {
            return _context.Set<EconomicActivity>().Any(c => c.Id != economicActivity && c.Description == description);
        }
        public bool CodeTakenForEdit(Guid economicActivity, string code)
        {
            return _context.Set<EconomicActivity>().Any(c => c.Id != economicActivity && c.Code == code);
        }
        public List<EconomicActivity> GetListAll()
        {
            return _context.Set<EconomicActivity>().Where(t1 => t1.Status).OrderBy(t1 => t1.Description).ToList();
        }

        public string GenerateCode()
        {
            var codeStrings = _context.Set<EconomicActivity>()
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

            // Encontrar el valor máximo
            var codeMax = codes.Count != 0 ? codes.Max() : 0;

            // Incrementar el valor máximo y generar el nuevo código con ceros a la izquierda
            var newCode = (codeMax + 1).ToString("D" + CommonStatic.numberZerosCode);

            return newCode;
        }

        public List<EconomicActivity> GetListFilter(bool status = true, string descriptionSearch = "", string codeSearch = "")
        {
            var query = _context.Set<EconomicActivity>().Where(t1 => t1.Status == status);
            if (!string.IsNullOrEmpty(descriptionSearch))
                query = query.Where(t1 => EF.Functions.Like(t1.Description, "%"+descriptionSearch+ "%"));
            if (!string.IsNullOrEmpty(codeSearch))
                query = query.Where(t1 => t1.Code.Contains(codeSearch));
            return query.OrderBy(t1 => t1.Description).ToList();
        }

        public Tuple<IEnumerable<EconomicActivity>, PaginationMetadata> GetList(
            int pageNumber, int pageSize, bool status = true, string descriptionSearch = "", string codeSearch = "")
        {
            if (pageSize > maxRowPageSize)
                pageSize = maxRowPageSize;

            var query = _context.Set<EconomicActivity>().Where(t1 => t1.Status == status);


            if (!string.IsNullOrEmpty(descriptionSearch))
                query = query = query.Where(t1 => EF.Functions.Like(t1.Description, "%"+descriptionSearch+ "%"));
            if (!string.IsNullOrEmpty(codeSearch))
                query = query = query.Where(t1 => t1.Code.Contains(codeSearch));

            var listEconomicActivity = query.OrderBy(t1 => t1.Description).Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList();
            int totalItemCount = query.Count();

            var paginationMetadata = new PaginationMetadata(
              totalItemCount, pageSize, pageNumber);

            return new Tuple<IEnumerable<EconomicActivity>, PaginationMetadata>
                (listEconomicActivity, paginationMetadata);
        }
    }
}

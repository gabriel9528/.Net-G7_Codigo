using Microsoft.EntityFrameworkCore;
using AnaPrevention.GeneralMasterData.Api.Common.API;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Common.Infrastructure.EF;
using AnaPrevention.GeneralMasterData.Api.Diagnostics.Domain.Entities;

namespace AnaPrevention.GeneralMasterData.Api.Diagnostics.Infrastructure.Repositories
{
    public class DiagnosticRepository(AnaPreventionContext context) : Repository<Diagnostic>(context)
    {
        public Diagnostic? GetbyDescription(string description)
        {
            return _context.Set<Diagnostic>().SingleOrDefault(x => x.Description == description);
        }

        public Diagnostic? GetbyCie10(string cie10)
        {
            return _context.Set<Diagnostic>().SingleOrDefault(x => x.Cie10 == cie10);
        }

        public bool DescriptionTakenForEdit(Guid diagnosticId, string description)
        {
            return _context.Set<Diagnostic>().Any(c => c.Id != diagnosticId && c.Description == description);
        }

        public bool Cie10TakenForEdit(Guid diagnosticId, string cie10)
        {
            return _context.Set<Diagnostic>().Any(c => c.Id != diagnosticId && c.Cie10 == cie10);
        }

        public List<Diagnostic> GetListAll(string? description, string? cie10Search)
        {
            if (string.IsNullOrWhiteSpace(description))
            {
                description = "";
            }
            if (string.IsNullOrWhiteSpace(cie10Search))
            {
                cie10Search = "";
            }

            return _context.Set<Diagnostic>().Where(t1 => t1.Status && t1.Description.Contains(description) && t1.Cie10.Contains(cie10Search)).OrderBy(t1 => t1.Description).Take(CommonStatic.MaxRowAutocomplete).ToList();
        }

        public List<Diagnostic> GetListFilter(bool status = true, string descriptionSearch = "", string description2Search = "", string cie10Search = "")
        {
            var query = _context.Set<Diagnostic>().Where(t1 => t1.Status == status);
            if (!string.IsNullOrEmpty(descriptionSearch))
                query = query.Where(t1 => EF.Functions.Like(t1.Description, "%"+descriptionSearch+ "%"));
            if (!string.IsNullOrEmpty(description2Search))
                query = query.Where(t1 => t1.Description2 != null && t1.Description2.Contains(description2Search));
            if (!string.IsNullOrEmpty(cie10Search))
                query = query.Where(t1 => t1.Cie10.Contains(cie10Search));
            return query.OrderBy(t1 => t1.Cie10).ToList();
        }
        public Tuple<IEnumerable<Diagnostic>, PaginationMetadata> GetList(int pageNumber, int pageSize, bool status, string? descriptionSearch, string? description2Search, string? cie10Search)
        {
            var query = _context.Set<Diagnostic>().Where(t1 => t1.Status == status);


            if (!string.IsNullOrEmpty(descriptionSearch))
                query = query = query.Where(t1 => EF.Functions.Like(t1.Description, "%"+descriptionSearch+ "%"));
            if (!string.IsNullOrEmpty(description2Search))
                query = query.Where(t1 => t1.Description2 != null && t1.Description2.Contains(description2Search)); ;
            if (!string.IsNullOrEmpty(cie10Search))
                query = query = query.Where(t1 => t1.Cie10.Contains(cie10Search));

            var listDiagnostic = query.OrderBy(t1 => t1.Description).Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList();
            int totalItemCount = query.Count();

            var paginationMetadata = new PaginationMetadata(
              totalItemCount, pageSize, pageNumber);

            return new Tuple<IEnumerable<Diagnostic>, PaginationMetadata>
                (listDiagnostic, paginationMetadata);
        }


    }
}


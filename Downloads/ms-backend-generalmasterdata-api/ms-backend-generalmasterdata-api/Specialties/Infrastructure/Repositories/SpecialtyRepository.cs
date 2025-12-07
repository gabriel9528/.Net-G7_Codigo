using Microsoft.EntityFrameworkCore;
using AnaPrevention.GeneralMasterData.Api.Common.API;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Common.Infrastructure.EF;
using AnaPrevention.GeneralMasterData.Api.Specialties.Entities;

namespace AnaPrevention.GeneralMasterData.Api.Specialties.Infrastructure.Repositories
{
    public class SpecialtyRepository : Repository<Specialty>
    {
        readonly int maxRowPageSize = CommonStatic.MaxRowPageSize;

        public SpecialtyRepository(AnaPreventionContext context) : base(context)
        {
        }

        public Specialty? GetDtoById(Guid id)
        {
            return _context.Set<Specialty>().SingleOrDefault(t1 => t1.Id == id);
        }

        public Specialty? GetbyDescription(string description)
        {
            return _context.Set<Specialty>().SingleOrDefault(t1 => t1.Description == description && t1.Status);
        }

        public bool DescriptionTakenForEdit(Guid specialtyId, string description)
        {
            return _context.Set<Specialty>().Any(t1 => t1.Id != specialtyId && t1.Description == description && t1.Status);
        }

        public bool CodeTakenForEdit(Guid specialtyId, string code)
        {
            return _context.Set<Specialty>().Any(t1 => t1.Id != specialtyId && t1.Code == code && t1.Status);
        }

        public Specialty? GetbyCode(string code)
        {
            return _context.Set<Specialty>().SingleOrDefault(t1 => t1.Code == code);
        }

        public List<Specialty> GetListAll()
        {
            return _context.Set<Specialty>().Where(t1 => t1.Status).OrderBy(t1 => t1.Description).ToList();
        }
        public string GenerateCode()
        {
            var codeStrings = _context.Set<Specialty>()
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

            ;

            var codeMax = codes.Count != 0 ? codes.Max() : 0;

            var newCode = (codeMax + 1).ToString("D" + CommonStatic.numberZerosCode);

            return newCode;
        }
        public List<Specialty> GetListFilter(bool status = true, string descriptionSearch = "", string codeSearch = "")
        {
            var query = _context.Set<Specialty>().Where(t1 => t1.Status == status);
            if (!string.IsNullOrEmpty(descriptionSearch))
                query = query.Where(t1 => EF.Functions.Like(t1.Description, "%"+descriptionSearch+ "%"));
            if (!string.IsNullOrEmpty(codeSearch))
                query = query.Where(t1 => t1.Code.Contains(codeSearch));
            return query.OrderBy(t1 => t1.Description).ToList();
        }
        public Tuple<IEnumerable<Specialty>, PaginationMetadata> GetList(int pageNumber, int pageSize, bool status = true, string descriptionSearch = "", string codeSearch = "")
        {
            if (pageSize > maxRowPageSize)
                pageSize = maxRowPageSize;

            var query = _context.Set<Specialty>().Where(t1 => t1.Status == status);


            if (!string.IsNullOrEmpty(descriptionSearch))
                query = query = query.Where(t1 => EF.Functions.Like(t1.Description, "%"+descriptionSearch+ "%"));
            if (!string.IsNullOrEmpty(codeSearch))
                query = query = query.Where(t1 => t1.Code.Contains(codeSearch));

            var listSpecialty = query.OrderBy(t1 => t1.Description).Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList();
            int totalItemCount = query.Count();

            var paginationMetadata = new PaginationMetadata(
              totalItemCount, pageSize, pageNumber);

            return new Tuple<IEnumerable<Specialty>, PaginationMetadata>
                (listSpecialty, paginationMetadata);
        }
    }
}

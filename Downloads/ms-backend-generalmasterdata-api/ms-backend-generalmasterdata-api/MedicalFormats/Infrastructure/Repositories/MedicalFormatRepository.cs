using Microsoft.EntityFrameworkCore;
using AnaPrevention.GeneralMasterData.Api.Common.API;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Common.Infrastructure.EF;
using AnaPrevention.GeneralMasterData.Api.ServiceCatalogs.Domain.Entities;

namespace AnaPrevention.GeneralMasterData.Api.ServiceCatalogs.Infrastructure.Repositories
{
    public class MedicalFormatRepository : Repository<MedicalFormat>
    {
        readonly int maxRowPageSize = CommonStatic.MaxRowPageSize;
        public MedicalFormatRepository(AnaPreventionContext context) : base(context)
        {
        }

        public MedicalFormat? GetbyDescription(string description)
        {
            return _context.Set<MedicalFormat>().SingleOrDefault(x => x.Description == description);
        }
        public MedicalFormat? GetbyCode(string code)
        {
            return _context.Set<MedicalFormat>().SingleOrDefault(x => x.Code == code);
        }
        public bool DescriptionTakenForEdit(Guid medicalFormat, string description)
        {
            return _context.Set<MedicalFormat>().Any(c => c.Id != medicalFormat && c.Description == description);
        }
        public bool CodeTakenForEdit(Guid medicalFormat, string code)
        {
            return _context.Set<MedicalFormat>().Any(c => c.Id != medicalFormat && c.Code == code);
        }
        public List<MedicalFormat> GetListAll()
        {
            return _context.Set<MedicalFormat>().Where(t1 => t1.Status).OrderBy(t1 => t1.Description).ToList();
        }

        public string GenerateCode()
        {
            var codeStrings = _context.Set<MedicalFormat>()
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

        public List<MedicalFormat> GetListFilter(bool status = true, string descriptionSearch = "", string codeSearch = "")
        {
            var query = _context.Set<MedicalFormat>().Where(t1 => t1.Status == status);

            if (!string.IsNullOrEmpty(descriptionSearch))
                query = query.Where(t1 => EF.Functions.Like(t1.Description, "%" + descriptionSearch + "%"));

            if (!string.IsNullOrEmpty(codeSearch))
                query = query.Where(t1 => t1.Code.Contains(codeSearch));

            return query.OrderBy(t1 => t1.Description).ToList();
        }

        public Tuple<IEnumerable<MedicalFormat>, PaginationMetadata> GetList(int pageNumber, int pageSize, bool status = true, string descriptionSearch = "", string codeSearch = "")
        {
            if (pageSize > maxRowPageSize)
                pageSize = maxRowPageSize;

            var query = _context.Set<MedicalFormat>().Where(t1 => t1.Status == status);

            if (!string.IsNullOrEmpty(descriptionSearch))
                query = query.Where(t1 => EF.Functions.Like(t1.Description, "%" + descriptionSearch + "%"));

            if (!string.IsNullOrEmpty(codeSearch))
                query = query.Where(t1 => t1.Code.Contains(codeSearch));

            var listMedicalFormat = query.OrderBy(t1 => t1.Description).Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList();
            int totalItemCount = query.Count();

            var paginationMetadata = new PaginationMetadata(
              totalItemCount, pageSize, pageNumber);

            return new Tuple<IEnumerable<MedicalFormat>, PaginationMetadata>
                (listMedicalFormat, paginationMetadata);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using AnaPrevention.GeneralMasterData.Api.Common.API;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Common.Infrastructure.EF;
using AnaPrevention.GeneralMasterData.Api.MeasurementUnits.Domain.Entities;

namespace AnaPrevention.GeneralMasterData.Api.MeasurementUnits.Infrastructure.Repositories
{
    public class UomRepository : Repository<Uom>
    {
        readonly int maxRowPageSize = CommonStatic.MaxRowPageSize;

        public UomRepository(AnaPreventionContext context) : base(context)
        {
        }

        public Uom? GetDtoById(Guid id)
        {
            return _context.Set<Uom>().SingleOrDefault(x => x.Id == id);
        }
        public Uom? GetbyDescription(string description)
        {
            return _context.Set<Uom>().SingleOrDefault(x => x.Description == description);
        }
        public Uom? GetbyCode(string code)
        {
            return _context.Set<Uom>().SingleOrDefault(x => x.Code == code);
        }

        public Uom? GetbyFiscalCode(string fiscalCode)
        {
            return _context.Set<Uom>().SingleOrDefault(x => x.FiscalCode == fiscalCode);
        }
        public bool DescriptionTakenForEdit(Guid uom, string description)
        {
            return _context.Set<Uom>().Any(c => c.Id != uom && c.Description == description);
        }
        public bool CodeTakenForEdit(Guid uom, string code)
        {
            return _context.Set<Uom>().Any(c => c.Id != uom && c.Code == code);
        }

        public bool FiscalCodeTakenForEdit(Guid uom, string fiscalCode)
        {
            return _context.Set<Uom>().Any(c => c.Id != uom && c.FiscalCode == fiscalCode);
        }
        public List<Uom> GetListAll()
        {
            return _context.Set<Uom>().Where(t1 => t1.Status).OrderBy(t1 => t1.Description).ToList();
        }

        public string GenerateCode()
        {
            var codeStrings = _context.Set<Uom>()
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
        public List<Uom> GetListFilter(bool status = true, string descriptionSearch = "", string codeSearch = "", string fiscalCodeSearch = "")
        {
            var query = _context.Set<Uom>().Where(t1 => t1.Status == status);
            if (!string.IsNullOrEmpty(descriptionSearch))
                query = query.Where(t1 => EF.Functions.Like(t1.Description, "%"+descriptionSearch+ "%"));
            if (!string.IsNullOrEmpty(codeSearch))
                query = query.Where(t1 => t1.Code.Contains(codeSearch));
            if (!string.IsNullOrEmpty(fiscalCodeSearch))
                query = query.Where(t1 => t1.Code.Contains(fiscalCodeSearch));
            return query.OrderBy(t1 => t1.Description).ToList();
        }
        public Tuple<IEnumerable<Uom>, PaginationMetadata> GetList(
            int pageNumber, int pageSize, bool status = true, string descriptionSearch = "", string codeSearch = "", string fiscalCodeSearch = "")
        {
            if (pageSize > maxRowPageSize)
                pageSize = maxRowPageSize;

            var query = _context.Set<Uom>().Where(t1 => t1.Status == status);


            if (!string.IsNullOrEmpty(descriptionSearch))
                query = query = query.Where(t1 => EF.Functions.Like(t1.Description, "%"+descriptionSearch+ "%"));
            if (!string.IsNullOrEmpty(codeSearch))
                query = query = query.Where(t1 => t1.Code.Contains(codeSearch));
            if (!string.IsNullOrEmpty(fiscalCodeSearch))
                query = query = query.Where(t1 => t1.Code.Contains(fiscalCodeSearch));

            var listUom = query.OrderBy(t1 => t1.Description).Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList();
            int totalItemCount = query.Count();

            var paginationMetadata = new PaginationMetadata(
              totalItemCount, pageSize, pageNumber);

            return new Tuple<IEnumerable<Uom>, PaginationMetadata>
                (listUom, paginationMetadata);
        }
    }
}

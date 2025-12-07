using Microsoft.EntityFrameworkCore;
using AnaPrevention.GeneralMasterData.Api.Common.API;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Common.Infrastructure.EF;
using AnaPrevention.GeneralMasterData.Api.Families.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Lines.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.Lines.Domain.Entities;

namespace AnaPrevention.GeneralMasterData.Api.Lines.Infrastructure.Repositories
{
    public class LineRepository : Repository<Line>
    {
        readonly int maxRowPageSize = CommonStatic.MaxRowPageSize;

        public LineRepository(AnaPreventionContext context) : base(context)
        {
        }

        public List<Line>? GetByIds(List<Guid> ids)
        {
            return _context.Set<Line>().Where(t1 => ids.Contains(t1.Id)).ToList();
        }
        public LineDto? GetDtoById(Guid id, Guid companyId)
        {
            return GetDtoQueryable().SingleOrDefault(t1 => t1.Id == id && t1.CompanyId == companyId);
        }
        public Line? GetbyDescription(string description, Guid companyId)
        {
            return _context.Set<Line>().SingleOrDefault(x => x.Description == description && x.CompanyId == companyId);
        }
        public Line? GetbyCode(string code, Guid companyId)
        {
            return _context.Set<Line>().SingleOrDefault(x => x.Code == code &&
                                                        x.CompanyId == companyId);
        }
        public bool DescriptionTakenForEdit(Guid id, string description, Guid companyId)
        {
            return _context.Set<Line>().Any(
                                                        c => c.Id != id &&
                                                        c.Description == description &&
                                                        c.CompanyId == companyId
                                                        );
        }

        public bool CodeTakenForEdit(Guid id, string code, Guid companyId)
        {
            return _context.Set<Line>().Any(
                                                        c => c.Id != id &&
                                                        c.Code == code &&
                                                        c.CompanyId == companyId
                                                        );
        }
        public List<LineDto> GetListAll(Guid companyId)
        {
            return GetDtoQueryable().Where(t1 => t1.Status && t1.CompanyId == companyId).ToList();
        }
        public string GenerateCode(Guid companyId)
        {
             var codeStrings = _context.Set<Line>().Where(t1 => t1.CompanyId == companyId)
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
        public List<LineDto> GetListFilter(Guid companyId, bool status = true, string descriptionSearch = "", string codeSearch = "")
        {
            var query = GetDtoQueryable().Where(t1 => t1.Status == status && t1.CompanyId == companyId);

            if (!string.IsNullOrEmpty(descriptionSearch))
                query = query.Where(t1 => EF.Functions.Like(t1.Description, "%" + descriptionSearch + "%"));

            if (!string.IsNullOrEmpty(codeSearch))
                query = query.Where(t1 => t1.Code.Contains(codeSearch));

            return query.OrderBy(t1 => t1.Description).ToList();
        }
        public Tuple<IEnumerable<LineDto>, PaginationMetadata> GetList(int pageNumber, int pageSize, Guid companyId, bool status = true, string descriptionSearch = "", string codeSearch = "")
        {
            if (pageSize > maxRowPageSize)
                pageSize = maxRowPageSize;

            var query = GetDtoQueryable().Where(t1 => t1.Status == status && t1.CompanyId == companyId);

            if (!string.IsNullOrEmpty(descriptionSearch))
                query = query.Where(t1 => EF.Functions.Like(t1.Description, "%" + descriptionSearch + "%"));

            if (!string.IsNullOrEmpty(codeSearch))
                query = query.Where(t1 => t1.Code.Contains(codeSearch));

            var listLineDto = query.Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList();
            int totalItemCount = query.Count();

            var paginationMetadata = new PaginationMetadata(
              totalItemCount, pageSize, pageNumber);
            return new Tuple<IEnumerable<LineDto>, PaginationMetadata>
                (listLineDto, paginationMetadata);
        }

        private IQueryable<LineDto> GetDtoQueryable()
        {
            return (from t1 in _context.Set<Line>()
                    join t2 in _context.Set<LineType>() on t1.LineTypeId equals t2.Id
                    orderby t1.Description
                    select new LineDto()
                    {
                        Id = t1.Id,
                        CompanyId = t1.CompanyId,
                        Code = t1.Code,
                        Description = t1.Description,
                        Status = t1.Status,
                        LineType = t2.Description,
                        LineTypeId = t1.LineTypeId,
                        CodeLineType = t2.Code,
                    });
        }
    }
}

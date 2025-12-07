using Microsoft.EntityFrameworkCore;
using AnaPrevention.GeneralMasterData.Api.Common.API;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Common.Infrastructure.EF;
using AnaPrevention.GeneralMasterData.Api.Families.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.Families.Domain.Entities;

namespace AnaPrevention.GeneralMasterData.Api.Families.Infrastructure.Repositories
{
    public class FamilyRepository : Repository<Family>
    {
        readonly int maxRowPageSize = CommonStatic.MaxRowPageSize;
        public FamilyRepository(AnaPreventionContext context) : base(context)
        {
        }

        public List<Family>? GetByIds(List<Guid> ids)
        {
            return _context.Set<Family>().Where(t1 => ids.Contains(t1.Id)).ToList();
        }
        public FamilyDto? GetDtoById(Guid id, Guid companyId)
        {
            return GetDtoQueryable().SingleOrDefault(t1 => t1.Id == id && t1.CompanyId == companyId);
        }
        public Family? GetbyDescription(string description, Guid companyId, Guid lineId)
        {
            return _context.Set<Family>().SingleOrDefault(x => x.Description == description && x.CompanyId == companyId && x.LineId == lineId);
        }
        public Family? GetbyCode(string code, Guid companyId)
        {
            return _context.Set<Family>().SingleOrDefault(x => x.Code == code && x.CompanyId == companyId);
        }
        public bool DescriptionTakenForEdit(Guid FamilyId, string description, Guid companyId, Guid lineId)
        {
            return _context.Set<Family>().Any(
                                                        c => c.Id != FamilyId &&
                                                        c.Description == description &&
                                                        c.CompanyId == companyId &&
                                                        c.LineId == lineId
                                                        );
        }

        public bool CodeTakenForEdit(Guid FamilyId, string code, Guid companyId, Guid lineId)
        {
            return _context.Set<Family>().Any(
                                                        c => c.Id != FamilyId &&
                                                        c.Code == code &&
                                                        c.CompanyId == companyId &&
                                                        c.LineId == lineId
                                                        );
        }
        public List<FamilyDto> GetListAllByLineId(Guid lineId)
        {
            return GetDtoQueryable().Where(t1 => t1.LineId == lineId && t1.Status).ToList();
        }
        public List<FamilyDto> GetListAll()
        {
            return GetDtoQueryable().Where(t1 => t1.Status).ToList();
        }

        public string GenerateCode(Guid companyId)
        {
           var codeStrings = _context.Set<Family>().Where(t1 => t1.CompanyId == companyId)
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
        public List<FamilyDto> GetListFilter(Guid companyId, bool status = true, string descriptionSearch = "", string codeSearch = "")
        {
            var query = GetDtoQueryable().Where(t1 => t1.Status == status && t1.CompanyId == companyId);

            if (!string.IsNullOrEmpty(descriptionSearch))
                query = query.Where(t1 => EF.Functions.Like(t1.Description, "%" + descriptionSearch + "%"));

            if (!string.IsNullOrEmpty(codeSearch))
                query = query.Where(t1 => t1.Code.Contains(codeSearch));

            return query.OrderBy(t1 => t1.Description).ToList();
        }

        public Tuple<IEnumerable<FamilyDto>, PaginationMetadata> GetList(int pageNumber, int pageSize, Guid companyId, bool status = true, string descriptionSearch = "", string codeSearch = "")
        {
            if (pageSize > maxRowPageSize)
                pageSize = maxRowPageSize;

            var query = GetDtoQueryable().Where(t1 => t1.Status == status && t1.CompanyId == companyId);

            if (!string.IsNullOrEmpty(descriptionSearch))
                query = query.Where(t1 => EF.Functions.Like(t1.Description, "%" + descriptionSearch + "%"));

            if (!string.IsNullOrEmpty(codeSearch))
                query = query.Where(t1 => t1.Code.Contains(codeSearch));

            var listFamilyDto = query.OrderBy(t1 => t1.Description)
               .Skip(pageSize * (pageNumber - 1))
               .Take(pageSize).ToList();
            int totalItemCount = query.Count();


            var paginationMetadata = new PaginationMetadata(
             totalItemCount, pageSize, pageNumber);

            return new Tuple<IEnumerable<FamilyDto>, PaginationMetadata>
                (listFamilyDto, paginationMetadata);
        }

        private IQueryable<FamilyDto> GetDtoQueryable()
        {
            return (from t1 in _context.Set<Family>()
                    join t2 in _context.Set<Line>() on t1.LineId equals t2.Id
                    where
                         t1.Status
                    select new FamilyDto()
                    {
                        Id = t1.Id,
                        Code = t1.Code,
                        Description = t1.Description,
                        CompanyId = t1.CompanyId,
                        Status = t1.Status,
                        LineDescripcion = t2.Description,
                        LineId = t1.LineId
                    });
        }

    }
}

using Microsoft.EntityFrameworkCore;
using AnaPrevention.GeneralMasterData.Api.Common.API;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Common.Infrastructure.EF;
using AnaPrevention.GeneralMasterData.Api.Families.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.SubFamilies.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.SubFamilies.Domain.Entities;

namespace AnaPrevention.GeneralMasterData.Api.SubFamilies.Infrastructure.Repositories
{
    public class SubFamilyRepository : Repository<SubFamily>
    {
        readonly int maxRowPageSize = CommonStatic.MaxRowPageSize;
        public SubFamilyRepository(AnaPreventionContext context) : base(context)
        {
        }

        public List<SubFamily>? GetByIds(List<Guid> ids)
        {
            return _context.Set<SubFamily>().Where(t1 => ids.Contains(t1.Id)).ToList();
        }
        public SubFamilyDto? GetDtoById(Guid id, Guid companyId)
        {
            return GetDtoQueryable().SingleOrDefault(t1 => t1.Id == id && t1.CompanyId == companyId);
        }
        public SubFamily? GetbyDescription(string description, Guid companyId, Guid familyId)
        {
            return _context.Set<SubFamily>().SingleOrDefault(x => x.Description == description && x.CompanyId == companyId && x.FamilyId == familyId);
        }
        public SubFamily? GetbyCode(string code, Guid companyId)
        {
            return _context.Set<SubFamily>().SingleOrDefault(x => x.Code == code && x.CompanyId == companyId);
        }
        public bool DescriptionTakenForEdit(Guid SubFamilyId, string description, Guid companyId, Guid familyId)
        {
            return _context.Set<SubFamily>().Any(
                                                        c => c.Id != SubFamilyId &&
                                                        c.Description == description &&
                                                        c.CompanyId == companyId &&
                                                        c.FamilyId == familyId
                                                        );
        }
        public bool CodeTakenForEdit(Guid SubFamilyId, string code, Guid companyId)
        {
            return _context.Set<SubFamily>().Any(
                                                        c => c.Id != SubFamilyId &&
                                                        c.Code == code &&
                                                        c.CompanyId == companyId
                                                        );
        }
        public List<SubFamilyDto> GetListAllByFamilyId(Guid familiId)
        {
            return GetDtoQueryable().Where(t1 => t1.FamilyId == familiId && t1.Status).ToList();
        }
        public List<SubFamilyDto> GetListAll()
        {
            return GetDtoQueryable().Where(t1 => t1.Status).ToList();
        }

        public string GenerateCode(Guid companyId)
        {
           var codeStrings = _context.Set<SubFamily>().Where(t1 => t1.CompanyId == companyId)
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
        public List<SubFamilyDto> GetListFilter(Guid companyId, bool status = true, string descriptionSearch = "", string codeSearch = "")
        {
            var query = GetDtoQueryable().Where(t1 => t1.Status == status && t1.CompanyId == companyId);

            if (!string.IsNullOrEmpty(descriptionSearch))
                query = query.Where(t1 => EF.Functions.Like(t1.Description, "%" + descriptionSearch + "%"));

            if (!string.IsNullOrEmpty(codeSearch))
                query = query.Where(t1 => t1.Code.Contains(codeSearch));

            return query.OrderBy(t1 => t1.Description).ToList();
        }
        public Tuple<IEnumerable<SubFamilyDto>, PaginationMetadata> GetList(int pageNumber, int pageSize, Guid companyId, bool status = true, string descriptionSearch = "", string codeSearch = "")
        {
            if (pageSize > maxRowPageSize)
                pageSize = maxRowPageSize;

            var query = GetDtoQueryable().Where(t1 => t1.Status == status && t1.CompanyId == companyId);

            if (!string.IsNullOrEmpty(descriptionSearch))
                query = query.Where(t1 => EF.Functions.Like(t1.Description, "%" + descriptionSearch + "%"));

            if (!string.IsNullOrEmpty(codeSearch))
                query = query.Where(t1 => t1.Code.Contains(codeSearch));

            var subFamilies = query.OrderBy(t1 => t1.Description).Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList();
            int totalItemCount = query.Count();

            var paginationMetadata = new PaginationMetadata(
             totalItemCount, pageSize, pageNumber);
            return new Tuple<IEnumerable<SubFamilyDto>, PaginationMetadata>
                (subFamilies, paginationMetadata);
        }
        private IQueryable<SubFamilyDto> GetDtoQueryable()
        {
            return (from t1 in _context.Set<SubFamily>()
                    join t2 in _context.Set<Family>() on t1.FamilyId equals t2.Id
                    select new SubFamilyDto()
                    {
                        Id = t1.Id,
                        Code = t1.Code,
                        Description = t1.Description,
                        CompanyId = t1.CompanyId,
                        SubFamilyType = t1.SubFamilyType,
                        Status = t1.Status,
                        FamilyId = t1.FamilyId,
                        FamilyDescription = t2.Description
                    });
        }

    }
}

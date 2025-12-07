using Microsoft.EntityFrameworkCore;
using AnaPrevention.GeneralMasterData.Api.Common.API;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Common.Infrastructure.EF;
using AnaPrevention.GeneralMasterData.Api.Dimensions.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.Dimensions.Domain.Entities;

namespace AnaPrevention.GeneralMasterData.Api.Dimensions.Infrastructure.Repositories
{
    public class DimensionRepository : Repository<Dimension>
    {
        readonly int maxRowPageSize = CommonStatic.MaxRowPageSize;


        public DimensionRepository(AnaPreventionContext context) : base(context)
        {
        }

        public DimensionDto? GetDtoById(Guid id, Guid companyId)
        {
            return GetDtoQueryable().Where(t1 => t1.Id == id && t1.CompanyId == companyId).SingleOrDefault();
        }
        public Dimension? GetbyDescription(string description, Guid companyId)
        {
            return _context.Set<Dimension>().SingleOrDefault(x => x.Description == description && x.CompanyId == companyId);
        }
        public Dimension? GetbyCode(string code)
        {
            return _context.Set<Dimension>().SingleOrDefault(x => x.Code == code);
        }
        public bool DescriptionTakenForEdit(Guid DimensionId, string description, Guid companyId)
        {
            return _context.Set<Dimension>().Any(
                                                        c => c.Id != DimensionId &&
                                                        c.Description == description &&
                                                        c.CompanyId == companyId
                                                        );
        }
        public bool CodeTakenForEdit(Guid DimensionId, string code, Guid companyId)
        {
            return _context.Set<Dimension>().Any(
                                                        c => c.Id != DimensionId &&
                                                        c.Code == code &&
                                                        c.CompanyId == companyId
                                                        );
        }

        public List<DimensionDto> GetListAll(Guid companyId)
        {
            return GetDtoQueryable().Where(t1 => t1.CompanyId == companyId).ToList();
        }

        public string GenerateCode(Guid companyId)
        {

           var codeStrings = _context.Set<Dimension>().Where(t1 => t1.CompanyId == companyId)
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

        public List<DimensionDto> GetListFilter(Guid companyId, bool status = true, string descriptionSearch = "", string codeSearch = "")
        {
            var query = GetDtoQueryable().Where(t1 => t1.Status == status && t1.CompanyId == companyId);

            if (!string.IsNullOrEmpty(descriptionSearch))
                query = query.Where(t1 => EF.Functions.Like(t1.Description, "%" + descriptionSearch + "%"));

            if (!string.IsNullOrEmpty(codeSearch))
                query = query.Where(t1 => t1.Code.Contains(codeSearch));

            return query.OrderBy(t1 => t1.Description).ToList();
        }

        public Tuple<IEnumerable<DimensionDto>, PaginationMetadata> GetList(int pageNumber, int pageSize, Guid companyId, bool status = true, string descriptionSearch = "", string codeSearch = "")
        {
            if (pageSize > maxRowPageSize)
                pageSize = maxRowPageSize;


            var query = GetDtoQueryable().Where(t1 => t1.Status == status && t1.CompanyId == companyId);

            if (!string.IsNullOrEmpty(descriptionSearch))
                query = query.Where(t1 => EF.Functions.Like(t1.Description, "%" + descriptionSearch + "%"));

            if (!string.IsNullOrEmpty(codeSearch))
                query = query.Where(t1 => t1.Code.Contains(codeSearch));

            var listDimensionDto = query.OrderBy(t1 => t1.Description).Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList();
            int totalItemCount = query.Count();

            var paginationMetadata = new PaginationMetadata(
              totalItemCount, pageSize, pageNumber);

            return new Tuple<IEnumerable<DimensionDto>, PaginationMetadata>
                (listDimensionDto, paginationMetadata);
        }
        private IQueryable<DimensionDto> GetDtoQueryable()
        {
            return (from t1 in _context.Set<Dimension>()
                    select new DimensionDto()
                    {
                        Id = t1.Id,
                        Code = t1.Code,
                        Description = t1.Description,
                        CompanyId = t1.CompanyId,
                        Status = t1.Status,
                        CostCenters = (_context.Set<CostCenter>().Where(x => x.DimensionId == t1.Id).ToList())
                    });
        }

    }
}

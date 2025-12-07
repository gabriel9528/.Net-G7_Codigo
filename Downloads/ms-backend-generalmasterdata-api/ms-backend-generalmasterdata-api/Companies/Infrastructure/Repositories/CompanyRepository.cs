using Microsoft.EntityFrameworkCore;
using AnaPrevention.GeneralMasterData.Api.Common.API;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Common.Infrastructure.EF;
using AnaPrevention.GeneralMasterData.Api.Companies.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.Companies.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Companies.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Dtos;

namespace AnaPrevention.GeneralMasterData.Api.Companies.Infrastructure.Repositories
{
    public class CompanyRepository : Repository<Company>
    {
        readonly int maxRowPageSize = CommonStatic.MaxRowPageSize;

        public CompanyRepository(AnaPreventionContext context) : base(context)
        {
        }

        public Company? GetbyDescription(string description)
        {
            return _context.Set<Company>().SingleOrDefault(t1 => t1.Description == description && t1.Status);
        }

        public bool DescriptionTakenForEdit(Guid id, string description)
        {
            return _context.Set<Company>().Any(t1 => t1.Id != id && t1.Description == description && t1.Status);
        }
        public CompanyDto? GetDtoById(Guid id)
        {
            var company = _context.Set<Company>().SingleOrDefault(t1 => t1.Id == id);

            CompanyDto? companyDto = null;
            if (company != null)
            {
                companyDto = new()
                {
                    Id = company.Id,
                    Description = company.Description,
                    Status = company.Status,
                    Setting = CommonStatic.ConvertJsonToSettingDto(company.Setting),

                };
            }

            return companyDto;
        }
        public List<CompanyDto> GetListAll(Guid userId)
        {
            return GetQueryableDtoByUserId(userId).Where(t1 => t1.Status).OrderBy(t1 => t1.Description).ToList();
        }

        public List<CompanyDto> GetListFilter(Guid userId, bool status = true, string descriptionSearch = "")
        {
            var query = GetQueryableDtoByUserId(userId).Where(t1 => t1.Status == status);
            if (!string.IsNullOrEmpty(descriptionSearch))
                query = query.Where(t1 => EF.Functions.Like(t1.Description, "%" + descriptionSearch + "%"));

            return query.OrderBy(t1 => t1.Description).ToList();
        }

        public Tuple<IEnumerable<CompanyDto>, PaginationMetadata> GetList(int pageNumber, int pageSize, Guid userId, bool status = true, string descriptionSearch = "")
        {
            if (pageSize > maxRowPageSize)
                pageSize = maxRowPageSize;

            var query = GetQueryableDtoByUserId(userId).Where(t1 => t1.Status == status);


            if (!string.IsNullOrEmpty(descriptionSearch))
                query = query.Where(t1 => EF.Functions.Like(t1.Description, "%" + descriptionSearch + "%"));

            var list = query.OrderBy(t1 => t1.Description)
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize).ToList();
            int totalItemCount = query.Count();

            var paginationMetadata = new PaginationMetadata(
              totalItemCount, pageSize, pageNumber);

            return new Tuple<IEnumerable<CompanyDto>, PaginationMetadata>
                (list, paginationMetadata);
        }

        public IQueryable<CompanyDto> GetQueryableDtoByUserId(Guid userId)
        {
            return (from t1 in _context.Set<UserByCompany>()
                    select new CompanyDto()
                    {
                        Id = t1.Id,
                        Description = t1.Description,
                        Status = t1.Status,
                        Setting = CompanyStatic.ConvertJsonToSettingDto(t1.Setting)
                    }
                    );
        }
    }
}

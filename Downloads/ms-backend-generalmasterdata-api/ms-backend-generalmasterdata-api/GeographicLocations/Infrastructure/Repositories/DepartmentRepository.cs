using Microsoft.EntityFrameworkCore;
using AnaPrevention.GeneralMasterData.Api.Common.API;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Common.Infrastructure.EF;
using AnaPrevention.GeneralMasterData.Api.GeographicLocations.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.GeographicLocations.Domain.Entities;

namespace AnaPrevention.GeneralMasterData.Api.GeographicLocations.Infrastructure.Repositories
{
    public class DepartmentRepository : Repository<Department>
    {
        readonly int maxRowPageSize = CommonStatic.MaxRowPageSize;
        public DepartmentRepository(AnaPreventionContext context) : base(context)
        {
        }
        public DepartmentDto? GetDtoById(string id)
        {
            return GetDtoQueryable().SingleOrDefault(t1 => t1.Id == id);
        }
        public Department? GetbyDescription(string description)
        {
            return _context.Set<Department>().SingleOrDefault(x => x.Description == description);
        }

        public List<DepartmentDto> GetListAutoComplete(string descriptionSearch = "")
        {
            var query = GetDtoQueryable().Where(t1 => t1.Status);

            if (!string.IsNullOrEmpty(descriptionSearch))
                query = query.Where(t1 => EF.Functions.Like(t1.Description, "%" + descriptionSearch + "%"));

            return query.OrderBy(t1 => t1.Description).ToList().Take(CommonStatic.MaxRowAutocomplete).ToList();

        }

        public List<DepartmentDto> GetListAllByCountryId(string countryId)
        {
            return GetDtoQueryable().Where(t1 => t1.CountryId == countryId && t1.Status).ToList();
        }

        public Tuple<IEnumerable<DepartmentDto>, PaginationMetadata> GetList(int pageNumber, int pageSize, bool status = true, string descriptionSearch = "", string searchId = "")
        {
            if (pageSize > maxRowPageSize)
                pageSize = maxRowPageSize;

            var query = GetDtoQueryable().Where(t1 => t1.Status);

            if (!string.IsNullOrEmpty(descriptionSearch))
                query = query.Where(t1 => EF.Functions.Like(t1.Description, "%" + descriptionSearch + "%"));

            if (!string.IsNullOrEmpty(searchId))
                query = query.Where(t1 => t1.Id.Contains(searchId));

            var listDepartmentDto = query
               .Skip(pageSize * (pageNumber - 1))
               .Take(pageSize).ToList();
            int totalItemCount = query.Count();


            var paginationMetadata = new PaginationMetadata(
              totalItemCount, pageSize, pageNumber);

            return new Tuple<IEnumerable<DepartmentDto>, PaginationMetadata>
                (listDepartmentDto, paginationMetadata);
        }

        private IQueryable<DepartmentDto> GetDtoQueryable()
        {
            return (from t1 in _context.Set<Department>()
                    join t2 in _context.Set<Country>() on t1.CountryId equals t2.Id
                    orderby t1.Description
                    select new DepartmentDto()
                    {
                        Id = t1.Id,
                        Description = t1.Description,
                        CountryId = t1.CountryId,
                        Country = t2.Description,
                        Status = t1.Status,

                    });
        }
    }
}

using Microsoft.EntityFrameworkCore;
using AnaPrevention.GeneralMasterData.Api.Common.API;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Common.Infrastructure.EF;
using AnaPrevention.GeneralMasterData.Api.GeographicLocations.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.GeographicLocations.Domain.Entities;

namespace AnaPrevention.GeneralMasterData.Api.GeographicLocations.Infrastructure.Repositories
{
    public class ProvinceRepository : Repository<Province>
    {
        readonly int maxRowPageSize = CommonStatic.MaxRowPageSize;
        public ProvinceRepository(AnaPreventionContext context) : base(context)
        {
        }
        public ProvinceDto? GetDtoById(string id)
        {
            return GetDtoQueryable().SingleOrDefault(t1 => t1.Id == id);
        }
        public Province? GetbyDescription(string description)
        {
            return _context.Set<Province>().SingleOrDefault(x => x.Description == description);
        }

        public List<ProvinceDto> GetListAutoComplete(string descriptionSearch = "")
        {
            var query = GetDtoQueryable().Where(t1 => t1.Status);

            if (!string.IsNullOrEmpty(descriptionSearch))
                query = query.Where(t1 => EF.Functions.Like(t1.Description, "%" + descriptionSearch + "%"));

            return query.OrderBy(t1 => t1.Description).ToList().OrderBy(t1 => t1.Description).Take(CommonStatic.MaxRowAutocomplete).ToList();

        }
        public List<ProvinceDto> GetListAllByDepartmentId(string departmentId)
        {
            return GetDtoQueryable().Where(t1 => t1.DepartmentId == departmentId && t1.Status).ToList();
        }

        public Tuple<IEnumerable<ProvinceDto>, PaginationMetadata> GetList(int pageNumber, int pageSize, bool status = true, string descriptionSearch = "", string searchId = "")
        {
            if (pageSize > maxRowPageSize)
                pageSize = maxRowPageSize;

            var query = GetDtoQueryable().Where(t1 => t1.Status);

            if (!string.IsNullOrEmpty(descriptionSearch))
                query = query.Where(t1 => EF.Functions.Like(t1.Description, "%" + descriptionSearch + "%"));

            if (!string.IsNullOrEmpty(searchId))
                query = query.Where(t1 => t1.Id.Contains(searchId));

            var listProvinceDto = query.Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList();
            int totalItemCount = query.Count();

            var paginationMetadata = new PaginationMetadata(
              totalItemCount, pageSize, pageNumber);

            return new Tuple<IEnumerable<ProvinceDto>, PaginationMetadata>
                (listProvinceDto, paginationMetadata);
        }

        private IQueryable<ProvinceDto> GetDtoQueryable()
        {
            return (from t1 in _context.Set<Province>()
                    join t2 in _context.Set<Department>() on t1.DepartmentId equals t2.Id
                    orderby t1.Description
                    select new ProvinceDto()
                    {
                        Id = t1.Id,
                        Description = t1.Description,
                        DepartmentId = t1.DepartmentId,
                        Department = t2.Description,
                        Status = t1.Status,
                    });
        }
    }
}

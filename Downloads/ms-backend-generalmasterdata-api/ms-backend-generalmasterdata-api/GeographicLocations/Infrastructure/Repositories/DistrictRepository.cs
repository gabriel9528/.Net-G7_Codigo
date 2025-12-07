using Microsoft.EntityFrameworkCore;
using AnaPrevention.GeneralMasterData.Api.Common.API;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Common.Infrastructure.EF;
using AnaPrevention.GeneralMasterData.Api.GeographicLocations.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.GeographicLocations.Domain.Entities;

namespace AnaPrevention.GeneralMasterData.Api.GeographicLocations.Infrastructure.Repositories
{
    public class DistrictRepository : Repository<District>
    {
        readonly int maxRowPageSize = CommonStatic.MaxRowPageSize;
        public DistrictRepository(AnaPreventionContext context) : base(context)
        {
        }
        public DistrictDto? GetDtoById(string id)
        {
            return GetDtoQueryable().SingleOrDefault(t1 => t1.Id == id);
        }
        public District? GetbyDescription(string description)
        {
            return _context.Set<District>().SingleOrDefault(x => x.Description == description);
        }

        public List<DistrictDto> GetListAllByProvinceId(string provinceId)
        {
            return GetDtoQueryable().Where(t1 => t1.ProvinceId == provinceId && t1.Status).ToList();
        }

        public List<DistrictDto> GetListAutoComplete(string descriptionSearch = "")
        {
            var query = GetDtoQueryable().Where(t1 => t1.Status);

            if (!string.IsNullOrEmpty(descriptionSearch))
                query = query.Where(t1 => EF.Functions.Like(t1.Description, "%" + descriptionSearch + "%"));

            return query.OrderBy(t1 => t1.Description).ToList().Take(CommonStatic.MaxRowAutocomplete).ToList();
        }

        public Tuple<IEnumerable<DistrictDto>, PaginationMetadata> GetList(int pageNumber, int pageSize, bool status = true, string descriptionSearch = "", string searchId = "")
        {
            if (pageSize > maxRowPageSize)
                pageSize = maxRowPageSize;

            var query = GetDtoQueryable().Where(t1 => t1.Status);

            if (!string.IsNullOrEmpty(descriptionSearch))
                query = query.Where(t1 => EF.Functions.Like(t1.Description, "%" + descriptionSearch + "%"));

            if (!string.IsNullOrEmpty(searchId))
                query = query.Where(t1 => t1.Id.Contains(searchId));

            var listDistrictDto = query.Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList();
            int totalItemCount = query.Count();

            var paginationMetadata = new PaginationMetadata(
              totalItemCount, pageSize, pageNumber);

            return new Tuple<IEnumerable<DistrictDto>, PaginationMetadata>
                (listDistrictDto, paginationMetadata);
        }

        private IQueryable<DistrictDto> GetDtoQueryable()
        {
            return (from t1 in _context.Set<District>()
                    join t2 in _context.Set<Province>() on t1.ProvinceId equals t2.Id
                    join t3 in _context.Set<Department>() on t2.DepartmentId equals t3.Id
                    orderby t1.Description
                    select new DistrictDto()
                    {
                        Id = t1.Id,
                        Description = t1.Description,
                        ProvinceId = t1.ProvinceId,
                        Province = t2.Description,
                        Department = t3.Description,
                        Status = t1.Status,

                    });
        }
    }
}

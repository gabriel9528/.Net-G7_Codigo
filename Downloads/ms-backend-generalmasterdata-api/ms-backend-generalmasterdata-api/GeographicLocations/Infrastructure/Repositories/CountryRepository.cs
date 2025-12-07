using Microsoft.EntityFrameworkCore;
using AnaPrevention.GeneralMasterData.Api.Common.API;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Common.Infrastructure.EF;
using AnaPrevention.GeneralMasterData.Api.GeographicLocations.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.GeographicLocations.Domain.Entities;

namespace AnaPrevention.GeneralMasterData.Api.GeographicLocations.Infrastructure.Repositories
{
    public class CountryRepository : Repository<Country>
    {
        readonly int maxRowPageSize = CommonStatic.MaxRowPageSize;
        public CountryRepository(AnaPreventionContext context) : base(context)
        {
        }
        public CountryDto? GetDtoById(string id)
        {
            return GetDtoQueryable().SingleOrDefault(t1 => t1.Id == id);
        }
        public Country? GetbyDescription(string description)
        {
            return _context.Set<Country>().SingleOrDefault(x => x.Description == description);
        }

        public List<CountryDto> GetListAutoComplete(string descriptionSearch = "")
        {
            var query = GetDtoQueryable().Where(t1 => t1.Status);

            if (!string.IsNullOrEmpty(descriptionSearch))
                query = query.Where(t1 => EF.Functions.Like(t1.Description, "%" + descriptionSearch + "%"));

            return query.OrderBy(t1 => t1.Description).OrderBy(t1 => t1.Description).Take(CommonStatic.MaxRowAutocomplete).ToList();
        }

        public List<CountryDto> GetListAll()
        {
            return GetDtoQueryable().Where(t1 => t1.Status).OrderBy(t1 => t1.Description).OrderBy(t1 => t1.Description).ToList();
        }

        public Tuple<IEnumerable<CountryDto>, PaginationMetadata> GetList(int pageNumber, int pageSize, bool status = true, string descriptionSearch = "", string searchId = "")
        {
            if (pageSize > maxRowPageSize)
                pageSize = maxRowPageSize;

            var query = GetDtoQueryable().Where(t1 => t1.Status == status);

            if (!string.IsNullOrEmpty(descriptionSearch))
                query = query.Where(t1 => EF.Functions.Like(t1.Description, "%" + descriptionSearch + "%"));

            if (!string.IsNullOrEmpty(searchId))
                query = query.Where(t1 => t1.Id.Contains(searchId));

            var listCountryDto = query.OrderBy(t1 => t1.Description)
               .Skip(pageSize * (pageNumber - 1))
               .Take(pageSize).ToList();
            int totalItemCount = query.Count();

            var paginationMetadata = new PaginationMetadata(
              totalItemCount, pageSize, pageNumber);

            return new Tuple<IEnumerable<CountryDto>, PaginationMetadata>
                (listCountryDto, paginationMetadata);
        }

        private IQueryable<CountryDto> GetDtoQueryable()
        {
            return (from t1 in _context.Set<Country>()
                    orderby t1.Description
                    select new CountryDto()
                    {
                        Id = t1.Id,
                        Description = t1.Description,
                        SecondDescription = t1.SecondDescription,
                        SecondCode = t1.SecondCode,
                        PhoneCode = t1.PhoneCode,
                        Status = t1.Status,

                    });
        }
    }
}
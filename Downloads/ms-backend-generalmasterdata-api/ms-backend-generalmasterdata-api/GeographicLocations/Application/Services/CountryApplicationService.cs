using AnaPrevention.GeneralMasterData.Api.Common.API;
using AnaPrevention.GeneralMasterData.Api.Common.Infrastructure.EF;
using AnaPrevention.GeneralMasterData.Api.GeographicLocations.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.GeographicLocations.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.GeographicLocations.Infrastructure.Repositories;

namespace AnaPrevention.GeneralMasterData.Api.GeographicLocations.Application.Services
{
    public class CountryApplicationService
    {
        private readonly AnaPreventionContext _context;
        private readonly CountryRepository _countryRepository;

        public CountryApplicationService(
       AnaPreventionContext context,
       CountryRepository countryRepository)
        {
            _context = context;
            _countryRepository = countryRepository;
        }

        public Country? GetById(Guid id)
        {
            return _countryRepository.GetById(id);
        }

        public CountryDto? GetDtoById(string id)
        {
            return _countryRepository.GetDtoById(id);
        }
        public List<CountryDto> GetListAutoComplete(string descriptionSearch = "")
        {
            return _countryRepository.GetListAutoComplete(descriptionSearch);
        }

        public List<CountryDto> GetListAll()
        {
            return _countryRepository.GetListAll();
        }
        public Tuple<IEnumerable<CountryDto>, PaginationMetadata> GetList(int pageNumber, int pageSize, bool status, string descriptionSearch = "", string idSearch = "")
        {
            return _countryRepository.GetList(pageNumber, pageSize, status, descriptionSearch, idSearch);
        }
    }
}

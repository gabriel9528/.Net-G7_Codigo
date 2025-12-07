using AnaPrevention.GeneralMasterData.Api.Common.API;
using AnaPrevention.GeneralMasterData.Api.GeographicLocations.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.GeographicLocations.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.GeographicLocations.Infrastructure.Repositories;

namespace AnaPrevention.GeneralMasterData.Api.GeographicLocations.Application.Services
{
    public class DistrictApplicationService
    {
        private readonly DistrictRepository _districtRepository;

        public DistrictApplicationService(
       DistrictRepository districtRepository)
        {
            _districtRepository = districtRepository;
        }

        public District? GetById(Guid id)
        {
            return _districtRepository.GetById(id);
        }

        public DistrictDto? GetDtoById(string id)
        {
            return _districtRepository.GetDtoById(id);
        }
        public List<DistrictDto> getListAutoComplete(string descriptionSearch = "")
        {
            return _districtRepository.GetListAutoComplete(descriptionSearch);
        }

        public List<DistrictDto> getListAllByProvinceId(string provinceId = "")
        {
            return _districtRepository.GetListAllByProvinceId(provinceId);
        }

        public Tuple<IEnumerable<DistrictDto>, PaginationMetadata> GetList(int pageNumber, int pageSize, bool status, string descriptionSearch = "", string idSearch = "")
        {
            return _districtRepository.GetList(pageNumber, pageSize, status, descriptionSearch, idSearch);
        }
    }
}

using AnaPrevention.GeneralMasterData.Api.Common.API;
using AnaPrevention.GeneralMasterData.Api.GeographicLocations.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.GeographicLocations.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.GeographicLocations.Infrastructure.Repositories;

namespace AnaPrevention.GeneralMasterData.Api.GeographicLocations.Application.Services
{
    public class ProvinceApplicationService
    {
        private readonly ProvinceRepository _provinceRepository;

        public ProvinceApplicationService(
       ProvinceRepository provinceRepository)
        {
            _provinceRepository = provinceRepository;
        }

        public Province? GetById(Guid id)
        {
            return _provinceRepository.GetById(id);
        }

        public ProvinceDto? GetDtoById(string id)
        {
            return _provinceRepository.GetDtoById(id);
        }
        public List<ProvinceDto> getListAutoComplete(string descriptionSearch = "")
        {
            return _provinceRepository.GetListAutoComplete(descriptionSearch);
        }

        public List<ProvinceDto> getListAllByDepartmentId(string departmentId = "")
        {
            return _provinceRepository.GetListAllByDepartmentId(departmentId);
        }

        public Tuple<IEnumerable<ProvinceDto>, PaginationMetadata> GetList(int pageNumber, int pageSize, bool status, string descriptionSearch = "", string idSearch = "")
        {
            return _provinceRepository.GetList(pageNumber, pageSize, status, descriptionSearch, idSearch);
        }
    }
}

using AnaPrevention.GeneralMasterData.Api.Common.API;
using AnaPrevention.GeneralMasterData.Api.Common.Infrastructure.EF;
using AnaPrevention.GeneralMasterData.Api.GeographicLocations.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.GeographicLocations.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.GeographicLocations.Infrastructure.Repositories;

namespace AnaPrevention.GeneralMasterData.Api.GeographicLocations.Application.Services
{
    public class DepartmentApplicationService
    {
        private readonly DepartmentRepository _departmentRepository;

        public DepartmentApplicationService(
       DepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public Department? GetById(Guid id)
        {
            return _departmentRepository.GetById(id);
        }

        public DepartmentDto? GetDtoById(string id)
        {
            return _departmentRepository.GetDtoById(id);
        }
        public List<DepartmentDto> getListAutoComplete(string descriptionSearch = "")
        {
            return _departmentRepository.GetListAutoComplete(descriptionSearch);
        }

        public List<DepartmentDto> getListAllByCountryId(string countryId = "")
        {
            return _departmentRepository.GetListAllByCountryId(countryId);
        }

        public Tuple<IEnumerable<DepartmentDto>, PaginationMetadata> GetList(int pageNumber, int pageSize, bool status, string descriptionSearch = "", string idSearch = "")
        {
            return _departmentRepository.GetList(pageNumber, pageSize, status, descriptionSearch, idSearch);
        }
    }
}

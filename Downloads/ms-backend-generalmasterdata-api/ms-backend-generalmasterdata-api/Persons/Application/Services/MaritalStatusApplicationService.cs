using AnaPrevention.GeneralMasterData.Api.Persons.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Persons.Infrastructure.Repositories;

namespace AnaPrevention.GeneralMasterData.Api.Persons.Application.Services
{
    public class MaritalStatusApplicationService
    {
        private readonly MaritalStatusRepository _maritalStatusRepository;
        public MaritalStatusApplicationService(MaritalStatusRepository maritalStatusRepository)
        {
            _maritalStatusRepository = maritalStatusRepository;
        }

        public List<MaritalStatus> GetListAll()
        {
            return _maritalStatusRepository.GetListAll();
        }
    }
}

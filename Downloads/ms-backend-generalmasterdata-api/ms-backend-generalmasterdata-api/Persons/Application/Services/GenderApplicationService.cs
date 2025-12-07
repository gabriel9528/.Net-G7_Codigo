using AnaPrevention.GeneralMasterData.Api.Persons.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Persons.Infrastructure.Repositories;

namespace AnaPrevention.GeneralMasterData.Api.Persons.Application.Services
{
    public class GenderApplicationService
    {

        private readonly GenderRepository _genderRepository;

        public GenderApplicationService(GenderRepository genderRepository)
        {
            _genderRepository = genderRepository;
        }

        public List<Gender> GetListAll()
        {
            return _genderRepository.GetListAll();
        }
    }
}

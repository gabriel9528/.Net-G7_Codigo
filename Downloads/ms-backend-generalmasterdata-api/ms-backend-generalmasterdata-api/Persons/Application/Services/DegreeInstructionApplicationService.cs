using AnaPrevention.GeneralMasterData.Api.Persons.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Persons.Infrastructure.Repositories;

namespace AnaPrevention.GeneralMasterData.Api.Persons.Application.Services
{
    public class DegreeInstructionApplicationService
    {
        private readonly DegreeInstructionRepository _degreeInstructionRepository;
        public DegreeInstructionApplicationService(DegreeInstructionRepository degreeInstructionRepository)
        {
            _degreeInstructionRepository = degreeInstructionRepository;
        }

        public List<DegreeInstruction> GetListAll()
        {
            return _degreeInstructionRepository.GetListAll();
        }
    }
}

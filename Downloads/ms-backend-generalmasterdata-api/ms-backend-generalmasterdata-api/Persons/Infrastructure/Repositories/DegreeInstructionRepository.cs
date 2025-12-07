using AnaPrevention.GeneralMasterData.Api.Common.Infrastructure.EF;
using AnaPrevention.GeneralMasterData.Api.Persons.Domain.Entities;

namespace AnaPrevention.GeneralMasterData.Api.Persons.Infrastructure.Repositories
{
    public class DegreeInstructionRepository : Repository<DegreeInstruction>
    {
        public DegreeInstructionRepository(AnaPreventionContext context) : base(context)
        {
        }
        public List<DegreeInstruction> GetListAll()
        {
            return _context.Set<DegreeInstruction>().ToList();
        }
    }
}


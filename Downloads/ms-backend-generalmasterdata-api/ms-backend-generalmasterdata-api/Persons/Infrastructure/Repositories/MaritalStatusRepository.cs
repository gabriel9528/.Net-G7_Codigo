using AnaPrevention.GeneralMasterData.Api.Common.Infrastructure.EF;
using AnaPrevention.GeneralMasterData.Api.Persons.Domain.Entities;

namespace AnaPrevention.GeneralMasterData.Api.Persons.Infrastructure.Repositories
{
    public class MaritalStatusRepository : Repository<MaritalStatus>
    {
        public MaritalStatusRepository(AnaPreventionContext context) : base(context)
        {
        }
        public List<MaritalStatus> GetListAll()
        {
            return _context.Set<MaritalStatus>().ToList();
        }
    }
}

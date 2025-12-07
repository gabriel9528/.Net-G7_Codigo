using AnaPrevention.GeneralMasterData.Api.Common.Infrastructure.EF;
using AnaPrevention.GeneralMasterData.Api.Persons.Domain.Entities;

namespace AnaPrevention.GeneralMasterData.Api.Persons.Infrastructure.Repositories
{
    public class GenderRepository : Repository<Gender>
    {
        public GenderRepository(AnaPreventionContext context) : base(context)
        {
        }

        public List<Gender> GetListAll()
        {
            return _context.Set<Gender>().ToList();
        }
    }
}

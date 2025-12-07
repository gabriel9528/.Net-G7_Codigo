using AnaPrevention.GeneralMasterData.Api.Common.Infrastructure.EF;
using AnaPrevention.GeneralMasterData.Api.Lines.Domain.Entities;

namespace AnaPrevention.GeneralMasterData.Api.Lines.Infrastructure.Repositories
{
    public class LineTypeRepository(AnaPreventionContext context) : Repository<LineType>(context)
    {
        public List<LineType> GetListAll() => _context.Set<LineType>().OrderBy(t1 => t1.Code).ToList();
    }
}

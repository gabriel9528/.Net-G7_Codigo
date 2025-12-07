using AnaPrevention.GeneralMasterData.Api.Businesses.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Common.Infrastructure.EF;
using AnaPrevention.GeneralMasterData.Api.EconomicActivities.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.EconomicActivities.Domain.Entities;

namespace AnaPrevention.GeneralMasterData.Api.Businesses.Infrastructure.Repositories
{
    public class BusinessEconomicActivityRepository : Repository<BusinessEconomicActivity>
    {
        public BusinessEconomicActivityRepository(AnaPreventionContext context) : base(context)
        {

        }

        public BusinessEconomicActivity? GetbyEconomicActivityId(Guid economicActivityId, Guid businessId)
        {
            return _context.Set<BusinessEconomicActivity>().SingleOrDefault(t1 => t1.EconomicActivityId == economicActivityId && t1.BusinessId == businessId);
        }

        public List<EconomicActivityDto>? GetListEconomicActivityByBusinessId(Guid businessId)
        {
            return (from t1 in _context.Set<EconomicActivity>()
                    join t2 in _context.Set<BusinessEconomicActivity>() on t1.Id equals t2.EconomicActivityId
                    where t2.BusinessId == businessId
                    select new EconomicActivityDto()
                    {
                        Id = t1.Id,
                        Description = t1.Description,
                        Code = t1.Code,
                        Status = t1.Status,
                    }
                     ).ToList();

        }
    }
}

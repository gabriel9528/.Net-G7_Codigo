using AnaPrevention.GeneralMasterData.Api.EconomicActivities.Domain.Entities;

namespace AnaPrevention.GeneralMasterData.Api.Businesses.Domain.Entities
{
    public class BusinessEconomicActivity
    {
        public Guid Id { get; set; }
        public Guid EconomicActivityId { get; set; }
        public EconomicActivity EconomicActivity { get; set; }
        public Guid BusinessId { get; set; }
        public Business Business { get; set; }

        public BusinessEconomicActivity()
        {

        }
        public BusinessEconomicActivity(Guid economicActivityId, Guid businessId, Guid id)
        {
            EconomicActivityId = economicActivityId;
            BusinessId = businessId;
            Id = id;
        }
    }
}

using AnaPrevention.GeneralMasterData.Api.Businesses.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Common.Domain.ValueObjects;

namespace AnaPrevention.GeneralMasterData.Api.BusinessCampaigns.Domain.Entities
{
    public class BusinessCampaign
    {
        public Guid Id { get; set; }
        public Guid BusinessId { get; set; }
        public Business? Business { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
        public Date DateStart { get; set; }
        public Date DateFinish { get; set; }
        public BusinessCampaign() { }

        public BusinessCampaign(string description, Guid businessId, Date dateStart, Date dateFinish, Guid id)
        {
            Description = description;
            BusinessId = businessId;
            DateStart = dateStart;
            DateFinish = dateFinish;
            Status = true;
            Id = id;
        }
    }
}

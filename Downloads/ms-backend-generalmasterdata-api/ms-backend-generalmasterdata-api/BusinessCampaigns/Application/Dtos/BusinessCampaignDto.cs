using AnaPrevention.GeneralMasterData.Api.Common.Domain.ValueObjects;

namespace AnaPrevention.GeneralMasterData.Api.BusinessCampaigns.Application.Dtos
{
    public class BusinessCampaignDto
    {
        public Guid Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public Guid BusinessId { get; set; }
        public bool Status { get; set; }
        public Date DateStart { get; set; }
        public Date DateFinish { get; set; }
    }
}

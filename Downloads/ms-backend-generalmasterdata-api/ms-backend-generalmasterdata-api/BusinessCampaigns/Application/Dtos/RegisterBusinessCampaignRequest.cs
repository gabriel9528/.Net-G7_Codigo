using AnaPrevention.GeneralMasterData.Api.Common.Domain.ValueObjects;

namespace AnaPrevention.GeneralMasterData.Api.BusinessCampaigns.Application.Dtos
{
    public class RegisterBusinessCampaignRequest
    {
        public string Description { get; set; } = string.Empty;
        public Guid BusinessId { get; set; }
        public string DateStart { get; set; } = string.Empty;
        public string DateFinish { get; set; } = string.Empty;
    }
}

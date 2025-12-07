namespace AnaPrevention.GeneralMasterData.Api.BusinessCampaigns.Application.Dtos
{
    public class RegisterBusinessCampaignResponse
    {
        public Guid Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public Guid BusinessId { get; set; }
        public bool Status { get; set; }
        public string DateStart { get; set; } = string.Empty;
        public string DateFinish { get; set; } = string.Empty;
    }
}

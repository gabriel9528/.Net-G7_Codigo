namespace AnaPrevention.GeneralMasterData.Api.BusinessCampaigns.Application.Dtos
{
    public class EditBusinessCampaignRequest
    {
        public Guid Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public string DateStart { get; set; } = string.Empty;
        public string DateFinish { get; set; } = string.Empty;
    }
}

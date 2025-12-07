namespace AnaPrevention.GeneralMasterData.Api.BusinessProfiles.Application.Dtos
{
    public class EditBusinessProfileResponse
    {
        public Guid Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public Guid BusinessId { get; set; }
        public bool Status { get; set; }
    }
}

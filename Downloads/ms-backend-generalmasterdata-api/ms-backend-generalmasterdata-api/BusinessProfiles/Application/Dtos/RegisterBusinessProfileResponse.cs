namespace AnaPrevention.GeneralMasterData.Api.BusinessProfiles.Application.Dtos
{
    public class RegisterBusinessProfileResponse
    {
        public Guid Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public Guid BusinessId { get; set; }
        public bool Status { get; set; }
    }
}

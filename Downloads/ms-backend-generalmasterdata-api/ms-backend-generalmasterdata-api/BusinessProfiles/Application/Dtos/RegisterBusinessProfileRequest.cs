namespace AnaPrevention.GeneralMasterData.Api.BusinessProfiles.Application.Dtos
{
    public class RegisterBusinessProfileRequest
    {
        public string Description { get; set; } = string.Empty;
        public Guid BusinessId { get; set; }

    }
}

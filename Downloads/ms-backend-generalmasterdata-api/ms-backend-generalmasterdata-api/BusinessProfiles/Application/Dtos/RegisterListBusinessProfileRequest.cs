namespace AnaPrevention.GeneralMasterData.Api.BusinessProfiles.Application.Dtos
{
    public class RegisterListBusinessProfileRequest
    {
        public List<string> ListDescription { get; set; } = new List<string>();
        public Guid BusinessId { get; set; }
    }
}

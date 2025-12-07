namespace AnaPrevention.GeneralMasterData.Api.BusinessProfiles.Application.Dtos
{
    public class RegisterListBusinessProfileResponse
    {
        public List<string> ListDescription { get; set; } = new List<string>();
        public Guid BusinessId { get; set; }
    }
}

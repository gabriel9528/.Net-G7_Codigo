namespace AnaPrevention.GeneralMasterData.Api.BusinessProfiles.Application.Dtos
{
    public class EditBusinessProfileRequest
    {
        public Guid Id { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}

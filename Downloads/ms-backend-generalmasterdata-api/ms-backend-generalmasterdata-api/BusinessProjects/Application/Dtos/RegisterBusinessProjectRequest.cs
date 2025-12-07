namespace AnaPrevention.GeneralMasterData.Api.BusinessProjects.Application.Dtos
{
    public class RegisterBusinessProjectRequest
    {
        public string Description { get; set; } = string.Empty;
        public Guid BusinessId { get; set; }

    }
}

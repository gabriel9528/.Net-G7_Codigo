namespace AnaPrevention.GeneralMasterData.Api.BusinessProjects.Application.Dtos
{
    public class RegisterBusinessProjectResponse
    {
        public Guid Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public Guid BusinessId { get; set; }
        public bool Status { get; set; }
    }
}

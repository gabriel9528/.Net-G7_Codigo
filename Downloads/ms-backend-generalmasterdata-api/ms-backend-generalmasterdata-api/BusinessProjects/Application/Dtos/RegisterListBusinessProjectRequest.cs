namespace AnaPrevention.GeneralMasterData.Api.BusinessProjects.Application.Dtos
{
    public class RegisterListBusinessProjectRequest
    {
        public List<string> ListDescription { get; set; } = new List<string>();
        public Guid BusinessId { get; set; }
    }
}

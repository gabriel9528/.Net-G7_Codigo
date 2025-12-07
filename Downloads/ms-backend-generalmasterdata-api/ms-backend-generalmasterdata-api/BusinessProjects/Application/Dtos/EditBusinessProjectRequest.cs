namespace AnaPrevention.GeneralMasterData.Api.BusinessProjects.Application.Dtos
{
    public class EditBusinessProjectRequest
    {
        public Guid Id { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}

namespace AnaPrevention.GeneralMasterData.Api.BusinessAreas.Application.Dtos
{
    public class EditBusinessAreaRequest
    {
        public Guid Id { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}

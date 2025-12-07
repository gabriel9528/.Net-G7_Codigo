namespace AnaPrevention.GeneralMasterData.Api.BusinessAreas.Application.Dtos
{
    public class EditBusinessAreaResponse
    {
        public Guid Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public Guid BusinessId { get; set; }
        public bool Status { get; set; }
    }
}

namespace AnaPrevention.GeneralMasterData.Api.BusinessAreas.Application.Dtos
{
    public class RegisterBusinessAreaRequest
    {
        public string Description { get; set; } = string.Empty;
        public Guid BusinessId { get; set; }

    }
}

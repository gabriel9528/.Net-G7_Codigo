namespace AnaPrevention.GeneralMasterData.Api.BusinessPositions.Application.Dtos
{
    public class EditBusinessPositionRequest
    {
        public Guid Id { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}

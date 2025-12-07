namespace AnaPrevention.GeneralMasterData.Api.BusinessPositions.Application.Dtos
{
    public class RegisterBusinessPositionRequest
    {
        public string Description { get; set; } = string.Empty;
        public Guid BusinessAreaId { get; set; }

    }
}

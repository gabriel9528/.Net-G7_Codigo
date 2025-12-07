namespace AnaPrevention.GeneralMasterData.Api.BusinessPositions.Application.Dtos
{
    public class RegisterListBusinessPositionRequest
    {
        public List<string> ListDescription { get; set; } = new List<string>();
        public Guid BusinessAreaId { get; set; }
    }
}

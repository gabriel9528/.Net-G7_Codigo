namespace AnaPrevention.GeneralMasterData.Api.BusinessPositions.Application.Dtos
{
    public class RegisterListBusinessPositionResponse
    {
        public List<string> ListDescription { get; set; } = new List<string>();
        public Guid BusinessAreaId { get; set; }
    }
}

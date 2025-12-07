namespace AnaPrevention.GeneralMasterData.Api.BusinessPositions.Application.Dtos
{
    public class BusinessPositionDto
    {
        public Guid Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public Guid BusinessAreaId { get; set; }
        public bool Status { get; set; }
    }
}

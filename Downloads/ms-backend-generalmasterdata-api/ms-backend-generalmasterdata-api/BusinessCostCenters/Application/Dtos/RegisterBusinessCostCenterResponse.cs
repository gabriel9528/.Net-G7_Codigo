namespace AnaPrevention.GeneralMasterData.Api.BusinessCostCenters.Application.Dtos
{
    public class RegisterBusinessCostCenterResponse
    {
        public Guid Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public Guid BusinessId { get; set; }
        public bool Status { get; set; }
    }
}

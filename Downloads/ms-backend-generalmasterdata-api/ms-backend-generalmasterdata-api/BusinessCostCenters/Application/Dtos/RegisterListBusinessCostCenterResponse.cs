namespace AnaPrevention.GeneralMasterData.Api.BusinessCostCenters.Application.Dtos
{
    public class RegisterListBusinessCostCenterResponse
    {
        public List<string> ListDescription { get; set; } = new List<string>();
        public Guid BusinessId { get; set; }
    }
}

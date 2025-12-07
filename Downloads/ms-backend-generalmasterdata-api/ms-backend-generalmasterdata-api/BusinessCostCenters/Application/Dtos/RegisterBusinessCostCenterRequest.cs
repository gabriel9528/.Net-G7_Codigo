namespace AnaPrevention.GeneralMasterData.Api.BusinessCostCenters.Application.Dtos
{
    public class RegisterBusinessCostCenterRequest
    {
        public string Description { get; set; } = string.Empty;
        public Guid BusinessId { get; set; }

    }
}

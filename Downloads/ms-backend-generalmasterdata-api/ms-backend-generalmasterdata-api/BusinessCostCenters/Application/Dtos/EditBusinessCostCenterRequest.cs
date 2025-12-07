namespace AnaPrevention.GeneralMasterData.Api.BusinessCostCenters.Application.Dtos
{
    public class EditBusinessCostCenterRequest
    {
        public Guid Id { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}

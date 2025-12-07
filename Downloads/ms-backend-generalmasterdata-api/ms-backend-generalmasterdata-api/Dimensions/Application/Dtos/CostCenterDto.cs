namespace AnaPrevention.GeneralMasterData.Api.Dimensions.Application.Dtos
{
    public class CostCenterDto
    {
        public Guid Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool Status { get; set; }
    }
}

namespace AnaPrevention.GeneralMasterData.Api.EconomicActivities.Application.Dtos
{
    public class RegisterEconomicActivityResponse
	{
        public Guid Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public bool Status { get; set; }
    }
}

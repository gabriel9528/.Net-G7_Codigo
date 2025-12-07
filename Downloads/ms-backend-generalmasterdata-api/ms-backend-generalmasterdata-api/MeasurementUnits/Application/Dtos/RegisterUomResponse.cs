namespace AnaPrevention.GeneralMasterData.Api.MeasurementUnits.Application.Dtos
{
    public class RegisterUomResponse
	{
        public Guid Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string FiscalCode { get; set; } = string.Empty;
        public bool Status { get; set; }
    }
}

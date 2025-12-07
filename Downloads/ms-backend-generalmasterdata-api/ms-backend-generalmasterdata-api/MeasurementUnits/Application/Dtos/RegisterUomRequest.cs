namespace AnaPrevention.GeneralMasterData.Api.MeasurementUnits.Application.Dtos
{
    public class RegisterUomRequest
	{
        public string Description { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string FiscalCode { get; set; } = string.Empty;
        public string Abbreviation { get; set; } = string.Empty;

    }
}

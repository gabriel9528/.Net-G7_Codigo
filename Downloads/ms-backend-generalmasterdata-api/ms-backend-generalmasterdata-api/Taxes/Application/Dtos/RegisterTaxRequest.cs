namespace AnaPrevention.GeneralMasterData.Api.Taxes.Application.Dtos
{
    public class RegisterTaxRequest
    {
        public string Description { get; set; } = string.Empty;
        public decimal Rate { get; set; }
        public string Code { get; set; } = string.Empty;
    }
}

namespace AnaPrevention.GeneralMasterData.Api.Taxes.Application.Dtos
{
    public class TaxDto
    {
        public Guid Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public decimal Rate { get; set; }
        public string Code { get; set; } = string.Empty;
        public bool Status { get; set; }
    }
}

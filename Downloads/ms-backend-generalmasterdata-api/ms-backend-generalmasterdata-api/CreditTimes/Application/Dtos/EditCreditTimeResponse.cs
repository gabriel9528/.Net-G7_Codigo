namespace AnaPrevention.GeneralMasterData.Api.CreditTimes.Application.Dtos
{
    public class EditCreditTimeResponse
    {
        public Guid Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public int NumberDay { get; set; }
        public bool Status { get; set; }
    }
}

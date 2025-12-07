namespace AnaPrevention.GeneralMasterData.Api.CreditTimes.Application.Dtos
{
    public class EditCreditTimeRequest
	{
        public Guid Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public int NumberDay { get; set; }
    }
}

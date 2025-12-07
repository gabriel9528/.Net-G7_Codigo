namespace AnaPrevention.GeneralMasterData.Api.WorkingConditions.Application.Dtos
{
    public class RegisterWorkingConditionResponse
	{
        public Guid Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public int Code { get; set; }
        public bool Status { get; set; }
    }
}

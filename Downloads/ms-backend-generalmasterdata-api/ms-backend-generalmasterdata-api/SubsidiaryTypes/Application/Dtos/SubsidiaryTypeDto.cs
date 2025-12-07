namespace AnaPrevention.GeneralMasterData.Api.SubsidiaryTypes.Application.Validators
{
    public class SubsidiaryTypeDto
    {

        public Guid Id { get; set; }
        public Guid CompanyId { get; set; }
        public string Code { get; set; } = String.Empty;
        public string Description { get; set; } = String.Empty;
        public bool Status { get; set; }
    }
}

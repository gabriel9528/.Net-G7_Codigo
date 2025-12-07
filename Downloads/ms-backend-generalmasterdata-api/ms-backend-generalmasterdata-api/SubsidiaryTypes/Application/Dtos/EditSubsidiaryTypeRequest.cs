namespace AnaPrevention.GeneralMasterData.Api.SubsidiaryTypes.Infrastructure.Repositories
{
    public class EditSubsidiaryTypeRequest
    {
        public Guid Id { get; set; }

        public string Code { get; set; } = String.Empty;
        public string Description { get; set; } = String.Empty;
        public bool Status { get; set; }
    }
}

namespace AnaPrevention.GeneralMasterData.Api.SubFamilies.Application.Dtos
{
    public class EditSubFamilyResponse
    {
        public Guid Id { get; set; }

        public string Code { get; set; } = String.Empty;
        public string Description { get; set; } = String.Empty;
        public bool Status { get; set; }
        public Guid CompanyId { get; set; }

        public Guid FamilyId { get; set; }
    }
}

namespace AnaPrevention.GeneralMasterData.Api.Families.Application.Dtos
{
    public class RegisterFamilyResponse
    {
        public Guid Id { get; set; }

        public string Code { get; set; } = String.Empty;
        public string Description { get; set; } = String.Empty;
        public bool Status { get; set; }
        public Guid CompanyId { get; set; }

        public Guid LineId { get; set; }
    }
}

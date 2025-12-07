namespace AnaPrevention.GeneralMasterData.Api.Families.Application.Dtos
{
    public class FamilyDto
    {
        public Guid Id { get; set; }
        public Guid CompanyId { get; set; }
        public string Code { get; set; } = String.Empty;
        public string Description { get; set; } = String.Empty;
        public bool Status { get; set; }
        public Guid LineId { get; set; }
        public string LineDescripcion   { get; set; } = String.Empty;
    }
}

namespace AnaPrevention.GeneralMasterData.Api.Lines.Application.Dtos
{
    public class RegisterLineResponse
    {
        public Guid Id { get; set; }
        public string Code { get; set; } = String.Empty;
        public string Description { get; set; } = String.Empty;
        public bool Status { get; set; }
        public Guid CompanyId { get; set; }
        public Guid LineTypeId { get; set; }
    }
}

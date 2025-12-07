namespace AnaPrevention.GeneralMasterData.Api.Diagnostics.Domain.Dtos
{
    public class EditDiagnosticResponse
    {
        public Guid Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Cie10 { get; set; } = string.Empty;
        public string? DiagnosticOptional { get; set; } = string.Empty;
        public string? Description2 { get; set; }
        public bool Status { get; set; }
    }
}

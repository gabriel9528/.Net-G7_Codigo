namespace AnaPrevention.GeneralMasterData.Api.Diagnostics.Domain.Entities
{
    public class Diagnostic
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public string Cie10 { get; set; }
        public string? Description2 { get; set; }
        public string? DiagnosticOptional { get; set; }
        public bool Status { get; set; }

        public Diagnostic() { }

        public Diagnostic(string description, string cie10, string? description2, string? diagnosticOptional, Guid id)
        {
            Description = description;
            Cie10 = cie10;
            Description2 = description2;
            DiagnosticOptional = diagnosticOptional;
            Status = true;
            Id= id;
        }
    }
}

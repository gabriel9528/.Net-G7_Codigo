using AnaPrevention.GeneralMasterData.Api.MedicalForms.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.ServiceCatalogs.Domain.Enums;

namespace AnaPrevention.GeneralMasterData.Api.Diagnostics.Domain.Dtos
{
    public class DiagnosticDto
    {
        public Guid Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Cie10 { get; set; } = string.Empty;
        public Guid OccupationalHealthId { get; set; }
        public string Recommendations { get; set; } = string.Empty;
        public Guid DiagnosticId { get; set; }
        public DiagnosticForm DiagnosticFor { get; set; }
        public MedicalFormsType MedicalFormsType { get; set; }
        public bool Status { get; set; }
    }
}

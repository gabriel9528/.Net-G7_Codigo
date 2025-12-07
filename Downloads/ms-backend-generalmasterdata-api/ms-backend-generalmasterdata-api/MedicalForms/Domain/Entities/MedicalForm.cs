using AnaPrevention.GeneralMasterData.Api.MedicalAreas.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.MedicalForms.Domain.Entities;

using AnaPrevention.GeneralMasterData.Api.ServiceCatalogs.Domain.Enums;
using AnaPrevention.GeneralMasterData.Api.ServiceTypes.Domain.Entities;

namespace AnaPrevention.GeneralMasterData.Api.ServiceCatalogs.Domain.Entities
{
    public class MedicalForm
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public ServiceType ServiceType { get; set; }
        public Guid ServiceTypeId { get; set; }
        public MedicalArea MedicalArea { get; set; }
        public Guid MedicalAreaId { get; set; }
        public MedicalFormsType MedicalFormsType { get; set; }
        public DiagnosticForm DiagnosticForm { get; set; } = DiagnosticForm.WITH_CIE10;
        public string? DescriptionInDiagnostic { get; set; } = String.Empty;
        public string? OptionDiagnosticJson { get; set; } = String.Empty;
        public bool ShowInDiagnostic { get; set; }
        public bool Status { get; set; }
        public string? IconDescription { get; set; } = String.Empty;
        public string? Icon { get; set; } = String.Empty;
        public int OrderRowAttention { get; set; }
        public string? Abbreviation { get; set; }
        public int OrderRowDiagnostic { get; set; }
        public bool IsMedicalExam { get; set; }

        public MedicalForm() { }

        public MedicalForm(string description, Guid serviceTypeId, Guid medicalAreaId, MedicalFormsType medicalFormsType, string code, Guid id, DiagnosticForm diagnosticForm = DiagnosticForm.WITH_CIE10, string? descriptionInDiagnostic = null, string? ooptionDiagnosticJson = null, bool showInDiagnostic = true, string? iconDescription = null, string? icon = null, int orderRowAttention = 0, string? abbreviation = null, int orderRowDiagnostic = 0, bool isMedicalExam = true)

        {
            Description = description;
            Status = true;
            ServiceTypeId = serviceTypeId;
            MedicalAreaId = medicalAreaId;
            MedicalFormsType = medicalFormsType;
            Code = code;
            Id = id;
            DiagnosticForm = diagnosticForm;
            DescriptionInDiagnostic = descriptionInDiagnostic;
            OptionDiagnosticJson = ooptionDiagnosticJson;
            ShowInDiagnostic = showInDiagnostic;
            IconDescription = iconDescription;
            Icon = icon;
            OrderRowAttention = orderRowAttention;
            Abbreviation = abbreviation;
            OrderRowDiagnostic = orderRowDiagnostic;
            IsMedicalExam = isMedicalExam;
        }
    }
}

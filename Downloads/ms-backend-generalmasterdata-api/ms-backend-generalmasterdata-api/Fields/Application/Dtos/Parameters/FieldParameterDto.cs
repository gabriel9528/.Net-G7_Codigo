using AnaPrevention.GeneralMasterData.Api.Fields.Domain.Enums;
using AnaPrevention.GeneralMasterData.Api.ServiceCatalogs.Domain.Enums;

namespace AnaPrevention.GeneralMasterData.Api.Fields.Application.Dtos
{
    public class FieldParameterDto
    {
        public Guid? Id { get; set; }
        public Guid FieldId { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string DefaultValue { get; set; } = string.Empty;
        public string Legend { get; set; } = string.Empty;
        public List<RegisterRangeHeaderRequest>? Range { get; set; }
        public string Uom { get; set; } = string.Empty;
        public FieldType FieldType { get; set; }
        public bool IsMandatory { get; set; }
        public bool Show { get; set; }
        public Guid? MedicalFormId { get; set; }
        public string MedicalForm { get; set; } = string.Empty;
        public Guid? GenderId { get; set; }
        public string? Gender { get; set; } = string.Empty;
        public bool Status { get; set; }
        public MedicalFormsType MedicalFormsType { get; set; }
        public MedicalFormSubType MedicalFormSubType { get; set; }
    }
}

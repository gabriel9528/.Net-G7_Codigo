using AnaPrevention.GeneralMasterData.Api.Fields.Domain.Enums;
using AnaPrevention.GeneralMasterData.Api.ServiceCatalogs.Domain.Enums;

namespace AnaPrevention.GeneralMasterData.Api.Fields.Application.Dtos
{
    public class FieldDto
    {
        public Guid Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string? Legend { get; set; } = string.Empty;
        public string Uom { get; set; } = string.Empty;
        public FieldType FieldType { get; set; }
        public string MedicalForm { get; set; } = string.Empty;
        public Guid? MedicalFormId { get; set; }
        public MedicalFormsType MedicalFormsType { get; set; }
        public bool Status { get; set; }

    }
}

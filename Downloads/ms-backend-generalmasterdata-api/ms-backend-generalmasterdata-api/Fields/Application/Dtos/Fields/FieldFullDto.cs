using AnaPrevention.GeneralMasterData.Api.Fields.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.Fields.Domain.Enums;

namespace AnaPrevention.GeneralMasterData.Api.Fields.Application.Dtos
{
    public class FieldFullDto
    {
        public Guid Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Legend { get; set; } = string.Empty;
        public string Uom { get; set; } = string.Empty;
        public FieldType FieldType { get; set; }
        public Guid MedicalFormId { get; set; }
        public string MedicalForm { get; set; } = string.Empty;
        public List<FieldParameterMinDto> ListFieldParameters { get; set; } = new List<FieldParameterMinDto>();
        public bool Status { get; set; }
    }
}

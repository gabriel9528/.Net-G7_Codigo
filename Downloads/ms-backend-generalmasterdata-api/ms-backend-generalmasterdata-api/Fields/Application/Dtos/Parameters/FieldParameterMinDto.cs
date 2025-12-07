using AnaPrevention.GeneralMasterData.Api.Fields.Domain.Enums;

namespace AnaPrevention.GeneralMasterData.Api.Fields.Application.Dtos
{
    public class FieldParameterMinDto
    {
        public Guid Id { get; set; }
        public string DefaultValue { get; set; } = string.Empty;
        public string Legend { get; set; } = string.Empty;
        public List<RegisterRangeHeaderRequest>? Range { get; set; }
        public string Uom { get; set; } = string.Empty;
        public bool IsMandatory { get; set; }
        public bool Show { get; set; }
        public Guid? GenderId { get; set; }
        public string? Gender { get; set; } = string.Empty;
        public bool Status { get; set; }
    }
}

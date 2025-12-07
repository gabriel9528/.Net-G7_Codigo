
namespace AnaPrevention.GeneralMasterData.Api.Fields.Application.Dtos
{
    public class EditFieldParameterResponse
    {
        public Guid Id { get; set; }
        public string DefaultValue { get; set; } = string.Empty;
        public string? Legend { get; set; } = string.Empty;
        public string? Uom { get; set; } = string.Empty;
        public bool IsMandatory { get; set; }
        public bool Show { get; set; }
        public Guid FieldId { get; set; }
        public Guid? GenderId { get; set; }
        public Guid FielParameterHeaderId { get; set; }
        public bool Status { get; set; }
    }
}

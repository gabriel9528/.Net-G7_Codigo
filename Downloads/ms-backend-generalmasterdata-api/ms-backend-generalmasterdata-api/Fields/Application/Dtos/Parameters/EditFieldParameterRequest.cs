
namespace AnaPrevention.GeneralMasterData.Api.Fields.Application.Dtos
{
    public class EditFieldParameterRequest
    {
        public Guid Id { get; set; }
        public string DefaultValue { get; set; } = string.Empty;
        public string Legend { get; set; } = string.Empty;
        public List<RegisterRangeHeaderRequest>? Range { get; set; }
        public string Uom { get; set; } = string.Empty;
        public bool IsMandatory { get; set; }
        public Dictionary<long, string>? Options { get; set; }
        public bool Show { get; set; }
        public Guid? GenderId { get; set; }
        public Guid FielParameterHeaderId { get; set; }
    }
}

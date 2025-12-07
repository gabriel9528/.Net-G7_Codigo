using AnaPrevention.GeneralMasterData.Api.Fields.Domain.Enums;

namespace AnaPrevention.GeneralMasterData.Api.Fields.Application.Dtos
{
    public class RegisterFieldRequest
    {
        public string? SecondCode { get; set; } = string.Empty;
        public string Description { get; set; } = String.Empty;
        public FieldType FieldType { get; set; }
        public string Uom { get; set; } = String.Empty;
        public string Legend { get; set; } = String.Empty;
        public int OrderRow { get; set; }
        public List<OptionFieldDto>? Options { get; set; }
        public List<Guid> ListServiceCatalogIds { get; set; } = new List<Guid>();
        public List<string> ReferenceValues { get; set; } = new List<string>();
        public FieldLabelType IsTittle { get; set; }
    }
}

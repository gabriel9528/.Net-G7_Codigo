using AnaPrevention.GeneralMasterData.Api.Fields.Domain.Enums;

namespace AnaPrevention.GeneralMasterData.Api.Fields.Application.Dtos
{
    public class EditFieldRequest
    {
        public Guid Id { get; set; }
        public string? SecondCode { get; set; } = string.Empty;
        public string Description { get; set; } = String.Empty;
        public string Uom { get; set; } = String.Empty;
        public string Legend { get; set; } = String.Empty;
        public FieldType FieldType { get; set; }
        public int OrderRow { get; set; }
        public List<OptionFieldDto> Options { get; set; } = new List<OptionFieldDto>();
        public List<Guid> ListServiceCatalogIds { get; set; } = new List<Guid>();
        public List<string> ReferenceValues { get; set; } = new List<string>();
        public FieldLabelType IsTittle { get; set; }
    }
}

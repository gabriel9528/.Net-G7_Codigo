using AnaPrevention.GeneralMasterData.Api.Fields.Domain.Enums;
using AnaPrevention.GeneralMasterData.Api.Lines.Domain.Enums;
using AnaPrevention.GeneralMasterData.Api.ServiceCatalogs.Application.Dtos;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnaPrevention.GeneralMasterData.Api.Fields.Application.Dtos.Fields
{
    public class FieldLaboratoryDto
    {
        public Guid Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string? SecondCode { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string? Legend { get; set; } = string.Empty;
        public string Uom { get; set; } = string.Empty;
        public FieldType FieldType { get; set; }
        public string FieldTypeDescription { get; set; } = string.Empty;
        public CodeLineType FieldExamenType { get; set; }
        public bool Status { get; set; }
        public int OrderRow { get; set; }
        public FieldLabelType IsTittle { get; set; }
        public Guid? ServiceCatalogId { get; set; }
        public List<OptionFieldDto>? Options { get; set; }
        public List<ServiceCatalogMinDto>? ListServiceCatalog { get; set; }
        [NotMapped]
        public List<string>? ReferenceValues { get; set; } = new List<string>();
    }
}

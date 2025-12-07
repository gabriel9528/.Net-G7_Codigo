using System.ComponentModel;
using AnaPrevention.GeneralMasterData.Api.Lines.Domain.Enums;
using AnaPrevention.GeneralMasterData.Api.SubFamilies.Domain.Enums;

namespace AnaPrevention.GeneralMasterData.Api.ServiceCatalogs.Application.Dtos
{
    public class ServiceCatalogLaboratoryDto
    {
        public Guid Id { get; set; }

        [Description("Descripcion")]
        public string Description { get; set; } = string.Empty;
        [Description("Codigo")]
        public string Code { get; set; } = string.Empty;
        public Guid SubFamilyId { get; set; }
        [Description("Subfamilia")]
        public string SubFamily { get; set; } = string.Empty;
        public Guid FamilyId { get; set; }
        [Description("Familia")]
        public string Family { get; set; } = string.Empty;
        [Description("Linea")]
        public string Line { get; set; } = string.Empty;
        public Guid LineId { get; set; }
        public CodeLineType CodeLineType { get; set; }
        public Guid TaxId { get; set; }
        public bool Status { get; set; }
        [Description("Tipo de Subfamilia")]
        public SubFamilyType SubFamilyType { get; set; }
       // [Description("Resultado")]
        //public List<LaboratoryResultDto>? Result { get; set; } = new();
    }
}

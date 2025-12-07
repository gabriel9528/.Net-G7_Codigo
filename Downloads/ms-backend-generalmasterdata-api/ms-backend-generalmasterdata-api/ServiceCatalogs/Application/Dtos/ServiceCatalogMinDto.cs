using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Lines.Domain.Enums;

namespace AnaPrevention.GeneralMasterData.Api.ServiceCatalogs.Application.Dtos
{
    public class ServiceCatalogMinDto
    {
        public Guid Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public Guid SubFamilyId { get; set; }
        public string SubFamily { get; set; } = string.Empty;
        public Guid FamilyId { get; set; }
        public string Family { get; set; } = string.Empty;
        public string Line { get; set; } = string.Empty;
        public Guid LineId { get; set; }
        public CodeLineType CodeLineType { get; set; }
        public Guid TaxId { get; set; }
        public string Tax { get; set; } = string.Empty;
        public decimal TaxRate { get; set; }
        public bool Status { get; set; }
        public int OrderRowLine { get; set; } = CommonStatic.DefaultOrderRow;
        public int OrderRowFamily { get; set; } = CommonStatic.DefaultOrderRow;
        public int OrderRowSubFamily { get; set; } = CommonStatic.DefaultOrderRow;
        public int OrderRowServiceCatalog { get; set; } = CommonStatic.DefaultOrderRow;
        public int orderRowTourSheetServiceCatalog { get; set; } = CommonStatic.DefaultOrderRow;
    }
}

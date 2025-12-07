using System.ComponentModel;
using AnaPrevention.GeneralMasterData.Api.ServiceCatalogs.Application.Dtos;

namespace AnaPrevention.GeneralMasterData.Api.SubFamilies.Application.Dtos
{
    public class SubFamilyLaboratoryDto
    {
        public Guid Id { get; set; }
        public Guid CompanyId { get; set; }

        [Description("Codigo")]
        public string Code { get; set; } = String.Empty;
        [Description("Descripcion")]
        public string Description { get; set; } = String.Empty;

        public bool Status { get; set; }

        public Guid FamilyId { get; set; }
        [Description("Familia Descripcion")]
        public string FamilyDescription { get; set; } = String.Empty;
        
        //[Description("Catalogo de Servicios")]
      //  public List<ServiceCatalogLaboratoryDto>? ServiceCatalogs { get; set; } = new();
    }
}

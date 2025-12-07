using AnaPrevention.GeneralMasterData.Api.Lines.Domain.Enums;
using AnaPrevention.GeneralMasterData.Api.ServiceCatalogs.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.ServiceTypes.Domain.Entities;

namespace AnaPrevention.GeneralMasterData.Api.ServiceCatalogs.Application.Dtos
{
    public class ServiceCatalogDto
    {
        public Guid Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string CodeSecond { get; set; } = string.Empty;
        public Guid SubFamilyId { get; set; }
        public string SubFamily { get; set; } = string.Empty;
        public Guid FamilyId { get; set; }
        public string Family { get; set; } = string.Empty;
        public Guid LineId { get; set; }
        public string Line { get; set; } = string.Empty;
        public string LineType { get; set; } = string.Empty;
        public Guid LineTypeId { get; set; }
        public CodeLineType CodeLineType { get; set; }
        public Guid UomId { get; set; }
        public string Uom { get; set; } = string.Empty;
        public Guid UomSecondId { get; set; }
        public string UomAbbreviation { get; set; } = string.Empty;
        public string UomSecond { get; set; } = string.Empty;
        public string UomSecondAbbreviation { get; set; } = string.Empty;
        public Guid ExistenceTypeId { get; set; }
        public string ExistenceType { get; set; } = string.Empty;
        public Guid TaxId { get; set; }
        public string Tax { get; set; } = string.Empty;
        public decimal TaxRate { get; set; }
        public bool IsActive { get; set; }
        public bool IsSales { get; set; }
        public bool IsBuy { get; set; }
        public bool IsInventory { get; set; }
        public bool IsRetention { get; set; }
        public string Comment { get; set; } = string.Empty;
        public bool Status { get; set; }
        public Guid? MedicalAreaId { get; set; }

        public string MedicalArea { get; set; } = string.Empty;
        public List<ServiceType>? ListServiceTypes { get; set; }
        public List<MedicalForm>? ListMedicalForms { get; set; }
    }
}

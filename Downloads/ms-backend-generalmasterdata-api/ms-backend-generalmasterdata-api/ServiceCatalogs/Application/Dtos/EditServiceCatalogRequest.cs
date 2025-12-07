namespace AnaPrevention.GeneralMasterData.Api.ServiceCatalogs.Application.Dtos
{
    public class EditServiceCatalogRequest
    {
        public Guid Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string CodeSecond { get; set; } = string.Empty;
        public Guid SubFamilyId { get; set; }
        public Guid UomId { get; set; }
        public Guid UomSecondId { get; set; }
        public Guid ExistenceTypeId { get; set; }
        public Guid TaxId { get; set; }
        public Guid? MedicalAreaId { get; set; } = null;
        public List<Guid>? ListServiceTypes { get; set; }
        public List<Guid>? ListMedicalFormIds { get; set; }
        public List<Guid>? LisFieldIds { get; set; }
        public bool IsActive { get; set; }
        public bool IsSales { get; set; }
        public bool IsBuy { get; set; }
        public bool IsInventory { get; set; }
        public bool IsRetention { get; set; }
        public string Comment { get; set; } = string.Empty;

    }
}

namespace AnaPrevention.GeneralMasterData.Api.ServiceCatalogs.Application.Dtos
{
    public class ServiceCatalogMedicalFormDto
    {
        public Guid Id { get; set; }
        public Guid MedicalFormId { get; set; }
        public string MedicalForm { get; set; } = String.Empty;
        public Guid ServiceCatalogId { get; set; }
        public string ServiceCatalog { get; set; } = String.Empty;
    }
}

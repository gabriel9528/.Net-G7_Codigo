
namespace AnaPrevention.GeneralMasterData.Api.ServiceCatalogs.Domain.Entities
{
    public class ServiceCatalogMedicalForm
    {
        public Guid Id { get; set; }
        public Guid MedicalFormId { get; set; }
        public MedicalForm? MedicalForm { get; set; }
        public Guid ServiceCatalogId { get; set; }
        public ServiceCatalog? ServiceCatalog { get; set; }
        public ServiceCatalogMedicalForm(Guid medicalFormId, Guid serviceCatalogId, Guid id)
        {
            MedicalFormId = medicalFormId;
            ServiceCatalogId = serviceCatalogId;
            Id = id;
        }

    }
}

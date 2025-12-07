using AnaPrevention.GeneralMasterData.Api.ServiceCatalogs.Domain.Entities;

namespace AnaPrevention.GeneralMasterData.Api.ServiceTypes.Domain.Entities
{
    public class ServiceCatalogServiceType
    {

        public Guid Id { get; set; }

        public ServiceCatalog ServiceCatalog { get; set; }
        public Guid ServiceCatalogId { get; set; }

        public ServiceType ServiceType { get; set; }
        public Guid ServiceTypeId { get; set; }

        public ServiceCatalogServiceType(Guid serviceCatalogId, Guid serviceTypeId, Guid id)
        {
            ServiceCatalogId = serviceCatalogId;
            ServiceTypeId = serviceTypeId;
            Id = id;
        }
    }
}

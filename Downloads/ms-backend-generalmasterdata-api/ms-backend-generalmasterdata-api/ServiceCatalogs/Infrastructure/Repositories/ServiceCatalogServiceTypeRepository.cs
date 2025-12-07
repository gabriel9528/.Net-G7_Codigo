using AnaPrevention.GeneralMasterData.Api.Common.Infrastructure.EF;

using AnaPrevention.GeneralMasterData.Api.ServiceTypes.Domain.Entities;

namespace AnaPrevention.GeneralMasterData.Api.ServiceCatalogs.Infrastructure.Repositories
{
    public class ServiceCatalogServiceTypeRepository : Repository<ServiceCatalogServiceType>
    {
        public ServiceCatalogServiceTypeRepository(AnaPreventionContext context) : base(context)
        {
        }

        public ServiceCatalogServiceType? GetByServicesCatalogAndServiceTypeId(Guid serviceCatalogId, Guid serviceTypeId)
        {
            return _context.Set<ServiceCatalogServiceType>().SingleOrDefault(x => x.ServiceCatalogId == serviceCatalogId && x.ServiceTypeId == serviceTypeId);
        }

        public List<ServiceCatalogServiceType>? GetServiceTypesByServiceCatalog(Guid serviceCatalogId)
        {
            return _context.Set<ServiceCatalogServiceType>().Where(x => x.ServiceCatalogId == serviceCatalogId).ToList();
        }

        public void RemoveServicCatalogServiceTypeRange(List<ServiceCatalogServiceType> serviceCatalogServiceTypes, Guid userId)
        {
            _context.Set<ServiceCatalogServiceType>().RemoveRange(serviceCatalogServiceTypes);
            _context.SaveChanges(userId);
        }
    }
}
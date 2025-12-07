using Microsoft.EntityFrameworkCore;
using AnaPrevention.GeneralMasterData.Api.Common.Infrastructure.EF;
using AnaPrevention.GeneralMasterData.Api.ServiceCatalogs.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.ServiceCatalogs.Domain.Entities;

namespace AnaPrevention.GeneralMasterData.Api.ServiceCatalogs.Infrastructure.Repositories
{
    public class ServiceCatalogMedicalFormRepository : Repository<ServiceCatalogMedicalForm>
    {
        public ServiceCatalogMedicalFormRepository(AnaPreventionContext context) : base(context)
        {
        }

        public List<ServiceCatalogMedicalFormDto>? GetByServiceCatalogIds(List<Guid> ListServiceCatalogIds)
        {
            return (from t1 in _context.Set<ServiceCatalogMedicalForm>()
                    join t2 in _context.Set<ServiceCatalog>() on t1.ServiceCatalogId equals t2.Id
                    join t3 in _context.Set<MedicalForm>() on t1.MedicalFormId equals t3.Id
                    where ListServiceCatalogIds.Contains(t1.ServiceCatalogId)
                    select new ServiceCatalogMedicalFormDto()
                    {
                        Id = t1.Id,
                        MedicalFormId = t1.MedicalFormId,
                        ServiceCatalogId = t1.ServiceCatalogId,
                        ServiceCatalog = t2.Description,
                        MedicalForm = t3.Description
                    }).Distinct().ToList();
        }

        public ServiceCatalogMedicalForm? GetByServicesCatalogAndMedicalFormId(Guid serviceCatalogId, Guid medicalFormId)
        {
            return _context.Set<ServiceCatalogMedicalForm>().SingleOrDefault(x => x.ServiceCatalogId == serviceCatalogId && x.MedicalFormId == medicalFormId);
        }

    }
}

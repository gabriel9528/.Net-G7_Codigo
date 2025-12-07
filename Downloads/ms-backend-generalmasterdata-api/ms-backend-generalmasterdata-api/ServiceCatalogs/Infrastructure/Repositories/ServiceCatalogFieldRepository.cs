using Microsoft.EntityFrameworkCore;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Common.Infrastructure.EF;
using AnaPrevention.GeneralMasterData.Api.Fields.Application.Dtos.Fields;
using AnaPrevention.GeneralMasterData.Api.Fields.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.ServiceCatalogs.Domain.Entities;

namespace AnaPrevention.GeneralMasterData.Api.ServiceCatalogs.Infrastructure.Repositories
{
    public class ServiceCatalogFieldRepository : Repository<ServiceCatalogField>
    {
        public ServiceCatalogFieldRepository(AnaPreventionContext context) : base(context)
        {
        }
        public List<ServiceCatalogField> GetByServiceCatalogIds(List<Guid> ListServiceCatalogIds)
        {
            return _context.Set<ServiceCatalogField>().Where(p => ListServiceCatalogIds.Contains(p.ServiceCatalogId)).Include(s => s.Field).ToList();
        }

        public List<ServiceCatalog> GetByServiceCatalogByFieldId(Guid fieldId)
        {

            return (from t1 in _context.Set<ServiceCatalog>()
                    join t2 in _context.Set<ServiceCatalogField>() on t1.Id equals t2.ServiceCatalogId
                    where t1.Status && t2.FieldId == fieldId
                    orderby t1.Description
                    select t1).ToList();

        }
        public ServiceCatalogField? GetByServicesCatalogAndFieldId(Guid serviceCatalogId, Guid fieldId)
        {
            return _context.Set<ServiceCatalogField>().SingleOrDefault(x => x.ServiceCatalogId == serviceCatalogId && x.FieldId == fieldId);
        }

        public List<ServiceCatalogField> GetServiceCatalogFieldByFieldId(Guid fieldId)
        {
            return _context.Set<ServiceCatalogField>().Where(p => p.FieldId == fieldId).ToList();
        }

        public void ServiceCatalogFieldRemoveRange(List<ServiceCatalogField> serviceCatalogFields, Guid userId)
        {
            _context.Set<ServiceCatalogField>().RemoveRange(serviceCatalogFields);
            _context.SaveChanges(userId);
        }
        public List<FieldLaboratoryDto>? GetFieldLaboratoryByServiceCatalogIds(List<Guid> serviceCatalogIds)
        {

            return (from t1 in _context.Set<Field>()
                    join t2 in _context.Set<ServiceCatalogField>() on t1.Id equals t2.FieldId
                    where t1.Status && serviceCatalogIds.Contains(t2.ServiceCatalogId)
                    orderby t1.Description
                    select new FieldLaboratoryDto()
                    {
                        Id = t1.Id,
                        Description = t1.Description,
                        Code = t1.Code,
                        Name = t1.Name,
                        Legend = t1.Legend,
                        Uom = t1.Uom,
                        FieldType = t1.FieldType,
                        ServiceCatalogId = t2.ServiceCatalogId,
                        IsTittle = t1.IsTittle,
                        OrderRow = t2.OrderRow,
                        FieldExamenType = t1.FieldExamenType,
                        Options = CommonStatic.ConvertJsonToListOptionFieldDto(t1.OptionsJson),
                        Status = t1.Status,

                    }).Distinct().ToList();
        }


        public List<FieldLaboratoryDto>? GetFieldLaboratoryByServiceCatalogId(Guid serviceCatalogId)
        {

            return (from t1 in _context.Set<Field>()
                    join t2 in _context.Set<ServiceCatalogField>() on t1.Id equals t2.FieldId
                    where t1.Status && t2.ServiceCatalogId == serviceCatalogId
                    orderby t1.Description
                    select new FieldLaboratoryDto()
                    {
                        Id = t1.Id,
                        Description = t1.Description,
                        Code = t1.Code,
                        Name = t1.Name,
                        Legend = t1.Legend,
                        Uom = t1.Uom,
                        FieldType = t1.FieldType,
                        IsTittle = t1.IsTittle,
                        OrderRow = t2.OrderRow,
                        ServiceCatalogId = t2.ServiceCatalogId,
                        FieldExamenType = t1.FieldExamenType,
                        Options = CommonStatic.ConvertJsonToListOptionFieldDto(t1.OptionsJson),
                        Status = t1.Status,

                    }).Distinct().ToList();
        }
    }
}

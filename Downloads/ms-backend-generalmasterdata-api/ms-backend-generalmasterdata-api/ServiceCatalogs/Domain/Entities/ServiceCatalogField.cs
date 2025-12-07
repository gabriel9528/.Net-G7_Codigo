using AnaPrevention.GeneralMasterData.Api.Fields.Domain.Entities;

namespace AnaPrevention.GeneralMasterData.Api.ServiceCatalogs.Domain.Entities
{
    public class ServiceCatalogField
    {

        public Guid Id { get; set; }
        public Guid ServiceCatalogId { get; set; }
        public ServiceCatalog? ServiceCatalog { get; set; }
        public Guid FieldId { get; set; }
        public Field? Field { get; set; }
        public int OrderRow { get; set; }

        public ServiceCatalogField() { }
        public ServiceCatalogField(Guid serviceCatalogId, Guid fieldId, Guid id, int orderRow = 999)
        {
            ServiceCatalogId = serviceCatalogId;
            FieldId = fieldId;
            Id = id;
            OrderRow = orderRow;
        }
    }
}

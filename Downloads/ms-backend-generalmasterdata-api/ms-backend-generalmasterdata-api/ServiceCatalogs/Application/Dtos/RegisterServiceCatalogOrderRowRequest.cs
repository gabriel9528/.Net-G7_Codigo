using AnaPrevention.GeneralMasterData.Api.ServiceCatalogs.Domain.Enums;

namespace AnaPrevention.GeneralMasterData.Api.ServiceCatalogs.Application.Dtos
{
    public class RegisterServiceCatalogOrderRowRequest
    {
        public Guid Id { get; set; }
        public int OrderRow { get; set; }
        public List<RegisterServiceCatalogOrderRowRequest>? Sons { get; set; }
        public OrderEntityType OrderEntityType { get; set; }
    }
}

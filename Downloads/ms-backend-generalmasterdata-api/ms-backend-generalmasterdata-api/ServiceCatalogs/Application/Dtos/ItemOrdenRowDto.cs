using AnaPrevention.GeneralMasterData.Api.ServiceCatalogs.Domain.Enums;

namespace AnaPrevention.GeneralMasterData.Api.ServiceCatalogs.Application.Dtos
{
    public class ItemOrdenRowDto
    {
        public Guid Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int OrderRow { get; set; }
        public List<ItemOrdenRowDto>? Sons { get; set; }
        public OrderEntityType OrderEntityType { get; set; }
    }
}

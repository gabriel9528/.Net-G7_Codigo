namespace AnaPrevention.GeneralMasterData.Api.ServiceCatalogs.Application.Dtos
{
    public class ServiceCatalogBasicDto
    {
        public Guid Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public decimal TaxRate { get; set; }
    }
}

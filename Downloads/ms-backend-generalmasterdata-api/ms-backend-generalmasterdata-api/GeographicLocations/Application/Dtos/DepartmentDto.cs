namespace AnaPrevention.GeneralMasterData.Api.GeographicLocations.Application.Dtos
{
    public class DepartmentDto
    {
        public string Id { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? CountryId { get; set; } = string.Empty;
        public string? Country { get; set; } = string.Empty;
        public bool Status { get; set; }
    }
}

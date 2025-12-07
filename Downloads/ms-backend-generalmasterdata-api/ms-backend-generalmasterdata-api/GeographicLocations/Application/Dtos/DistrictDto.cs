namespace AnaPrevention.GeneralMasterData.Api.GeographicLocations.Application.Dtos
{
    public class DistrictDto
    {
        public string Id { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? ProvinceId { get; set; } = string.Empty;
        public string? Province { get; set; } = string.Empty;
        public string? Department { get; set; } = string.Empty;
        public bool Status { get; set; }
    }
}

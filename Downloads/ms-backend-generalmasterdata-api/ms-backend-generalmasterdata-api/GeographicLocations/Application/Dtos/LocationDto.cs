namespace AnaPrevention.GeneralMasterData.Api.GeographicLocations.Application.Dtos
{
    public class LocationDto
    {
        public string? Ubigeo { get; set; } = string.Empty;
        public string? District { get; set; } = string.Empty;
        public string? Province { get; set; } = string.Empty;
        public string? Department { get; set; } = string.Empty;
    }
}

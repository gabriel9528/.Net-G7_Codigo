namespace AnaPrevention.GeneralMasterData.Api.GeographicLocations.Application.Dtos
{
    public class ProvinceDto
    {
        public string Id { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? DepartmentId { get; set; } = string.Empty;
        public string? Department { get; set; } = string.Empty;
        public bool Status { get; set; }
    }
}

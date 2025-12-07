namespace AnaPrevention.GeneralMasterData.Api.GeographicLocations.Application.Dtos
{
    public class CountryDto
    {
        public string Id { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? SecondDescription { get; set; } = string.Empty;
        public string? SecondCode { get; set; } = string.Empty;
        public string? PhoneCode { get; set; } = string.Empty;
        public bool Status { get; set; }
    }
}

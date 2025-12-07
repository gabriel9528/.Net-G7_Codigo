namespace AnaPrevention.GeneralMasterData.Api.GeographicLocations.Domain.Entities
{
    public class Department
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public string? CountryId { get; set; }
        public Country? Country { get; set; }
        public bool Status { get; set; }

        public Department() { }

        public Department(string id, string description, string? countryId)
        {
            Id = id;
            Description = description;
            CountryId = countryId;
            Status = true;
        }
    }
}

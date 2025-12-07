namespace AnaPrevention.GeneralMasterData.Api.GeographicLocations.Domain.Entities
{
    public class District
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public string? ProvinceId { get; set; }
        public Province? Province { get; set; }
        public bool Status { get; set; }

        public District() { }

        public District(string id, string description, string? provinceId)
        {
            Id = id;
            Description = description;
            ProvinceId = provinceId;
            Status = true;
        }
    }
}

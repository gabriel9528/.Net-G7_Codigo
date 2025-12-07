namespace AnaPrevention.GeneralMasterData.Api.GeographicLocations.Domain.Entities
{
    public class Country
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public string? SecondDescription { get; set; }
        public string? SecondCode { get; set; }
        public string? PhoneCode { get; set; }
        public bool Status { get; set; }

        public Country() { }

        public Country(string id, string description, string? secondDescription, string? secondCode, string? phoneCode)
        {
            Id = id;
            Description = description;
            SecondDescription = secondDescription;
            SecondCode = secondCode;
            PhoneCode = phoneCode;
            Status = true;
        }
    }
}

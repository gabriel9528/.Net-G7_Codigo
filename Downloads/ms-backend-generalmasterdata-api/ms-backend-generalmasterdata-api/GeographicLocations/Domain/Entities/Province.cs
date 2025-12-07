namespace AnaPrevention.GeneralMasterData.Api.GeographicLocations.Domain.Entities
{
    public class Province
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public string? DepartmentId { get; set; }
        public Department? Department { get; set; }
        public bool Status { get; set; }

        public Province() { }

        public Province(string id, string description, string? departamentId)
        {
            Id = id;
            Description = description;
            DepartmentId = departamentId;
            Status = true;
        }
    }
}

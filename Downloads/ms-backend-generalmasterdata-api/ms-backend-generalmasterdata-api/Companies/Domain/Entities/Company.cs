namespace AnaPrevention.GeneralMasterData.Api.Companies.Domain.Entities
{
    public class Company
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public string? Setting { get; set; }
        public bool Status { get; set; }

        public Company()
        {

        }
        public Company(string description, string setting, Guid id)
        {
            Description = description;
            Setting = setting;
            Status = true;
            Id = id;
        }
    }
}

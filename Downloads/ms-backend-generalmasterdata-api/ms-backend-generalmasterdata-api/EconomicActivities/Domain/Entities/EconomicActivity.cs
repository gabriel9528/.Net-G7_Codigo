

namespace AnaPrevention.GeneralMasterData.Api.EconomicActivities.Domain.Entities
{
    public class EconomicActivity
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public bool Status { get; set; }

        public EconomicActivity() { }

        public EconomicActivity(string description, string code, Guid id)
        {
            Description = description;
            Code = code;
            Status = true;
            Id = id;
        }
    }
}

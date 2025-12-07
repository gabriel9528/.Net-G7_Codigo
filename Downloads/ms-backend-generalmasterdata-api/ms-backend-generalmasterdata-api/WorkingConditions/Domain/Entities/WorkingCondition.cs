namespace AnaPrevention.GeneralMasterData.Api.WorkingConditions.Domain.Entities
{
    public class WorkingCondition
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public int Code { get; set; }
        public bool Status { get; set; }

        public WorkingCondition() { }

        public WorkingCondition(string description, int code, Guid id) {
            Description = description;
            Code = code;
            Status = true;
            Id= id;
        }
    }
}

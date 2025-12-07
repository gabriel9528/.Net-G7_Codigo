namespace AnaPrevention.GeneralMasterData.Api.CreditTimes.Domain.Entities
{
    public class CreditTime
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public int NumberDay { get; set; }        
        public bool Status { get; set; }

        public CreditTime() { }

        public CreditTime(string description, string code, int numberDay, Guid id)
        {
            Description = description;
            Code = code;
            NumberDay = numberDay;
            Status = true;
            Id = id;
        }
    }
}

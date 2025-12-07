namespace AnaPrevention.GeneralMasterData.Api.Taxes.Domain.Entities
{
    public class Tax
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public decimal Rate { get; set; }
        public bool Status { get; set; }
        public string Code { get; set; } 

        public Tax()
        {

        }

        public Tax(string description, decimal rate, string code, Guid id)
        {
            Description = description;
            Rate = rate;
            Code = code;
            Status = true;
            Id = id;
        }
    }
}

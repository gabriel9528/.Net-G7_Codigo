namespace AnaPrevention.GeneralMasterData.Api.ExistenceTypes.Domain.Entities
{
    public class ExistenceType
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public bool Status { get; set; }

        public ExistenceType() { }

        public ExistenceType(string description, string code, Guid id)
        {
            Description = description;
            Code = code;
            Status = true;
            Id = id;
        }
    }
}

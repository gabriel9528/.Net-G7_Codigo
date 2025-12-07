namespace AnaPrevention.GeneralMasterData.Api.ItemTypes.Domain.Entities
{
    public class ItemType
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public bool Status { get; set; }

        public ItemType() { }

        public ItemType(string description, string code, Guid id)
        {
            Description = description;
            Code = code;
            Status = true;
            Id = id;
        }
    }
}

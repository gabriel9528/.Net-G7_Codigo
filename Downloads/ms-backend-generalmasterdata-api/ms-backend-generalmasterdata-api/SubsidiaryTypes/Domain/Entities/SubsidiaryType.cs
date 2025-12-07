namespace AnaPrevention.GeneralMasterData.Api.SubsidiaryTypes.Domain.Entities
{
    public class SubsidiaryType
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }

        private SubsidiaryType()
        {

        }

        public SubsidiaryType(string description, string code, Guid id)
        {
            Code = code;
            Description = description;
            Status = true;
            Id = id;
        }
    }
}

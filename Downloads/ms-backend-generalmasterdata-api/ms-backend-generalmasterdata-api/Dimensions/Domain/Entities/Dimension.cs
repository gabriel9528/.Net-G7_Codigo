namespace AnaPrevention.GeneralMasterData.Api.Dimensions.Domain.Entities
{
    public class Dimension
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public Guid CompanyId { get; set; }
        public bool Status { get; set; }

        private Dimension()
        {

        }

        public Dimension(string description, string code, Guid companyId, Guid id)
        {
            Code = code;
            Description = description;
            Status = true;
            CompanyId = companyId;
            Id = id;
        }
    }
}

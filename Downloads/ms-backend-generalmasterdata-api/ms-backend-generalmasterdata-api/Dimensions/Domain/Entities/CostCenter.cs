namespace AnaPrevention.GeneralMasterData.Api.Dimensions.Domain.Entities
{
    public class CostCenter
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
        public Guid DimensionId { get; set; }
        public Dimension Dimension { get; set; }

        private CostCenter()
        {

        }

        public CostCenter(string description, string code, Guid dimensionId, Guid id)
        {
            Code = code;
            Description = description;
            Status = true;
            DimensionId = dimensionId;
            Id = id;
        }
    }
}

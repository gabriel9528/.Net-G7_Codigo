using AnaPrevention.GeneralMasterData.Api.Businesses.Domain.Entities;

namespace AnaPrevention.GeneralMasterData.Api.BusinessCostCenters.Domain.Entities
{
    public class BusinessCostCenter
    {
        public Guid Id { get; set; }
        public Guid BusinessId { get; set; }
        public Business Business { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }

        public BusinessCostCenter() { }

        public BusinessCostCenter(string description, Guid businessId, Guid id)
        {
            Description = description;
            BusinessId = businessId;
            Status = true;
            Id = id;
        }
    }
}

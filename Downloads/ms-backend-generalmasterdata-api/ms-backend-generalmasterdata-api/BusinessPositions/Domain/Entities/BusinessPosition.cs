using AnaPrevention.GeneralMasterData.Api.BusinessAreas.Domain.Entities;

namespace AnaPrevention.GeneralMasterData.Api.BusinessPositions.Domain.Entities
{
    public class BusinessPosition
    {
        public Guid Id { get; set; }
        public Guid BusinessAreaId { get; set; }
        public BusinessArea BusinessArea { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }

        public BusinessPosition() { }

        public BusinessPosition(string description, Guid businessAreaId, Guid id)
        {
            Description = description;
            BusinessAreaId = businessAreaId;
            Status = true;
            Id = id;
        }
    }
}

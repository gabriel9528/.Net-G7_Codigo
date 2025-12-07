using AnaPrevention.GeneralMasterData.Api.Businesses.Domain.Entities;

namespace AnaPrevention.GeneralMasterData.Api.BusinessProjects.Domain.Entities
{
    public class BusinessProject
    {
        public Guid Id { get; set; }
        public Guid BusinessId { get; set; }
        public Business Business { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }

        public BusinessProject() { }

        public BusinessProject(string description, Guid businessId)
        {
            Description = description;
            BusinessId = businessId;
            Status = true;
            Id = Guid.NewGuid();
        }
    }
}

using AnaPrevention.GeneralMasterData.Api.Persons.Domain.Enums;

namespace AnaPrevention.GeneralMasterData.Api.Persons.Domain.Entities
{
    public class MaritalStatus
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public MaritalStatusType Code { get; set; }

        public MaritalStatus() { }

        public MaritalStatus(string description, MaritalStatusType code, Guid id)
        {
            Description = description;
            Code = code;
            Id = id;
        }
    }
}

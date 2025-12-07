using AnaPrevention.GeneralMasterData.Api.Persons.Domain.Enums;

namespace AnaPrevention.GeneralMasterData.Api.Persons.Domain.Entities
{
    public class Gender
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public GenderType Code { get; set; }

        public Gender() { }

        public Gender(string description, GenderType code, Guid id)
        {
            Description = description;
            Code = code;
            Id = id;
        }

    }
}

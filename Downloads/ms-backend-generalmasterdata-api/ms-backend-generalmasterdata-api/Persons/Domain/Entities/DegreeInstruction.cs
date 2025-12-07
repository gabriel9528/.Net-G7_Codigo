using AnaPrevention.GeneralMasterData.Api.Persons.Domain.Enums;

namespace AnaPrevention.GeneralMasterData.Api.Persons.Domain.Entities
{
    public class DegreeInstruction
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public DegreeInstructionType Code { get; set; }

        public DegreeInstruction() { }

        public DegreeInstruction(string description, DegreeInstructionType code, Guid id)
        {
            Description = description;
            Code = code;
            Id = id;
        }
    }
}

using AnaPrevention.GeneralMasterData.Api.Lines.Domain.Enums;

namespace AnaPrevention.GeneralMasterData.Api.Lines.Domain.Entities
{
    public class LineType
    {
        public Guid Id { get; set; }
        public CodeLineType Code { get; set; }
        public string Description { get; set; }

        public LineType()
        {

        }

        public LineType(CodeLineType code, string description, Guid id)
        {
            Code = code;
            Description = description;
            Id = id;
        }
    }
}

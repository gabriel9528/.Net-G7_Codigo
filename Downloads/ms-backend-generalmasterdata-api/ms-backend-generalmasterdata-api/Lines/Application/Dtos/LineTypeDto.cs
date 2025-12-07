using AnaPrevention.GeneralMasterData.Api.Lines.Domain.Enums;

namespace AnaPrevention.GeneralMasterData.Api.Lines.Application.Dtos
{
    public class LineTypeDto
    {
        public Guid Id { get; set; }
        public CodeLineType Code { get; set; }
        public string Description { get; set; } = String.Empty;
    }
}

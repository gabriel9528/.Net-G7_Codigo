using AnaPrevention.GeneralMasterData.Api.Lines.Domain.Enums;

namespace AnaPrevention.GeneralMasterData.Api.Lines.Application.Dtos
{
    public class LineDto
    {
        public Guid Id { get; set; }
        public Guid CompanyId { get; set; }
        public string Code { get; set; } = String.Empty;
        public string Description { get; set; } = String.Empty;
        public bool Status { get; set; }
        public string LineType { get; set; } = String.Empty;
        public Guid LineTypeId { get; set; }
        public CodeLineType CodeLineType { get; set; }
    }
}

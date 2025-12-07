namespace AnaPrevention.GeneralMasterData.Api.Common.Application.Dtos.OccupationalMasterData.Dto
{
    public class OrderMinDto
    {
        public Guid Id { get; set; }
        public string DateOrder { get; set; } = string.Empty;
        public DateTime DateOrderFormat { get; set; }
        public Guid PersonId { get; set; }
        public string Person { get; set; } = string.Empty;
        public string Names { get; set; } = string.Empty;
        public string LastNames { get; set; } = string.Empty;
        public string DocumentNumber { get; set; } = string.Empty;
        public Guid? IdentityDocumentTypeId { get; set; }
        public string? PersonalEmail { get; set; }
    }
}

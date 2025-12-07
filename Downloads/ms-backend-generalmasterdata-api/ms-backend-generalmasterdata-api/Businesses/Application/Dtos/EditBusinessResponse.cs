namespace AnaPrevention.GeneralMasterData.Api.Businesses.Application.Dtos
{
    public class EditBusinessResponse
    {
        public Guid Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Tradename { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public Guid IdentityDocumentTypeId { get; set; }
        public Guid MedicalFormatId { get; set; }
        public Guid CreditTimeId { get; set; }
        public string DistrictId { get; set; } = string.Empty;
        public string DocumentNumber { get; set; } = string.Empty;
        public string Comment { get; set; } = string.Empty;
        public DateTime DateInscription { get; set; }
        public bool IsActive { get; set; }
        public bool Status { get; set; }
        public List<string>? FileUrl { get; set; } = new List<string>();
    }
}

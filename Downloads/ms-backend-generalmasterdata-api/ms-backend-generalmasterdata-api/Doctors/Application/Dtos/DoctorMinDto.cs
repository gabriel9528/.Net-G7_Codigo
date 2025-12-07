namespace AnaPrevention.GeneralMasterData.Api.Doctors.Application.Dtos
{
    public class DoctorMinDto
    {
        public Guid? Id { get; set; }
        public string? FullName { get; set; } = string.Empty;
        public string? IdentityDocumentType { get; set; } = string.Empty;
        public string? DocumentNumber { get; set; } = string.Empty;
        public string? Code { get; set; } = string.Empty;
    }
}

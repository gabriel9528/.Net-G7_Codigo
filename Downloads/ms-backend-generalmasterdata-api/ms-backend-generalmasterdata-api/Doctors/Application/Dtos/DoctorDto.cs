using AnaPrevention.GeneralMasterData.Api.Specialties.Application.Dtos;

namespace AnaPrevention.GeneralMasterData.Api.Doctors.Application.Dtos
{
    public class DoctorDto
    {
        public Guid Id { get; set; }
        public Guid PersonId { get; set; }
        public Guid? UserId { get; set; }
        public string Certifications { get; set; } = string.Empty;
        public string Photo { get; set; } = string.Empty;
        public string Signs { get; set; } = string.Empty;
        public string DocumentNumber { get; set; } = string.Empty;
        public Guid IdentityDocumentTypeId { get; set; }
        public string IdentityDocumentTypeDescription { get; set; } = string.Empty;
        public string Names { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string SecondLastName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public List<Dictionary<string, string>>? ListCertifications { get; set; }
        public List<DoctorSpecialtyDto>? Specialties { get; set; }
        public bool Status { get; set; }
    }
}

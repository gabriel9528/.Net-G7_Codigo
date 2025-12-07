namespace AnaPrevention.GeneralMasterData.Api.Doctors.Application.Dtos
{
    public class EditDoctorRequest
    {
        public Guid Id { get; set; }
        public List<EditDoctorSpecialtyRequest>? ListSpecialty { get; set; }
        public List<RegisterCertificationsRequest>? Certifications { get; set; }
        public List<Guid>? ListMedicalAreaIds { get; set; }
        public string Photo { get; set; } = string.Empty;
        public string Signs { get; set; } = string.Empty;
        public string? Code { get; set; } = string.Empty;
    }
}

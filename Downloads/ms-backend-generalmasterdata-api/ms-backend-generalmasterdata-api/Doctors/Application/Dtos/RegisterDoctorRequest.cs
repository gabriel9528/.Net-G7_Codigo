namespace AnaPrevention.GeneralMasterData.Api.Doctors.Application.Dtos
{
    public class RegisterDoctorRequest
    {
        public List<RegisterDoctorSpecialtyRequest>? ListSpeciality { get; set; }
        public List<Guid>? ListMedicalAreaIds { get; set; }
        public List<RegisterCertificationsRequest>? Certifications { get; set; }
        public Guid PersonId { get; set; }
        public string Photo { get; set; } = string.Empty;
        public string Signs { get; set; } = string.Empty;
        public string? Code { get; set; } = string.Empty;

    }
}

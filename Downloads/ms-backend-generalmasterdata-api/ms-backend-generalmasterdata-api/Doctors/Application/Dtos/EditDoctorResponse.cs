namespace AnaPrevention.GeneralMasterData.Api.Doctors.Application.Dtos
{
    public class EditDoctorResponse
    {
        public Guid Id { get; set; }
        public string Certifications { get; set; } = string.Empty;
        public List<Guid>? ListSpecialityId { get; set; }
        public string Photo { get; set; } = string.Empty;
        public string Signs { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public bool Status { get; set; }
    }
}

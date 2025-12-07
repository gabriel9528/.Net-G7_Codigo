namespace AnaPrevention.GeneralMasterData.Api.Doctors.Application.Dtos
{
    public class DoctorFormatCertificationDto
    {
        public Guid OccupationalHealthId { get; set; }
        public Guid Id { get; set; }
        public Guid PersonId { get; set; }
        public Guid? UserId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Photo { get; set; } = string.Empty;
        public string Signs { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public bool Status { get; set; }
        public string DocumentNumber { get; set; } = string.Empty;
        public string DocumentType { get; set; } = string.Empty;
        public List<DoctorSpecialtyDto>? Specialties { get; set; }
    }
}

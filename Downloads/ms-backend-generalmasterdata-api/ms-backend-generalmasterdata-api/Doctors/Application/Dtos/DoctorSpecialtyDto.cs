namespace AnaPrevention.GeneralMasterData.Api.Doctors.Application.Dtos
{
    public class DoctorSpecialtyDto
    {
        public Guid SpecialtyId { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public Guid DoctorId { get; set; }
    }
}

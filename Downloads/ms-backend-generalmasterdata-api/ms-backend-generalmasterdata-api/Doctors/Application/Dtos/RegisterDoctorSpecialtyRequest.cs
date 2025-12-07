namespace AnaPrevention.GeneralMasterData.Api.Doctors.Application.Dtos
{
    public class RegisterDoctorSpecialtyRequest
    {
        public Guid SpecialtyId { get; set; }
        public string Code { get; set; } = String.Empty;
    }
}

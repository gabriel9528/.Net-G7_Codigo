namespace AnaPrevention.GeneralMasterData.Api.Doctors.Application.Dtos
{
    public class RegisterDoctorMedicalAreaRequest
    {
        public Guid Id { get; set; }
        public List<Guid>? ListMedicalAreaIds { get; set; }
    }
}

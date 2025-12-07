namespace AnaPrevention.GeneralMasterData.Api.Doctors.Application.Dtos
{
    public class EditDoctorSpecialtyRequest
    {
        public Guid SpecialtyId { get; set; }
        public string Code { get; set; } = String.Empty;
        //public bool Status { get; set; } 
    }
}

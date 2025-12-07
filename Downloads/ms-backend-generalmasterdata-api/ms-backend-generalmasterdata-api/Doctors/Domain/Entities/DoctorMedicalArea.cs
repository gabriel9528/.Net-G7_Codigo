using AnaPrevention.GeneralMasterData.Api.MedicalAreas.Domain.Entities;

namespace AnaPrevention.GeneralMasterData.Api.Doctors.Domain.Entities
{
    public class DoctorMedicalArea
    {
        public Guid Id { get; set; }
        public Guid MedicalAreaId { get; set; }
        public MedicalArea MedicalArea { get; set; }
        public Guid DoctorId { get; set; }
        public Doctor Doctor { get; set; }

        public DoctorMedicalArea()
        {

        }
        public DoctorMedicalArea(Guid medicalAreaId, Guid doctorId, Guid id)
        {
            MedicalAreaId = medicalAreaId;
            DoctorId = doctorId;
            Id = id;
        }
    }
}

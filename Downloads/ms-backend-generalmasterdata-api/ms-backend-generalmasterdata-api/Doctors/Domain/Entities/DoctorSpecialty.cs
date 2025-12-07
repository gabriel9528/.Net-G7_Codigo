using AnaPrevention.GeneralMasterData.Api.Specialties.Entities;

namespace AnaPrevention.GeneralMasterData.Api.Doctors.Domain.Entities
{
    public class DoctorSpecialty
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public Guid SpecialtyId { get; set; }
        public Specialty Specialty { get; set; }
        public Guid DoctorId { get; set; }
        public Doctor Doctor { get; set; }

        public DoctorSpecialty()
        {

        }

        public DoctorSpecialty(string code, Guid doctorId, Guid specialtyId, Guid id)
        {
            Code = code;
            DoctorId = doctorId;
            SpecialtyId = specialtyId;
            Id = id;
        }
    }
}

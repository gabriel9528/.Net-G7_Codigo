using AnaPrevention.GeneralMasterData.Api.Persons.Domain.Entities;

namespace AnaPrevention.GeneralMasterData.Api.Doctors.Domain.Entities
{
    public class Doctor
    {
        public Guid Id { get; set; }
        public Guid PersonId { get; set; }
        public Person Person { get; set; }
        public string Certifications { get; set; }
        public string Photo { get; set; }
        public string Signs { get; set; }
        public string Code { get; set; }
        public bool Status { get; set; }

        public Doctor() { }

        public Doctor(string code, string certifications, string photo, string signs, Guid personId, Guid id)
        {
            Signs = signs;
            Code = code;
            Certifications = certifications;
            Photo = photo;
            PersonId = personId;
            Id = id;
            Status = true;
        }
    }
}

namespace AnaPrevention.GeneralMasterData.Api.Specialties.Entities
{
    public class Specialty
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }

        private Specialty()
        {

        }

        public Specialty(string description, string code, Guid id)
        {
            Code = code;
            Description = description;
            Status = true;
            Id=id;
        }
    }
}

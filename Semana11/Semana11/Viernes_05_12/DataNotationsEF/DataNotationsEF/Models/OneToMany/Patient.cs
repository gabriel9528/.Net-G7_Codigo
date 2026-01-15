namespace DataNotationsEF.Models.OneToMany
{
    public class Patient
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        //[ForeignKey("Doctor")]
        public int DoctorId { get; set; }
        public virtual Doctor? Doctor { get; set; }
    }
}

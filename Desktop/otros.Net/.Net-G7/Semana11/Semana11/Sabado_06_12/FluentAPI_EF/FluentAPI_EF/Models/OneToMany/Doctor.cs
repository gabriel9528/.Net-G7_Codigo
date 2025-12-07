namespace FluentAPI_EF.Models.OneToMany
{
    public class Doctor
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public virtual ICollection<Patient>? Patients { get; set; }
    }
}

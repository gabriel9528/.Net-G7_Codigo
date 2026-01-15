using System.Text.Json.Serialization;

namespace DataNotationsEF.Models.OneToMany
{
    public class Doctor
    {
        //[Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        [JsonIgnore]
        public virtual ICollection<Patient>? Patients { get; set; }
    }
}

namespace FluentAPI_EF.Models.OneToMany
{
    public class Patient
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int DoctorsIds { get; set; }
        public virtual Doctor? Doctor { get; set; }
    }
}

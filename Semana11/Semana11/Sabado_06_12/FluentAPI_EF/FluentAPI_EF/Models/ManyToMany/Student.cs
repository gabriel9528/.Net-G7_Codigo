namespace FluentAPI_EF.Models.ManyToMany
{
    public class Student
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public ICollection<StudentSubject>? StudentSubjects { get; set; }
    }
}

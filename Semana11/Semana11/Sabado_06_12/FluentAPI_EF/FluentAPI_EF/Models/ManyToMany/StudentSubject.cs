namespace FluentAPI_EF.Models.ManyToMany
{
    public class StudentSubject
    {
        public int StudentId { get; set; }
        public Student? Student { get; set; }
        public int SubjectId { get; set; }
        public Subject? Subject { get; set; }
    }
}

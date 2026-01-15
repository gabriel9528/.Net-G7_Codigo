namespace DataNotationsEF.Models.ManyToMany
{
    public class Student
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public virtual ICollection<StudentSubject>? StudentSubjects { get; set; }
    }
}

using FluentAPI_EF.Data;
using FluentAPI_EF.Models.ManyToMany;
using Microsoft.EntityFrameworkCore;

namespace FluentAPI_EF.Repositories
{
    public class RepositoryManyToMany
    {
        private readonly ApplicationDbContext _context;
        public RepositoryManyToMany(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddStudentAsync(Student student)
        {
            _context.Students.Add(student);
            await _context.SaveChangesAsync();
        }

        public async Task AddSubjectAsync(Subject subject)
        {
            _context.Subjects.Add(subject);
            await _context.SaveChangesAsync();
        }

        public async Task AddStudentSubjectAsync(int studentId, int subjectId)
        {
            await _context.StudentSubjects.AddAsync(new StudentSubject()
            {
                StudentId = studentId,
                SubjectId = subjectId
            });

            await _context.SaveChangesAsync();
        }

        public async Task<List<Student>> GetStudentsAsync() =>
            await _context.Students.Include(x => x.StudentSubjects).ToListAsync();

        public async Task<List<Subject>> GetSubjectsAsync() =>
            await _context.Subjects.Include(x => x.StudentSubjects).ToListAsync();

        public async Task<List<StudentSubject>> GetStudentSubjectsAsync() =>
            await _context.StudentSubjects
            .Include(x => x.Student)
            .Include(x => x.Subject)
            .ToListAsync();

    }
}

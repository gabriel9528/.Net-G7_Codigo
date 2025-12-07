using DataNotationsEF.Data;
using DataNotationsEF.Models.ManyToMany;
using Microsoft.EntityFrameworkCore;

namespace DataNotationsEF.Repo
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

        public async Task<List<Student>> GetStudentsAsync() =>
            await _context.Students.ToListAsync();

        public async Task<List<Subject>> GetSubjectsAsync() =>
            await _context.Subjects.ToListAsync();
    }
}

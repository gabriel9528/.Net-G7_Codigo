using FluentAPI_EF.Models.ManyToMany;
using FluentAPI_EF.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace FluentAPI_EF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManyToManyController : ControllerBase
    {
        private readonly RepositoryManyToMany _repositoryManyToMany;

        public ManyToManyController(RepositoryManyToMany repositoryManyToMany)
        {
            _repositoryManyToMany = repositoryManyToMany;
        }

        [HttpPost("Student")]
        public async Task<IActionResult> AddStudent(Student student)
        {
            await _repositoryManyToMany.AddStudentAsync(student);
            return Ok("Student saved successfully");
        }

        [HttpPost("Subject")]
        public async Task<IActionResult> AddSubject(Subject subject)
        {
            await _repositoryManyToMany.AddSubjectAsync(subject);
            return Ok("Subject saved successfully");
        }

        [HttpPost("StudentSubject")]
        public async Task<IActionResult> AddStudentSubject(int studentId, int subjectId)
        {
            await _repositoryManyToMany.AddStudentSubjectAsync(studentId, subjectId);
            return Ok("Relation created successfully");
        }


        [HttpGet("GetStudents")]
        public async Task<IActionResult> GetStudents() =>
            Ok(await _repositoryManyToMany.GetStudentsAsync());


        [HttpGet("FGetSubjects")]
        public async Task<IActionResult> GetSubjects() =>
            Ok(await _repositoryManyToMany.GetSubjectsAsync());

        [HttpGet("GetStudentSubjects")]
        public async Task<IActionResult> GetStudentSubjects() =>
            Ok(await _repositoryManyToMany.GetStudentSubjectsAsync());
    }
}

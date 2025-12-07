using DataNotationsEF.Models.ManyToMany;
using DataNotationsEF.Repo;
using Microsoft.AspNetCore.Mvc;

namespace DataNotationsEF.Controllers
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
        public async Task<IActionResult> AddStudentAsync(Student student)
        {
            await _repositoryManyToMany.AddStudentAsync(student);
            return Ok("Student saved successfully");
        }

        [HttpPost("Subject")]
        public async Task<IActionResult> AddPatient(Subject subject)
        {
            await _repositoryManyToMany.AddSubjectAsync(subject);
            return Ok("Subject saved successfully");
        }

        [HttpGet("GetStudents")]
        public async Task<IActionResult> GetStudentsAsync() =>
            Ok(await _repositoryManyToMany.GetStudentsAsync());

        [HttpGet("GetSubjects")]
        public async Task<IActionResult> GetSubjectsAsync() =>
            Ok(await _repositoryManyToMany.GetSubjectsAsync());
    }
}

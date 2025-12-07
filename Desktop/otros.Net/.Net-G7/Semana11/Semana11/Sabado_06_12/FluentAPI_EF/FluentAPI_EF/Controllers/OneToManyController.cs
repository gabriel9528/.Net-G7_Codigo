using FluentAPI_EF.Models.OneToMany;
using FluentAPI_EF.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace FluentAPI_EF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OneToManyController : ControllerBase
    {
        private readonly RepositoryOneToMany _repositoryOneToMany;
        public OneToManyController(RepositoryOneToMany repositoryOneToMany)
        {
            _repositoryOneToMany = repositoryOneToMany;
        }

        [HttpPost("Doctor")]
        public async Task<IActionResult> AddDoctor(Doctor doctor)
        {
            await _repositoryOneToMany.AddDoctorAsync(doctor);
            return Ok("Doctor saved successfully");
        }

        [HttpPost("Patient")]
        public async Task<IActionResult> AddPatient(Patient patient)
        {
            await _repositoryOneToMany.AddPatientAsync(patient);
            return Ok("Patient saved successfully");
        }

        [HttpGet("GetDoctors")]
        public async Task<IActionResult> GetDoctors() =>
            Ok(await _repositoryOneToMany.GetDoctorsAsync());

        [HttpGet("GetPatients")]
        public async Task<IActionResult> GetPatients() =>
            Ok(await _repositoryOneToMany.GetPatientsAsync());
    }
}

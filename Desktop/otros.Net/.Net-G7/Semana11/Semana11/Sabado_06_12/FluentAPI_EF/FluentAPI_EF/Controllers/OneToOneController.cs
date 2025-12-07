using FluentAPI_EF.Models.OneToOne;
using FluentAPI_EF.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace FluentAPI_EF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OneToOneController : ControllerBase
    {
        private readonly RepositoryOneToOne _repositoryOneToOne;
        public OneToOneController(RepositoryOneToOne repositoryOneToOne)
        {
            _repositoryOneToOne = repositoryOneToOne;
        }

        [HttpPost("CarCompany")]
        public async Task<IActionResult> AddCarCompany(CarCompany carCompany)
        {
            await _repositoryOneToOne.AddCarCompany(carCompany);
            return Ok("Car Company saved successfullly");
        }

        [HttpPost("CarModel")]
        public async Task<IActionResult> AddCarModel(CarModel carModel)
        {
            await _repositoryOneToOne.AddCarModel(carModel);
            return Ok("Car Model saved successfullly");
        }

        [HttpGet("GetCarCompanies")]
        public async Task<IActionResult> GetCarCompanies() =>
            Ok(await _repositoryOneToOne.GetCarCompaniesAsync());

        [HttpGet("GetCarModels")]
        public async Task<IActionResult> GetCarModels(CarModel carModel) =>
            Ok(await _repositoryOneToOne.GetCarModelsAsync());

    }
}

using DataNotationsEF.Models.OneToOne;
using DataNotationsEF.Repo;
using Microsoft.AspNetCore.Mvc;

namespace DataNotationsEF.Controllers
{
    public class OneToOneController : Controller
    {
        private readonly RepositoryOneToOne _repositoryOneToOne;
        public OneToOneController(RepositoryOneToOne repositoryOneToOne)
        {
            _repositoryOneToOne = repositoryOneToOne;
        }

        [HttpPost("CarCompany")]
        public async Task<IActionResult> AddCarCompany([FromBody] CarCompany carCompany)
        {
            await _repositoryOneToOne.AddCarCompany(carCompany);
            return Ok("Car Company add successfully");
        }

        [HttpPost("CarModel")]
        public async Task<IActionResult> AddCarModel([FromBody] CarModel carModel)
        {
            await _repositoryOneToOne.AddCarModel(carModel);
            return Ok("Car Model add successfully");
        }

        [HttpGet("GetCarCompanies")]
        public async Task<IActionResult> GetCarCompanies()
        {
            var carCompanies = await _repositoryOneToOne.GetCarCompaniesAsync();
            return Ok(carCompanies);
        }

        [HttpGet("GetCarModels")]
        public async Task<IActionResult> GetCarModels()
        {
            var carModels = await _repositoryOneToOne.GetCarModelssAsync();
            return Ok(carModels);
        }
    }
}

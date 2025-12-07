using Microsoft.AspNetCore.Mvc;
using AnaPrevention.GeneralMasterData.Api.Common.API;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Services;
using AnaPrevention.GeneralMasterData.Api.GeographicLocations.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.GeographicLocations.Application.Services;
using Asp.Versioning;

namespace AnaPrevention.GeneralMasterData.Api.GeographicLocations.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/geoLocations")]
    public class GeographicLocationController(IHttpContextAccessor httpContextAccessor, IConfiguration configuration, CountryApplicationService countryApplicationService, DepartmentApplicationService departmentApplicationService, ProvinceApplicationService provinceApplicationService, DistrictApplicationService districtApplicationService) : ApplicationController(httpContextAccessor, configuration)
    {
        private readonly CountryApplicationService _countryApplicationService = countryApplicationService;
        private readonly DepartmentApplicationService _departmentApplicationService = departmentApplicationService;
        private readonly ProvinceApplicationService _provinceApplicationService = provinceApplicationService;
        private readonly DistrictApplicationService _districtApplicationService = districtApplicationService;

        [HttpGet("{id}/country")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetCountryById(string id)
        {
            try
            {
                CountryDto? countryDto = _countryApplicationService.GetDtoById(id);

                if (countryDto == null)
                    return NotFound();

                return Ok(countryDto);
            }
            catch (Exception ex)
            {
                string msg = (ex.InnerException == null) ? "" : "| Inner Msg Error: " + ex.InnerException.Message.ToString(); ConsoleLog.WriteLine(ex.StackTrace + "| Msg Error:" + ex.Message.ToString() + msg);
                return ServerError();
            }
        }
        [HttpGet("country/getListAutoComplete")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult getcountryListAutoComplete(string? descriptionSearch = "")
        {
            try
            {
                return Ok(_countryApplicationService.GetListAutoComplete(descriptionSearch ?? ""));

            }
            catch (Exception ex)
            {
                string msg = (ex.InnerException == null) ? "" : "| Inner Msg Error: " + ex.InnerException.Message.ToString(); ConsoleLog.WriteLine(ex.StackTrace + "| Msg Error:" + ex.Message.ToString() + msg);
                return ServerError();
            }
        }

        [HttpGet("country/getListAll")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetCountryListAll()
        {
            try
            {
                return Ok(_countryApplicationService.GetListAll());

            }
            catch (Exception ex)
            {
                string msg = (ex.InnerException == null) ? "" : "| Inner Msg Error: " + ex.InnerException.Message.ToString(); ConsoleLog.WriteLine(ex.StackTrace + "| Msg Error:" + ex.Message.ToString() + msg);
                return ServerError();

            }
        }
        [HttpGet("country/getList")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetcountryList(int pageNumber = 1, int pageSize = 10, bool status = true, string? descriptionSearch = "", string? idSearch = "")
        {
            try
            {
                var (country, paginationMetadata) = _countryApplicationService.GetList(pageNumber, pageSize, status, descriptionSearch, idSearch);

                Dictionary<string, object> result = new()
                {
                    { "data", country },
                    { "pagination", paginationMetadata }
                };
                return Ok(result);
            }
            catch (Exception ex)
            {
                string msg = (ex.InnerException == null) ? "" : "| Inner Msg Error: " + ex.InnerException.Message.ToString(); ConsoleLog.WriteLine(ex.StackTrace + "| Msg Error:" + ex.Message.ToString() + msg);
                return ServerError();
            }
        }

        [HttpGet("{id}/department")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetDeparmentById(string id)
        {
            try
            {
                DepartmentDto? departmentDto = _departmentApplicationService.GetDtoById(id);

                if (departmentDto == null)
                    return NotFound();

                return Ok(departmentDto);
            }
            catch (Exception ex)
            {
                string msg = (ex.InnerException == null) ? "" : "| Inner Msg Error: " + ex.InnerException.Message.ToString(); ConsoleLog.WriteLine(ex.StackTrace + "| Msg Error:" + ex.Message.ToString() + msg);
                return ServerError();
            }
        }
        [HttpGet("department/getListAutoComplete")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult getDeparmentListAutoComplete(string? descriptionSearch = "")
        {
            try
            {
                return Ok(_departmentApplicationService.getListAutoComplete(descriptionSearch ?? ""));

            }
            catch (Exception ex)
            {
                string msg = (ex.InnerException == null) ? "" : "| Inner Msg Error: " + ex.InnerException.Message.ToString(); ConsoleLog.WriteLine(ex.StackTrace + "| Msg Error:" + ex.Message.ToString() + msg);
                return ServerError();
            }
        }

        [HttpGet("department/getListAllByCountryId")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult getListAllByCountry(string countryId)
        {
            try
            {
                return Ok(_departmentApplicationService.getListAllByCountryId(countryId));
            }
            catch (Exception ex)
            {
                string msg = (ex.InnerException == null) ? "" : "| Inner Msg Error: " + ex.InnerException.Message.ToString(); ConsoleLog.WriteLine(ex.StackTrace + "| Msg Error:" + ex.Message.ToString() + msg);
                return ServerError();
            }
        }

        [HttpGet("department/getList")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetDeparmentList(int pageNumber = 1, int pageSize = 10, bool status = true, string? descriptionSearch = "", string? idSearch = "")
        {
            try
            {
                var (country, paginationMetadata) = _departmentApplicationService.GetList(pageNumber, pageSize, status, descriptionSearch, idSearch);

                Dictionary<string, object> result = new();
                result.Add("data", country);
                result.Add("pagination", paginationMetadata);
                return Ok(result);
            }
            catch (Exception ex)
            {
                string msg = (ex.InnerException == null) ? "" : "| Inner Msg Error: " + ex.InnerException.Message.ToString(); ConsoleLog.WriteLine(ex.StackTrace + "| Msg Error:" + ex.Message.ToString() + msg);
                return ServerError();
            }
        }

        [HttpGet("{id}/province")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetProvinceById(string id)
        {
            try
            {
                ProvinceDto? provinceDto = _provinceApplicationService.GetDtoById(id);

                if (provinceDto == null)
                    return NotFound();

                return Ok(provinceDto);
            }
            catch (Exception ex)
            {
                string msg = (ex.InnerException == null) ? "" : "| Inner Msg Error: " + ex.InnerException.Message.ToString(); ConsoleLog.WriteLine(ex.StackTrace + "| Msg Error:" + ex.Message.ToString() + msg);
                return ServerError();
            }
        }
        [HttpGet("province/getListAutoComplete")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult getProvinceListAutoComplete(string? descriptionSearch = "")
        {
            try
            {
                return Ok(_provinceApplicationService.getListAutoComplete(descriptionSearch ?? ""));

            }
            catch (Exception ex)
            {
                string msg = (ex.InnerException == null) ? "" : "| Inner Msg Error: " + ex.InnerException.Message.ToString(); ConsoleLog.WriteLine(ex.StackTrace + "| Msg Error:" + ex.Message.ToString() + msg);
                return ServerError();
            }
        }

        [HttpGet("province/getListAllByDepartmentId")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult getListAllByDepartmentId(string departmentId)
        {
            try
            {
                return Ok(_provinceApplicationService.getListAllByDepartmentId(departmentId));
            }
            catch (Exception ex)
            {
                string msg = (ex.InnerException == null) ? "" : "| Inner Msg Error: " + ex.InnerException.Message.ToString(); ConsoleLog.WriteLine(ex.StackTrace + "| Msg Error:" + ex.Message.ToString() + msg);
                return ServerError();
            }
        }

        [HttpGet("province/getList")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetProvinceList(int pageNumber = 1, int pageSize = 10, bool status = true, string? descriptionSearch = "", string? idSearch = "")
        {
            try
            {
                var (country, paginationMetadata) = _provinceApplicationService.GetList(pageNumber, pageSize, status, descriptionSearch, idSearch);

                Dictionary<string, object> result = new();
                result.Add("data", country);
                result.Add("pagination", paginationMetadata);
                return Ok(result);
            }
            catch (Exception ex)
            {
                string msg = (ex.InnerException == null) ? "" : "| Inner Msg Error: " + ex.InnerException.Message.ToString(); ConsoleLog.WriteLine(ex.StackTrace + "| Msg Error:" + ex.Message.ToString() + msg);
                return ServerError();
            }
        }

        [HttpGet("{id}/district")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetDistrictById(string id)
        {
            try
            {
                DistrictDto? districtDto = _districtApplicationService.GetDtoById(id);

                if (districtDto == null)
                    return NotFound();

                return Ok(districtDto);
            }
            catch (Exception ex)
            {
                string msg = (ex.InnerException == null) ? "" : "| Inner Msg Error: " + ex.InnerException.Message.ToString(); ConsoleLog.WriteLine(ex.StackTrace + "| Msg Error:" + ex.Message.ToString() + msg);
                return ServerError();
            }
        }
        [HttpGet("district/getListAutoComplete")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult getDistrictListAutoComplete(string? descriptionSearch = "")
        {
            try
            {
                return Ok(_districtApplicationService.getListAutoComplete(descriptionSearch ?? ""));

            }
            catch (Exception ex)
            {
                string msg = (ex.InnerException == null) ? "" : "| Inner Msg Error: " + ex.InnerException.Message.ToString(); ConsoleLog.WriteLine(ex.StackTrace + "| Msg Error:" + ex.Message.ToString() + msg);
                return ServerError();
            }
        }

        [HttpGet("district/getListAllByDepartmentId")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult getListAllByProvinceId(string provinceId)
        {
            try
            {
                return Ok(_districtApplicationService.getListAllByProvinceId(provinceId));
            }
            catch (Exception ex)
            {
                string msg = (ex.InnerException == null) ? "" : "| Inner Msg Error: " + ex.InnerException.Message.ToString(); ConsoleLog.WriteLine(ex.StackTrace + "| Msg Error:" + ex.Message.ToString() + msg);
                return ServerError();
            }
        }

        [HttpGet("district/getList")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetDistrictList(int pageNumber = 1, int pageSize = 10, bool status = true, string? descriptionSearch = "", string? idSearch = "")
        {
            try
            {
                var (country, paginationMetadata) = _districtApplicationService.GetList(pageNumber, pageSize, status, descriptionSearch, idSearch);

                Dictionary<string, object> result = new();
                result.Add("data", country);
                result.Add("pagination", paginationMetadata);
                return Ok(result);
            }
            catch (Exception ex)
            {
                string msg = (ex.InnerException == null) ? "" : "| Inner Msg Error: " + ex.InnerException.Message.ToString(); ConsoleLog.WriteLine(ex.StackTrace + "| Msg Error:" + ex.Message.ToString() + msg);
                return ServerError();
            }
        }

    }
}

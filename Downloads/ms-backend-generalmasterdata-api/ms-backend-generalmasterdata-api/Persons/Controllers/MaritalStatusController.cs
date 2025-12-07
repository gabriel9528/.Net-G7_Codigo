
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using AnaPrevention.GeneralMasterData.Api.Common.API;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Services;
using AnaPrevention.GeneralMasterData.Api.Persons.Application.Services;

namespace AnaPrevention.GeneralMasterData.Api.Persons.Controllers
{
    [ApiController]
    
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/maritalStatus")]
    public class MaritalStatusController(IHttpContextAccessor httpContextAccessor, IConfiguration configuration, MaritalStatusApplicationService maritalStatusApplicationService) : ApplicationController(httpContextAccessor, configuration)
    {
        private readonly MaritalStatusApplicationService _maritalStatusApplicationService = maritalStatusApplicationService;

        [HttpGet("getListAll")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetListAll()
        {
            try
            {
                return Ok(_maritalStatusApplicationService.GetListAll());
            }
            catch (Exception ex)
            {
                string msg = (ex.InnerException == null) ? "" : "| Inner Msg Error: " + ex.InnerException.Message.ToString(); ConsoleLog.WriteLine(ex.StackTrace + "| Msg Error:" + ex.Message.ToString() + msg);
                return ServerError();
            }
        }
    }
}

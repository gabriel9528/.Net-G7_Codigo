using Microsoft.AspNetCore.Mvc;
using AnaPrevention.GeneralMasterData.Api.Common.API;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Services;
using AnaPrevention.GeneralMasterData.Api.Persons.Application.Services;
using Asp.Versioning;

namespace AnaPrevention.GeneralMasterData.Api.Persons.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/degreeInstruction")]
    public class DegreeInstructionController(IHttpContextAccessor httpContextAccessor, IConfiguration configuration, DegreeInstructionApplicationService degreeInstructionApplicationService) : ApplicationController(httpContextAccessor, configuration)
    {
        private readonly DegreeInstructionApplicationService _degreeInstructionApplicationService = degreeInstructionApplicationService;

        [HttpGet("getListAll")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetListAll()
        {
            try
            {
                return Ok(_degreeInstructionApplicationService.GetListAll());
            }
            catch (Exception ex)
            {
                string msg = (ex.InnerException == null) ? "" : "| Inner Msg Error: " + ex.InnerException.Message.ToString(); ConsoleLog.WriteLine(ex.StackTrace + "| Msg Error:" + ex.Message.ToString() + msg);
                return ServerError();
            }
        }
    }
}

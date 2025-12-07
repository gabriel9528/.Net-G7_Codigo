using AnaPrevention.GeneralMasterData.Api.Common.API;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Services;
using AnaPrevention.GeneralMasterData.Api.Common.Tools.Class;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace AnaPrevention.GeneralMasterData.Api.Tools.Controller
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/tools/")]
    public class ToolController(IHttpContextAccessor httpContextAccessor, IConfiguration configuration) : ApplicationController(httpContextAccessor, configuration)
    {
        [HttpGet("GetDateTimeNow")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetDateNow()
        {
            try
            {
                return Ok(DateTimePersonalized.NowPeru);
            }
            catch (Exception ex)
            {
                string msg = (ex.InnerException == null) ? "" : "| Inner Msg Error: " + ex.InnerException.Message.ToString(); ConsoleLog.WriteLine(ex.StackTrace + "| Msg Error:" + ex.Message.ToString() + msg);
                return ServerError();
            }
        }
    }
}

using CSharpFunctionalExtensions;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using AnaPrevention.GeneralMasterData.Api.Common.API;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Services;
using AnaPrevention.GeneralMasterData.Api.Common.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Lines.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.Lines.Application.Services;
using System.Security.Claims;

namespace AnaPrevention.GeneralMasterData.Api.Lines.Controllers
{
    [ApiController]
    
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/lines")]
    public class LineController(IHttpContextAccessor httpContextAccessor, IConfiguration configuration, LineApplicationService lineApplicationService, LineTypeApplicationService lineTypeApplicationService) : ApplicationController(httpContextAccessor, configuration)
    {
        private readonly LineApplicationService _lineApplicationService = lineApplicationService;        
        private readonly LineTypeApplicationService _lineTypeApplicationService = lineTypeApplicationService;

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult RegisterLine(RegisterLineRequest request)
        {
            try
            {
                var userId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value??"");
                var tokenCompanyId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == "companyId")?.Value??"");

                Result<RegisterLineResponse, Notification> result = _lineApplicationService.RegisterLine(request, tokenCompanyId, userId);

                if (result.IsFailure)
                    return BadRequest(result.Error.GetErrors());

                return Created(result.Value);
            }
            catch (Exception ex)
            {
                string msg = (ex.InnerException == null) ? "" : "| Inner Msg Error: " + ex.InnerException.Message.ToString(); ConsoleLog.WriteLine(ex.StackTrace + "| Msg Error:" + ex.Message.ToString() + msg);
                return ServerError();
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult EditLine(Guid id, EditLineRequest request)
        {
            try
            {
                request.Id = id;
                var userId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value??"");
                var line = _lineApplicationService.GetById(request.Id);

                if (line == null)
                {
                    return NotFound();
                }

                var tokenCompanyId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == "companyId")?.Value??"");
                if (line.CompanyId != tokenCompanyId)
                    return NotFound();

                Notification notification = _lineApplicationService.ValidateEditLineRequest(request, tokenCompanyId);
                if (notification.HasErrors())
                    return BadRequest(notification.GetErrors());

                EditLineResponse response = _lineApplicationService.EditLine(request, line, userId);

                return Ok(response);
            }
            catch (Exception ex)
            {
                string msg = (ex.InnerException == null) ? "" : "| Inner Msg Error: " + ex.InnerException.Message.ToString(); ConsoleLog.WriteLine(ex.StackTrace + "| Msg Error:" + ex.Message.ToString() + msg);
                return ServerError();
            }
        }

        [HttpPatch("active/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult ActiveLine(Guid id)
        {
            try
            {
                var userId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value??"");
                var line = _lineApplicationService.GetById(id);

                if (line == null)
                    return NotFound();

                var tokenCompanyId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == "companyId")?.Value??"");
                if (line.CompanyId != tokenCompanyId)
                    return NotFound();


                EditLineResponse response = _lineApplicationService.ActiveLine(line, userId);

                return Ok(response);
            }
            catch (Exception ex)
            {
                string msg = (ex.InnerException == null) ? "" : "| Inner Msg Error: " + ex.InnerException.Message.ToString(); ConsoleLog.WriteLine(ex.StackTrace + "| Msg Error:" + ex.Message.ToString() + msg);
                return ServerError();
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult RemoveLine(Guid id)
        {
            try
            {
                var userId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value??"");
                var line = _lineApplicationService.GetById(id);

                if (line == null)
                    return NotFound();

                var tokenCompanyId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == "companyId")?.Value??"");
                if (line.CompanyId != tokenCompanyId)
                    return NotFound();

                EditLineResponse response = _lineApplicationService.RemoveLine(line, userId);

                return Ok(response);
            }
            catch (Exception ex)
            {
                string msg = (ex.InnerException == null) ? "" : "| Inner Msg Error: " + ex.InnerException.Message.ToString(); ConsoleLog.WriteLine(ex.StackTrace + "| Msg Error:" + ex.Message.ToString() + msg);
                return ServerError();
            }
        }


        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetById(Guid id)
        {
            try
            {
                var tokenCompanyId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == "companyId")?.Value??"");

                LineDto? lineDto = _lineApplicationService.GetDtoById(id, tokenCompanyId);

                if (lineDto == null)
                    return NotFound();

                return Ok(lineDto);
            }
            catch (Exception ex)
            {
                string msg = (ex.InnerException == null) ? "" : "| Inner Msg Error: " + ex.InnerException.Message.ToString(); ConsoleLog.WriteLine(ex.StackTrace + "| Msg Error:" + ex.Message.ToString() + msg);
                return ServerError();
            }
        }

        [HttpGet("getListAll")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetListAll()
        {
            try
            {
                var tokenCompanyId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == "companyId")?.Value??"");
                return Ok(_lineApplicationService.GetListAll(tokenCompanyId));
            }
            catch (Exception ex)
            {
                string msg = (ex.InnerException == null) ? "" : "| Inner Msg Error: " + ex.InnerException.Message.ToString(); ConsoleLog.WriteLine(ex.StackTrace + "| Msg Error:" + ex.Message.ToString() + msg);
                return ServerError();
            }
        }

        [HttpGet("lineType/getListAll")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetListAllLineType()
        {
            try
            {
                return Ok(_lineTypeApplicationService.GetListAll());
            }
            catch (Exception ex)
            {
                string msg = (ex.InnerException == null) ? "" : "| Inner Msg Error: " + ex.InnerException.Message.ToString(); ConsoleLog.WriteLine(ex.StackTrace + "| Msg Error:" + ex.Message.ToString() + msg);
                return ServerError();
            }
        }

        [HttpGet("getList")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetList(int pageNumber = 1, int pageSize = 10, bool status = true, string? descriptionSearch = "", string? codeSearch = "")
        {
            try
            {
                var tokenCompanyId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == "companyId")?.Value??"");
                var (line, paginationMetadata) = _lineApplicationService.GetList(pageNumber, pageSize, tokenCompanyId, status, descriptionSearch, codeSearch);

                Dictionary<string, object> result = new()
                {
                    { "data", line },
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
      
    }
}

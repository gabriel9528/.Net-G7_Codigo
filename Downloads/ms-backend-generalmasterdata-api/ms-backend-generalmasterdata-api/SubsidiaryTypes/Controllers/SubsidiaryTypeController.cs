using CSharpFunctionalExtensions;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using AnaPrevention.GeneralMasterData.Api.Common.API;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Services;
using AnaPrevention.GeneralMasterData.Api.Common.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.SubsidiaryTypes.Application.Services;
using AnaPrevention.GeneralMasterData.Api.SubsidiaryTypes.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.SubsidiaryTypes.Infrastructure.Repositories;
using System.Security.Claims;

namespace AnaPrevention.GeneralMasterData.Api.SubsidiaryTypes.Controllers
{

    [ApiController]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/subsidiarytype")]
    public class SubsidiaryTypeController : ApplicationController
    {
        private readonly SubsidiaryTypeApplicationService _subsidiaryTypeApplicationService;

        public SubsidiaryTypeController(IHttpContextAccessor httpContextAccessor, IConfiguration configuration, SubsidiaryTypeApplicationService subsidiaryTypeApplicationService) : base(httpContextAccessor, configuration)
        {
            _subsidiaryTypeApplicationService = subsidiaryTypeApplicationService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult RegisterSubsidiaryType(RegisterSubsidiaryTypeRequest request)
        {
            try
            {
                var userId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "");

                Result<RegisterSubsidiaryTypeResponse, Notification> result = _subsidiaryTypeApplicationService.RegisterSubsidiaryType(request, userId);

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
        public IActionResult EditSubsidiaryType(Guid id, EditSubsidiaryTypeRequest request)
        {
            try
            {
                request.Id = id;
                var userId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "");
                var subsidiaryType = _subsidiaryTypeApplicationService.GetById(request.Id);

                if (subsidiaryType == null)
                {
                    return NotFound();
                }


                Notification notification = _subsidiaryTypeApplicationService.ValidateEditSubsidiaryTypeRequest(request);
                if (notification.HasErrors())
                    return BadRequest(notification.GetErrors());

                EditSubsidiaryTypeResponse response = _subsidiaryTypeApplicationService.EditSubsidiaryType(request, subsidiaryType, userId);

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
        public IActionResult ActiveSubsidiaryType(Guid id)
        {
            try
            {
                var subsidiaryType = _subsidiaryTypeApplicationService.GetById(id);
                var userId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "");
                if (subsidiaryType == null)
                    return NotFound();



                EditSubsidiaryTypeResponse response = _subsidiaryTypeApplicationService.ActiveSubsidiaryType(subsidiaryType, userId);

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
        public IActionResult RemoveSubsidiaryType(Guid id)
        {
            try
            {
                var subsidiaryType = _subsidiaryTypeApplicationService.GetById(id);
                var userId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "");
                if (subsidiaryType == null)
                    return NotFound();


                EditSubsidiaryTypeResponse response = _subsidiaryTypeApplicationService.RemoveSubsidiaryType(subsidiaryType, userId);

                return Ok(response);
            }
            catch (Exception ex)
            {
                string msg = (ex.InnerException == null) ? "" : "| Inner Msg Error: " + ex.InnerException.Message.ToString(); ConsoleLog.WriteLine(ex.StackTrace + "| Msg Error:" + ex.Message.ToString() + msg);
                return ServerError();
            }
        }


        [HttpGet("{id}")]
        //[Authorize(Policy = "EmailMustBeFromJPerez")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetById(Guid id)
        {
            try
            {
                var tokenCompanyId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == "companyId")?.Value ?? "");

                SubsidiaryType? subsidiaryTypeDto = _subsidiaryTypeApplicationService.GetDtoById(id, tokenCompanyId);

                if (subsidiaryTypeDto == null)
                    return NotFound();

                return Ok(subsidiaryTypeDto);
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
                var tokenCompanyId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == "companyId")?.Value ?? "");
                return Ok(_subsidiaryTypeApplicationService.GetListAll(tokenCompanyId));
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
                var (subsidiaryType, paginationMetadata) = _subsidiaryTypeApplicationService.GetList(pageNumber, pageSize, status, descriptionSearch, codeSearch);

                Dictionary<string, object> result = new()
                {
                    { "data", subsidiaryType },
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

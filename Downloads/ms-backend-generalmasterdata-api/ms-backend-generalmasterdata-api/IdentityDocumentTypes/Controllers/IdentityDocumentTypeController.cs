using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc;
using AnaPrevention.GeneralMasterData.Api.Common.API;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Services;
using AnaPrevention.GeneralMasterData.Api.Common.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.IdentityDocumentTypes.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.IdentityDocumentTypes.Application.Services;
using AnaPrevention.GeneralMasterData.Api.IdentityDocumentTypes.Domain.Entities;
using System.Security.Claims;
using Asp.Versioning;

namespace AnaPrevention.GeneralMasterData.Api.IdentityDocumentTypes.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/identitydocumenttype")]
    public class IdentityDocumentTypeController(IHttpContextAccessor httpContextAccessor, IConfiguration configuration, IdentityDocumentTypeApplicationService identityDocumentTypeApplicationService) : ApplicationController(httpContextAccessor, configuration)
    {
        private readonly IdentityDocumentTypeApplicationService _identityDocumentTypeApplicationService = identityDocumentTypeApplicationService;

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult RegisterIdentityDocumentType(RegisterIdentityDocumentTypeRequest request)
        {
            try
            {
                var userId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "");
                Result<RegisterIdentityDocumentTypeResponse, Notification> result = _identityDocumentTypeApplicationService.RegisterIdentityDocumentType(request, userId);

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
        public IActionResult EditIdentityDocumentType(Guid id, EditIdentityDocumentTypeRequest request)
        {
            try
            {
                request.Id = id;
                var userId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "");

                var identityDocumentType = _identityDocumentTypeApplicationService.GetById(request.Id);

                if (identityDocumentType == null)
                {
                    return NotFound();
                }

                Notification notification = _identityDocumentTypeApplicationService.ValidateEditIdentityDocumentTypeRequest(request);
                if (notification.HasErrors())
                    return BadRequest(notification.GetErrors());

                EditIdentityDocumentTypeResponse response = _identityDocumentTypeApplicationService.EditIdentityDocumentType(request, identityDocumentType, userId);

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
        public IActionResult RemoveIdentityDocumentType(Guid id)
        {
            try
            {
                var userId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "");
                var identityDocumentType = _identityDocumentTypeApplicationService.GetById(id);

                if (identityDocumentType == null)
                    return NotFound();

                EditIdentityDocumentTypeResponse response = _identityDocumentTypeApplicationService.RemoveIdentityDocumentType(identityDocumentType, userId);

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
        public IActionResult ActiveIdentityDocumentType(Guid id)
        {
            try
            {
                var userId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "");
                var identityDocumentType = _identityDocumentTypeApplicationService.GetById(id);

                if (identityDocumentType == null)
                    return NotFound();


                EditIdentityDocumentTypeResponse response = _identityDocumentTypeApplicationService.ActiveIdentityDocumentType(identityDocumentType, userId);

                return Ok(response);
            }
            catch (Exception ex)
            {
                string msg = (ex.InnerException == null) ? "" : "| Inner Msg Error: " + ex.InnerException.Message.ToString(); ConsoleLog.WriteLine(ex.StackTrace + "| Msg Error:" + ex.Message.ToString() + msg);
                return ServerError();
            }
        }

        /// <summary>
        /// Get a client by id
        /// </summary>
        /// <param name="id">The id of the type user to get</param>
        /// <returns>An IActionResult</returns>
        /// <response code="200">Returns the requested </response>
        [HttpGet("{id}")]
        //[Authorize(Policy = "EmailMustBeFromJPerez")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetById(Guid id)
        {
            try
            {
                IdentityDocumentType? identityDocumentTypeDto = _identityDocumentTypeApplicationService.GetDtoById(id);

                if (identityDocumentTypeDto == null)
                    return NotFound();

                return Ok(identityDocumentTypeDto);
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
        public IActionResult GetList(int pageNumber = 1, int pageSize = 10, bool status = true, string? descriptionSearch = "", string? codeSearch = "", string? abbreviationSearch = "")
        {
            try
            {
                var (identityDocumentType, paginationMetadata) = _identityDocumentTypeApplicationService.GetList(pageNumber, pageSize, status, descriptionSearch, codeSearch, abbreviationSearch);

                Dictionary<string, object> result = new Dictionary<string, object>();
                result.Add("data", identityDocumentType);
                result.Add("pagination", paginationMetadata);
                return Ok(result);
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
        public IActionResult GetAllList()
        {
            try
            {
                List<IdentityDocumentType> ListIdentityDocumentTypeDto = _identityDocumentTypeApplicationService.GetListAll();

                return Ok(ListIdentityDocumentTypeDto);
            }
            catch (Exception ex)
            {
                string msg = (ex.InnerException == null) ? "" : "| Inner Msg Error: " + ex.InnerException.Message.ToString(); ConsoleLog.WriteLine(ex.StackTrace + "| Msg Error:" + ex.Message.ToString() + msg);
                return ServerError();
            }
        }
        [HttpGet("getListOnlyPersonLegal")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetListOnlyPersonLegal()
        {
            try
            {
                List<IdentityDocumentType> ListIdentityDocumentTypeDto = _identityDocumentTypeApplicationService.GetListOnlyPersonLegal();

                return Ok(ListIdentityDocumentTypeDto);
            }
            catch (Exception ex)
            {
                string msg = (ex.InnerException == null) ? "" : "| Inner Msg Error: " + ex.InnerException.Message.ToString(); ConsoleLog.WriteLine(ex.StackTrace + "| Msg Error:" + ex.Message.ToString() + msg);
                return ServerError();
            }
        }
        [HttpGet("getListOnlyPersonNatural")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetListOnlyPersonNatural()
        {
            try
            {
                List<IdentityDocumentType> ListIdentityDocumentTypeDto = _identityDocumentTypeApplicationService.GetListOnlyPersonNatural();

                return Ok(ListIdentityDocumentTypeDto);
            }
            catch (Exception ex)
            {
                string msg = (ex.InnerException == null) ? "" : "| Inner Msg Error: " + ex.InnerException.Message.ToString(); ConsoleLog.WriteLine(ex.StackTrace + "| Msg Error:" + ex.Message.ToString() + msg);
                return ServerError();
            }
        }

        [HttpGet("getListFilter")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetListFilter(bool status = true, string descriptionSearch = "", string codeSearch = "", string abbreviationSearch = "")
        {
            try
            {
                List<IdentityDocumentType> ListIdentityDocumentTypeDto = _identityDocumentTypeApplicationService.GetListFilter(status,descriptionSearch, codeSearch, abbreviationSearch);

                return Ok(ListIdentityDocumentTypeDto);
            }
            catch (Exception ex)
            {
                string msg = (ex.InnerException == null) ? "" : "| Inner Msg Error: " + ex.InnerException.Message.ToString(); ConsoleLog.WriteLine(ex.StackTrace + "| Msg Error:" + ex.Message.ToString() + msg);
                return ServerError();
            }
        }
    }
}

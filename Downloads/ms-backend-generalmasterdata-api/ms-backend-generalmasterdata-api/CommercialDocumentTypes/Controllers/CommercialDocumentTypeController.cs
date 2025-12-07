using CSharpFunctionalExtensions;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using AnaPrevention.GeneralMasterData.Api.CommercialDocumentTypes.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.CommercialDocumentTypes.Application.Services;
using System.Security.Claims;
using AnaPrevention.GeneralMasterData.Api.Common.API;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Services;
using AnaPrevention.GeneralMasterData.Api.Common.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.CommercialDocumentTypes.Application.Entities;

namespace AnaPrevention.GeneralMasterData.Api.CommercialDocumentTypes.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/commercialdocumenttype")]
    public class CommercialDocumentTypeController(IHttpContextAccessor httpContextAccessor, IConfiguration configuration, CommercialDocumentTypeApplicationService commercialDocumentTypeApplicationService) : ApplicationController(httpContextAccessor, configuration)
    {
        private readonly CommercialDocumentTypeApplicationService _commercialDocumentTypeApplicationService = commercialDocumentTypeApplicationService;

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult RegisterCommercialDocumentType(RegisterCommercialDocumentTypeRequest request)
        {
            try
            {
                var userId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value??"");
                Result<RegisterCommercialDocumentTypeResponse, Notification> result = _commercialDocumentTypeApplicationService.RegisterCommercialDocumentType(request, userId);

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
        public IActionResult EditCommercialDocumentType(Guid id, EditCommercialDocumentTypeRequest request)
        {
            try
            {
                request.Id = id;
                var userId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value??"");
                var commercialDocumentType = _commercialDocumentTypeApplicationService.GetById(request.Id);

                if (commercialDocumentType == null)
                {
                    return NotFound();
                }

                Notification notification = _commercialDocumentTypeApplicationService.ValidateEditCommercialDocumentTypeRequest(request);
                if (notification.HasErrors())
                    return BadRequest(notification.GetErrors());

                EditCommercialDocumentTypeResponse response = _commercialDocumentTypeApplicationService.EditCommercialDocumentType(request, commercialDocumentType, userId);

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
        public IActionResult RemoveCommercialDocumentType(Guid id)
        {
            try
            {
                var userId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value??"");
                var commercialDocumentType = _commercialDocumentTypeApplicationService.GetById(id);

                if (commercialDocumentType == null)
                    return NotFound();

                EditCommercialDocumentTypeResponse response = _commercialDocumentTypeApplicationService.RemoveCommercialDocumentTypel(commercialDocumentType, userId);

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
        public IActionResult ActiveCommercialDocumentType(Guid id)
        {
            try
            {
                var userId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value??"");
                var commercialDocumentType = _commercialDocumentTypeApplicationService.GetById(id);

                if (commercialDocumentType == null)
                    return NotFound();

                EditCommercialDocumentTypeResponse response = _commercialDocumentTypeApplicationService.ActiveCommercialDocumentType(commercialDocumentType, userId);

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
                CommercialDocumentType? commercialDocumentType = _commercialDocumentTypeApplicationService.GetDtoById(id);

                if (commercialDocumentType == null)
                    return NotFound();

                return Ok(commercialDocumentType);
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
                var (commercialDocumentType, paginationMetadata) = _commercialDocumentTypeApplicationService.GetList(pageNumber, pageSize, status, descriptionSearch, codeSearch,abbreviationSearch);

                Dictionary<string, object> result = new()
                {
                    { "data", commercialDocumentType },
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

        //[HttpGet("getAudit")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //public IActionResult GetAudit(Guid id, int pageNumber = 1, int pageSize = 10)
        //{
        //    try
        //    {
        //        var (commercialDocument, paginationMetadata) = _auditApplicationService.GetAuditMaster(pageNumber, pageSize, id, "CommercialDocumentType");

        //        Dictionary<string, object> result = new Dictionary<string, object>();
        //        result.Add("data", commercialDocument);
        //        result.Add("pagination", paginationMetadata);
        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        string msg = (ex.InnerException == null) ? "" : "| Inner Msg Error: " + ex.InnerException.Message.ToString(); ConsoleLog.WriteLine(ex.StackTrace + "| Msg Error:" + ex.Message.ToString() + msg);
        //        return ServerError();
        //    }
        //}
    }
}

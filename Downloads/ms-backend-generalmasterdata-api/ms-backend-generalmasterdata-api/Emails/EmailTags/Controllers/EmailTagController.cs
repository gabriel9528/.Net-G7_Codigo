using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc;
using AnaPrevention.GeneralMasterData.Api.Common.API;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Services;
using AnaPrevention.GeneralMasterData.Api.Common.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Emails.EmailTags.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.Emails.EmailTags.Application.Services;
using AnaPrevention.GeneralMasterData.Api.Emails.EmailTags.Domain.Enum;
using System.Security.Claims;
using Asp.Versioning;

namespace AnaPrevention.GeneralMasterData.Api.Emails.EmailTags.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/email/tag")]
    public class EmailTagController(IHttpContextAccessor httpContextAccessor, IConfiguration configuration, EmailTagApplicationService emailTagApplicationService) : ApplicationController(httpContextAccessor, configuration)
    {
        private readonly EmailTagApplicationService _emailTagApplicationService = emailTagApplicationService;

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public Task<IActionResult> RegisterEmailTag(RegisterEmailTagRequest request)
        {
            try
            {
                var userId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value??"");
                Result<RegisterEmailTagResponse, Notification> result =  _emailTagApplicationService.RegisterEmailTag(request, userId);

                if (result.IsFailure)
                    return Task.FromResult(BadRequest(result.Error.GetErrors()));

                return Task.FromResult(Created(result.Value));
            }
            catch (Exception ex)
            {
                string msg = (ex.InnerException == null) ? "" : "| Inner Msg Error: " + ex.InnerException.Message.ToString(); ConsoleLog.WriteLine(ex.StackTrace + "| Msg Error:" + ex.Message.ToString() + msg);
                return Task.FromResult(ServerError());
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult EditEmailTag(Guid id, EditEmailTagRequest request)
        {
            try
            {
                request.Id = id;
                var userId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "");
                var emailTag = _emailTagApplicationService.GetById(request.Id);
                if (emailTag == null)
                {
                    return NotFound();
                }

                Notification notification = _emailTagApplicationService.ValidateEditEmailTagRequest(request);
                if (notification.HasErrors())
                    return BadRequest(notification.GetErrors());

                var response = _emailTagApplicationService.EditEmailTag(request, emailTag, userId);

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
        public IActionResult RemoveEmailTag(Guid id)
        {
            try
            {
                var userId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value??"");
                var emailTag = _emailTagApplicationService.GetById(id);

                if (emailTag == null)
                    return NotFound();

                RegisterEmailTagResponse response = _emailTagApplicationService.RemoveEmailTag(emailTag, userId);

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
        public IActionResult ActiveEmailTag(Guid id)
        {
            try
            {
                var userId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value??"");
                var emailTag = _emailTagApplicationService.GetById(id);

                if (emailTag == null)
                    return NotFound();


                RegisterEmailTagResponse response = _emailTagApplicationService.ActiveEmailTag(emailTag, userId);

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
                var emailTagDto = _emailTagApplicationService.GetById(id);

                if (emailTagDto == null)
                    return NotFound();

                return Ok(emailTagDto);
            }
            catch (Exception ex)
            {
                string msg = (ex.InnerException == null) ? "" : "| Inner Msg Error: " + ex.InnerException.Message.ToString(); ConsoleLog.WriteLine(ex.StackTrace + "| Msg Error:" + ex.Message.ToString() + msg);
                return ServerError();
            }
        }

        [HttpGet("getListAll")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetListAll(EmailTagTemplateType emailTagTemplateType)
        {
            try
            {
                var emailTagDtos = _emailTagApplicationService.GetListAll(emailTagTemplateType);

                if (emailTagDtos == null)
                    return NotFound();

                return Ok(emailTagDtos);
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
        public IActionResult GetList(
           int pageNumber = 1, int pageSize = 10, bool status = true, string descriptionSearch = "", string tagSearch = "")
        {
            try
            {
                var (emailTag, paginationMetadata) = _emailTagApplicationService.GetList(pageNumber, pageSize, status, descriptionSearch, tagSearch);

                Dictionary<string, object> result = new()
                {
                    { "data", emailTag },
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

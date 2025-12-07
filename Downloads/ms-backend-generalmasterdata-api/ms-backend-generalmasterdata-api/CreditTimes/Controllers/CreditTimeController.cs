using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc;
using AnaPrevention.GeneralMasterData.Api.Common.API;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Services;
using AnaPrevention.GeneralMasterData.Api.Common.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.CreditTimes.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.CreditTimes.Application.Services;
using AnaPrevention.GeneralMasterData.Api.CreditTimes.Domain.Entities;
using System.Security.Claims;

using Asp.Versioning;

namespace AnaPrevention.GeneralMasterData.Api.CreditTimes.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/creditTime")]
    public class CreditTimeController(IHttpContextAccessor httpContextAccessor, IConfiguration configuration, CreditTimeApplicationService creditTimeApplicationService) : ApplicationController(httpContextAccessor, configuration)
    {
        private readonly CreditTimeApplicationService _creditTimeApplicationService = creditTimeApplicationService;

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult RegisterCreditTime(RegisterCreditTimeRequest request)
        {
            try
            {
                var userId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value??"");
                Result<RegisterCreditTimeResponse, Notification> result = _creditTimeApplicationService.RegisterCreditTime(request, userId);

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
        public IActionResult EditCreditTime(Guid id, EditCreditTimeRequest request)
        {
            try
            {
                request.Id = id;
                var userId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value??"");
                var creditTime = _creditTimeApplicationService.GetById(request.Id);

                if (creditTime == null)
                {
                    return NotFound();
                }

                Notification notification = _creditTimeApplicationService.ValidateEditCreditTimeRequest(request);
                if (notification.HasErrors())
                    return BadRequest(notification.GetErrors());

                EditCreditTimeResponse response = _creditTimeApplicationService.EditCreditTime(request, creditTime, userId);

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
        public IActionResult RemoveCreditTime(Guid id)
        {
            try
            {
                var userId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value??"");
                var creditTime = _creditTimeApplicationService.GetById(id);

                if (creditTime == null)
                    return NotFound();

                EditCreditTimeResponse response = _creditTimeApplicationService.RemoveCreditTime(creditTime, userId);

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
        public IActionResult ActiveCreditTime(Guid id)
        {
            try
            {
                var userId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value??"");
                var creditTime = _creditTimeApplicationService.GetById(id);

                if (creditTime == null)
                    return NotFound();


                EditCreditTimeResponse response = _creditTimeApplicationService.ActiveCreditTime(creditTime, userId);

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
                CreditTime? creditTimeDto = _creditTimeApplicationService.GetDtoById(id);

                if (creditTimeDto == null)
                    return NotFound();

                return Ok(creditTimeDto);
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
                return Ok(_creditTimeApplicationService.GetListAll());
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
                var (creditTime, paginationMetadata) = _creditTimeApplicationService.GetList(pageNumber, pageSize, status, descriptionSearch, codeSearch);

                Dictionary<string, object> result = new()
                {
                    { "data", creditTime },
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


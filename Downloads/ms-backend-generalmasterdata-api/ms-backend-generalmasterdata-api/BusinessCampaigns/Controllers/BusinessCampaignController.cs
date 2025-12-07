using CSharpFunctionalExtensions;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using AnaPrevention.GeneralMasterData.Api.Common.API;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Services;
using AnaPrevention.GeneralMasterData.Api.Common.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.BusinessCampaigns.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.BusinessCampaigns.Application.Services;
using System.Security.Claims;
using AnaPrevention.GeneralMasterData.Api.Common.Domain.ValueObjects;

namespace AnaPrevention.GeneralMasterData.Api.BusinessCampaigns.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/business/campaigns")]
    public class BusinessCampaignController(IHttpContextAccessor httpContextAccessor, IConfiguration configuration, BusinessCampaignApplicationService businessCampaignApplicationService) : ApplicationController(httpContextAccessor, configuration)
    {
        private readonly BusinessCampaignApplicationService _businessCampaignApplicationService = businessCampaignApplicationService;



        //[Authorize(Roles = "admin")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult RegisterBusinessCampaign(RegisterBusinessCampaignRequest request)
        {
            try
            {
                var userId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "");
                Result<RegisterBusinessCampaignResponse, Notification> result = _businessCampaignApplicationService.RegisterBusinessCampaign(request, userId);


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
        public IActionResult EditBusinessCampaign(Guid id, EditBusinessCampaignRequest request)
        {
            try
            {
                request.Id = id;
                var userId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "");
                var businessCampaign = _businessCampaignApplicationService.GetById(request.Id);

                if (businessCampaign == null)
                {
                    return NotFound();
                }

                Notification notification = _businessCampaignApplicationService.ValidateEditBusinessCampaignRequest(request);

                if (notification.HasErrors())
                    return BadRequest(notification.GetErrors());

                Result<Date, Notification> resultDateStart = Date.Create(request.DateStart);
                Result<Date, Notification> resultDateFinish = Date.Create(request.DateFinish);

                if (resultDateStart.IsFailure && resultDateStart.Error.HasErrors())
                    return BadRequest(resultDateStart.Error.GetErrors());

                if (resultDateFinish.IsFailure && resultDateFinish.Error.HasErrors())
                    return BadRequest(resultDateFinish.Error.GetErrors());


                EditBusinessCampaignResponse response = _businessCampaignApplicationService.EditBusinessCampaign(request, businessCampaign, userId);

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
        public IActionResult RemoveBusinessCampaign(Guid id)
        {
            try
            {
                var userId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "");
                var businessCampaign = _businessCampaignApplicationService.GetById(id);

                if (businessCampaign == null)
                    return NotFound();

                EditBusinessCampaignResponse response = _businessCampaignApplicationService.RemoveBusinessCampaign(businessCampaign, userId);

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
        public IActionResult ActiveBusinessCampaign(Guid id)
        {
            try
            {
                var userId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "");
                var businessCampaign = _businessCampaignApplicationService.GetById(id);

                if (businessCampaign == null)
                    return NotFound();


                EditBusinessCampaignResponse response = _businessCampaignApplicationService.ActiveBusinessCampaign(businessCampaign, userId);

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
                BusinessCampaignDto? businessCampaignDto = _businessCampaignApplicationService.GetDtoById(id);

                if (businessCampaignDto == null)
                    return NotFound();

                return Ok(businessCampaignDto);
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
        public IActionResult GetListAll(Guid businessId)
        {
            try
            {
                return Ok(_businessCampaignApplicationService.GetListAll(businessId));
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
        public IActionResult GetList(Guid businessId, int pageNumber = 1, int pageSize = 10, bool status = true, string? descriptionSearch = "")
        {
            try
            {
                var (businessCampaign, paginationMetadata) = _businessCampaignApplicationService.GetList(pageNumber, pageSize, businessId, status, descriptionSearch);

                Dictionary<string, object> result = new();
                result.Add("data", businessCampaign);
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

using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc;
using AnaPrevention.GeneralMasterData.Api.Common.API;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Services;
using AnaPrevention.GeneralMasterData.Api.Common.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Equipments.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.Equipments.Application.Services;
using AnaPrevention.GeneralMasterData.Api.MedicalAreas.Domain.Enums;
using System.Security.Claims;
using Asp.Versioning;

namespace AnaPrevention.GeneralMasterData.Api.Equipments.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/equipment")]
    public class EquipmentController(IHttpContextAccessor httpContextAccessor, IConfiguration configuration, EquipmentApplicationService equipmentApplicationService) : ApplicationController(httpContextAccessor, configuration)
    {
        private readonly EquipmentApplicationService _equipmentApplicationService = equipmentApplicationService;

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RegisterEquipment(RegisterEquipmentRequest request)
        {
            try
            {
                var tokenCompanyId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == "companyId")?.Value ?? "");
                var userId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "");
                Result<RegisterEquipmentResponse, Notification> result = await _equipmentApplicationService.RegisterEquipment(request, userId, tokenCompanyId);

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
        public async Task<IActionResult> EditEquipment(Guid id, EditEquipmentRequest request)
        {
            try
            {
                request.Id = id;
                var userId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "");
                var equipment = _equipmentApplicationService.GetById(request.Id);

                if (equipment == null)
                {
                    return NotFound();
                }

                Result<EditEquipmentResponse, Notification> result = await _equipmentApplicationService.EditEquipment(request, equipment, userId);

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
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult RemoveEquipment(Guid id)
        {
            try
            {
                var userId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "");
                var equipment = _equipmentApplicationService.GetById(id);

                if (equipment == null)
                    return NotFound();

                EditEquipmentResponse response = _equipmentApplicationService.RemoveEquipment(equipment, userId);

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
        public IActionResult ActiveEquipment(Guid id)
        {
            try
            {
                var userId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "");
                var equipment = _equipmentApplicationService.GetById(id);

                if (equipment == null)
                    return NotFound();


                EditEquipmentResponse response = _equipmentApplicationService.ActiveEquipment(equipment, userId);

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
                var equipment = _equipmentApplicationService.GetDtoById(id);

                if (equipment == null)
                    return NotFound();

                return Ok(equipment);
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
                return Ok(_equipmentApplicationService.GetListAll(tokenCompanyId));
            }
            catch (Exception ex)
            {
                string msg = (ex.InnerException == null) ? "" : "| Inner Msg Error: " + ex.InnerException.Message.ToString(); ConsoleLog.WriteLine(ex.StackTrace + "| Msg Error:" + ex.Message.ToString() + msg);
                return ServerError();
            }
        }

       
        [HttpGet("getListBySubsidiaryMedicalArea")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetListAudiometry(Guid subsidiaryId, MedicalAreaType medicalAreaType)
        {
            try
            {
                var list = _equipmentApplicationService.GetListByMedicalAreaTypeAndSubsidiaryId(subsidiaryId, medicalAreaType);
                return Ok(list);
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
        public IActionResult GetList(int pageNumber = 1, int pageSize = 10, bool status = true, string? descriptionSearch = "")
        {
            try
            {
                var tokenCompanyId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == "companyId")?.Value ?? "");
                var (equipment, paginationMetadata) = _equipmentApplicationService.GetList(pageNumber, pageSize, status, descriptionSearch, tokenCompanyId);

                Dictionary<string, object> result = new()
                {
                    { "data", equipment },
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

        [HttpGet("getListBySubsidiary")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetListBySubsidiary(Guid SubsidiaryId, int pageNumber = 1, int pageSize = 10, bool status = true)
        {
            try
            {
                var tokenCompanyId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == "companyId")?.Value ?? "");
                var (equipment, paginationMetadata) = _equipmentApplicationService.GetListBySubsidiary(pageNumber, pageSize, status, tokenCompanyId, SubsidiaryId);

                Dictionary<string, object> result = new()
                {
                    { "data", equipment },
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

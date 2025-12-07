using CSharpFunctionalExtensions;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using AnaPrevention.GeneralMasterData.Api.Common.API;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Services;
using AnaPrevention.GeneralMasterData.Api.Common.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Doctors.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.Doctors.Application.Services;
using System.Security.Claims;

namespace AnaPrevention.GeneralMasterData.Api.Doctors.Controllers
{
    [ApiController]
    
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/doctor")]
    public class DoctorController(IHttpContextAccessor httpContextAccessor, IConfiguration configuration, DoctorApplicationService doctorApplicationService) : ApplicationController(httpContextAccessor, configuration)
    {
        private readonly DoctorApplicationService _doctorApplicationService = doctorApplicationService;

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RegisterDoctors(RegisterDoctorRequest request)
        {
            try
            {
                var userId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "");
                Result<RegisterDoctorResponse, Notification> result = await _doctorApplicationService.RegisterDoctors(request, userId);

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
        public async Task<IActionResult> EditDoctors(Guid id, EditDoctorRequest request)
        {
            try
            {
                request.Id = id;
                var userId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "");
                var doctors = _doctorApplicationService.GetById(request.Id);

                if (doctors == null)
                    return NotFound();

                Notification notification = _doctorApplicationService.ValidateEditDoctorsRequest(request);
                if (notification.HasErrors())
                    return BadRequest(notification.GetErrors());

                Result<EditDoctorResponse, Notification> result = await _doctorApplicationService.EditDoctors(request, doctors, userId);

                if (result.IsFailure)
                    return BadRequest(result.Error.GetErrors());

                return Ok(result.Value);
            }
            catch (Exception ex)
            {
                string msg = (ex.InnerException == null) ? "" : "| Inner Msg Error: " + ex.InnerException.Message.ToString(); ConsoleLog.WriteLine(ex.StackTrace + "| Msg Error:" + ex.Message.ToString() + msg);
                return ServerError();
            }
        }

        [HttpPatch("registerDoctorMedicalArea/{id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult RegisterDoctorMedicalArea(Guid id, RegisterDoctorMedicalAreaRequest request)
        {
            try
            {
                request.Id = id;
                var userId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "");

                var doctor = _doctorApplicationService.GetById(request.Id);

                if (doctor == null)
                    return NotFound();

                Result<EditDoctorResponse, Notification> result = _doctorApplicationService.RegisterDoctorMedicalArea(doctor, request.ListMedicalAreaIds, userId);

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
        public IActionResult RemoveDoctors(Guid id)
        {
            try
            {
                var userId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "");
                var doctors = _doctorApplicationService.GetById(id);

                if (doctors == null)
                    return NotFound();

                EditDoctorResponse response = _doctorApplicationService.RemoveDoctors(doctors, userId);

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
        public IActionResult ActiveDoctors(Guid id)
        {
            try
            {
                var userId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "");
                var doctors = _doctorApplicationService.GetById(id);

                if (doctors == null)
                    return NotFound();


                EditDoctorResponse response = _doctorApplicationService.ActiveDoctors(doctors, userId);

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
                DoctorDto? doctorsDto = _doctorApplicationService.GetDtoById(id);

                if (doctorsDto == null)
                    return NotFound();

                return Ok(doctorsDto);
            }
            catch (Exception ex)
            {
                string msg = (ex.InnerException == null) ? "" : "| Inner Msg Error: " + ex.InnerException.Message.ToString(); ConsoleLog.WriteLine(ex.StackTrace + "| Msg Error:" + ex.Message.ToString() + msg);
                return ServerError();
            }
        }

        [HttpGet("GetListAll")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetListAll(string? fullName = "")
        {
            try
            {
                return Ok(_doctorApplicationService.GetListAll(fullName??""));
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
            int pageNumber = 1, int pageSize = 10, bool status = true, string? displayNameSearch = "", string? codeSearch = "", string? documentNumberSearch = "", string? SpecialtySearch = "")
        {
            try
            {
                var (doctors, paginationMetadata) = _doctorApplicationService.GetList(pageNumber, pageSize, status, displayNameSearch, codeSearch, documentNumberSearch, SpecialtySearch);

                Dictionary<string, object> result = new();
                result.Add("data", doctors);
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

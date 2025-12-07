using CSharpFunctionalExtensions;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using AnaPrevention.GeneralMasterData.Api.Common.API;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Services;
using AnaPrevention.GeneralMasterData.Api.Common.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Subsidiaries.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.Subsidiaries.Application.Services;
using System.Security.Claims;

namespace AnaPrevention.GeneralMasterData.Api.Subsidiaries.Controllers
{
    [ApiController]
    
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/Subsidiary")]
    public class SubsidiaryController(IHttpContextAccessor httpContextAccessor, IConfiguration configuration, SubsidiaryApplicationService subsidiaryApplicationService) : ApplicationController(httpContextAccessor, configuration)
    {
        private readonly SubsidiaryApplicationService _subsidiaryApplicationService = subsidiaryApplicationService;

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RegisterSubsidiary(RegisterSubsidiaryRequest request)
        {
            try
            {
                

                var tokenCompanyId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == "companyId")?.Value ?? "");
                var userId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "");
                Result<RegisterSubsidiaryResponse, Notification> result = await _subsidiaryApplicationService.RegisterSubsidiary(request, tokenCompanyId, userId);

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

        [HttpPost("distributionList")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult RegisterDistributionList(RegisterDistributionListEmailRequest request)
        {
            try
            {
                
                var userId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "");
                var subsidiary = _subsidiaryApplicationService.GetById(request.SubsidiaryId);

                if (subsidiary == null)
                {
                    return NotFound();
                }

                var result = _subsidiaryApplicationService.RegisterDistributionListEmail(request, subsidiary, userId);


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
        public async Task<IActionResult> EditSubsidiary(Guid id, EditSubsidiaryRequest request)
        {
            try
            {
                request.Id = id;
                var tokenCompanyId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == "companyId")?.Value ?? "");
                var userId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "");
                var subsidiary = _subsidiaryApplicationService.GetById(request.Id);

                if (subsidiary == null)
                {
                    return NotFound();
                }

                Notification notification = _subsidiaryApplicationService.ValidateEditSubsidiaryRequest(request, tokenCompanyId);
                if (notification.HasErrors())
                    return BadRequest(notification.GetErrors());

                EditSubsidiaryResponse response = await _subsidiaryApplicationService.EditSubsidiary(request, subsidiary, userId, tokenCompanyId);

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
        public IActionResult RemoveSubsidiary(Guid id)
        {
            try
            {
              
                var userId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "");
                var subsidiary = _subsidiaryApplicationService.GetById(id);

                if (subsidiary == null)
                    return NotFound();

                var tokenCompanyId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == "companyId")?.Value ?? "");
                if (subsidiary.CompanyId != tokenCompanyId)
                    return NotFound();

                EditSubsidiaryResponse response = _subsidiaryApplicationService.RemoveSubsidiary(subsidiary, userId);

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
        public IActionResult ActiveSubsidiary(Guid id)
        {
            try
            {
               
                var userId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "");
                var subsidiary = _subsidiaryApplicationService.GetById(id);

                if (subsidiary == null)
                    return NotFound();

                var tokenCompanyId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == "companyId")?.Value ?? "");
                if (subsidiary.CompanyId != tokenCompanyId)
                    return NotFound();

                EditSubsidiaryResponse response = _subsidiaryApplicationService.ActiveSubsidiary(subsidiary, userId);

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
                var tokenCompanyId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == "companyId")?.Value ?? "");
                SubsidiaryDto? subsidiaryDto = _subsidiaryApplicationService.GetDtoById(id, tokenCompanyId);

                if (subsidiaryDto == null)
                    return NotFound();

                return Ok(subsidiaryDto);
            }
            catch (Exception ex)
            {
                string msg = (ex.InnerException == null) ? "" : "| Inner Msg Error: " + ex.InnerException.Message.ToString(); ConsoleLog.WriteLine(ex.StackTrace + "| Msg Error:" + ex.Message.ToString() + msg);
                return ServerError();
            }
        }

        [HttpGet("distributionList/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetByDistributionListId(Guid id)
        {
            try
            {
                var userId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "");
                var subsidiary = _subsidiaryApplicationService.GetById(id);

                if (subsidiary == null)
                {
                    return NotFound();
                }


                return Ok(_subsidiaryApplicationService.GetDistributionListEmail(subsidiary));
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
                return Ok(_subsidiaryApplicationService.GetListAll(tokenCompanyId));
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
        public IActionResult GetList(int pageNumber = 1, int pageSize = 10, bool status = true, string? descriptionSearch = "", string? codeSearch = "", string? serviceTypeSearch = "", string? subsidiaryTypeSearch = "")
        {
            try
            {

                
                var tokenCompanyId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == "companyId")?.Value ?? "");
                var (subsidiary, paginationMetadata) = _subsidiaryApplicationService.GetList(pageNumber, pageSize, tokenCompanyId, status, descriptionSearch, codeSearch, serviceTypeSearch, subsidiaryTypeSearch);

                Dictionary<string, object> result = new()
                {
                    { "data", subsidiary },
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

        //[HttpGet("getListQuota")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //public IActionResult GetListQuota(Guid businessId, string? dateStartSearch = "")
        //{
        //    try
        //    {

        //        if (!DateTime.TryParse(dateStartSearch, out DateTime dateStart))
        //        {
        //            return BadRequest(OccupationalHealthStatic.MsgErrorConvertDateStart);
        //        }
        //        var list = _subsidiaryApplicationService.GetMinDtoByQuota(businessId, dateStart);

        //        return Ok(list);
        //    }
        //    catch (Exception ex)
        //    {
        //        string msg = (ex.InnerException == null) ? "" : "| Inner Msg Error: " + ex.InnerException.Message.ToString(); ConsoleLog.WriteLine(ex.StackTrace + "| Msg Error:" + ex.Message.ToString() + msg);
        //        return ServerError();
        //    }
        //}

      
    }
}
using CSharpFunctionalExtensions;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using AnaPrevention.GeneralMasterData.Api.Common.API;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Services;
using AnaPrevention.GeneralMasterData.Api.Common.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.CostCenters.Application.Services;
using AnaPrevention.GeneralMasterData.Api.Dimensions.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.Dimensions.Application.Services;
using AnaPrevention.GeneralMasterData.Api.Dimensions.Domain.Entities;
using System.Security.Claims;

namespace AnaPrevention.GeneralMasterData.Api.Dimensions.Controllers
{
    [ApiController]
    
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/dimension")]
    public class DimensionController(IHttpContextAccessor httpContextAccessor, IConfiguration configuration, DimensionApplicationService dimensionApplicationService, CostCenterApplicationService costCenterApplicationService) : ApplicationController(httpContextAccessor, configuration)
    {
        private readonly DimensionApplicationService _dimensionApplicationService = dimensionApplicationService;
        private readonly CostCenterApplicationService _costCenterApplicationService = costCenterApplicationService;


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult RegisterDimension(RegisterDimensionRequest request)
        {
            try
            {
                var tokenCompanyId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == "companyId")?.Value ?? "");
                var userId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "");
                Result<RegisterDimensionResponse, Notification> result = _dimensionApplicationService.RegisterDimension(request, tokenCompanyId, userId);

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
        public IActionResult EditDimension(Guid id, EditDimensionRequest request)
        {
            try
            {
                request.Id = id;
                var userId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "");
                var dimension = _dimensionApplicationService.GetById(request.Id);

                if (dimension == null)
                {
                    return NotFound();
                }

                var tokenCompanyId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == "companyId")?.Value ?? "");
                if (dimension.CompanyId != tokenCompanyId)
                    return NotFound();

                Notification notification = _dimensionApplicationService.ValidateEditDimensionRequest(request, tokenCompanyId);
                if (notification.HasErrors())
                    return BadRequest(notification.GetErrors());

                List<CostCenter> costCenterEditList = new List<CostCenter>();
                List<EditCostCenterRequest> costCenterEditRequestList = new List<EditCostCenterRequest>();
                List<RegisterCostCenterRequest> costCenterInsertRequestList = new List<RegisterCostCenterRequest>();
                foreach (EditCostCenterRequest costCenterRequest in request.costCenters)
                {
                    if (costCenterRequest.Id != null)
                    {
                        CostCenter? costCenter = _costCenterApplicationService.GetById((Guid)costCenterRequest.Id);
                        if (costCenter == null || costCenter.DimensionId != dimension.Id)
                        {
                            return NotFound();
                        }
                        Notification notificationCostCenter = _costCenterApplicationService.ValidateEditCostCenterRequest(costCenterRequest);
                        if (notificationCostCenter.HasErrors())
                            return BadRequest(notificationCostCenter.GetErrors());
                        costCenterEditList.Add(costCenter);
                        costCenterEditRequestList.Add(costCenterRequest);
                    }
                    else
                    {
                        RegisterCostCenterRequest registerCostCenterRequest = _costCenterApplicationService.ConvertToRegisterValidator(costCenterRequest);
                        Notification notificationCostCenter = _costCenterApplicationService.ValidateRegisterCostCenterRequest(registerCostCenterRequest);
                        if (notificationCostCenter.HasErrors())
                            return BadRequest(notificationCostCenter.GetErrors());

                        costCenterInsertRequestList.Add(registerCostCenterRequest);
                    }
                }



                EditDimensionResponse response = _dimensionApplicationService.EditDimension(request, dimension, costCenterEditList, costCenterEditRequestList, costCenterInsertRequestList, userId);

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
        public IActionResult ActiveDimension(Guid id)
        {
            try
            {
                var dimension = _dimensionApplicationService.GetById(id);
                var userId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "");
                if (dimension == null)
                    return NotFound();

                var tokenCompanyId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == "companyId")?.Value ?? "");
                if (dimension.CompanyId != tokenCompanyId)
                    return NotFound();


                EditDimensionResponse response = _dimensionApplicationService.ActiveDimension(dimension, userId);

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
        public IActionResult RemoveDimension(Guid id)
        {
            try
            {
                var dimension = _dimensionApplicationService.GetById(id);
                var userId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "");
                if (dimension == null)
                    return NotFound();

                var tokenCompanyId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == "companyId")?.Value ?? "");
                if (dimension.CompanyId != tokenCompanyId)
                    return NotFound();

                EditDimensionResponse response = _dimensionApplicationService.RemoveDimension(dimension, userId);

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
                var tokenCompanyId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == "companyId")?.Value ?? "");

                DimensionDto? dimensionDto = _dimensionApplicationService.GetDtoById(id, tokenCompanyId);

                if (dimensionDto == null)
                    return NotFound();

                return Ok(dimensionDto);
            }
            catch (Exception ex)
            {
                string msg = (ex.InnerException == null) ? "" : "| Inner Msg Error: " + ex.InnerException.Message.ToString(); ConsoleLog.WriteLine(ex.StackTrace + "| Msg Error:" + ex.Message.ToString() + msg);
                return ServerError();
            }
        }

        [HttpGet("{id}/getcostcenters")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetCostCenterByDimesionId(Guid id)
        {
            try
            {
                var tokenCompanyId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == "companyId")?.Value ?? "");

                DimensionDto? dimensionDto = _dimensionApplicationService.GetDtoById(id, tokenCompanyId);

                if (dimensionDto == null)
                    return NotFound();

                List<CostCenter>? costCenters = _costCenterApplicationService.GetDtoByDimensionId(dimensionDto.Id);

                return Ok(costCenters);
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
                var (dimension, paginationMetadata) = _dimensionApplicationService.GetList(pageNumber, pageSize, tokenCompanyId, status, descriptionSearch??"");

                Dictionary<string, object> result = new()
                {
                    { "data", dimension },
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

       
        [HttpGet("getListAll")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetListAll()
        {
            try
            {
                var tokenCompanyId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == "companyId")?.Value ?? "");
                return Ok(_dimensionApplicationService.GetListAll(tokenCompanyId));
            }
            catch (Exception ex)
            {
                string msg = (ex.InnerException == null) ? "" : "| Inner Msg Error: " + ex.InnerException.Message.ToString(); ConsoleLog.WriteLine(ex.StackTrace + "| Msg Error:" + ex.Message.ToString() + msg);
                return ServerError();
            }
        }


       
       
    }
}

using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc;
using AnaPrevention.GeneralMasterData.Api.Common.API;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Services;
using AnaPrevention.GeneralMasterData.Api.Emails.EmailContents.Applications.Dtos;
using AnaPrevention.GeneralMasterData.Api.Emails.EmailContents.Applications.Services;
using System.Security.Claims;
using Asp.Versioning;
using AnaPrevention.GeneralMasterData.Api.Emails.EmailTags.Domain.Enum;
using AnaPrevention.GeneralMasterData.Api.Common.Tools.Class;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Common.Helper.Mail;

namespace AnaPrevention.GeneralMasterData.Api.Emails.EmailContents.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/email/content")]
    public class EmailContentController(IHttpContextAccessor httpContextAccessor, IConfiguration configuration, EmailContentApplicationServices emailContentApplicationServices, ApiApplicationService apiApplicationService, EmailService emailService) : ApplicationController(httpContextAccessor, configuration)
    {
        private readonly EmailContentApplicationServices _emailContentApplicationServices = emailContentApplicationServices;
        private readonly ApiApplicationService _apiApplicationService = apiApplicationService;
        private readonly EmailService _emailService = emailService;

        [HttpGet("getEmailTagTemplateType")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetEmailTagTemplateType()
        {
            try
            {
                return Ok(_emailContentApplicationServices.GetEmailTagTemplateType());
            }
            catch (Exception ex)
            {
                string msg = ex.InnerException == null ? "" : "| Inner Msg Error: " + ex.InnerException.Message.ToString(); ConsoleLog.WriteLine(ex.StackTrace + "| Msg Error:" + ex.Message.ToString() + msg);
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
                var emailContentDto = _emailContentApplicationServices.GetDtoById(id);

                if (emailContentDto == null)
                    return NotFound();

                return Ok(emailContentDto);
            }
            catch (Exception ex)
            {
                string msg = ex.InnerException == null ? "" : "| Inner Msg Error: " + ex.InnerException.Message.ToString(); ConsoleLog.WriteLine(ex.StackTrace + "| Msg Error:" + ex.Message.ToString() + msg);
                return ServerError();
            }
        }

        [HttpGet("getList")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetList(
           int pageNumber = 1, int pageSize = 10, bool status = true, string? fromAddress = "", string? toAddress = "", EmailTagTemplateType? emailTagTemplateType = null, string? dateStartSearch = null, string? dateFinishSearch = null, string? subject = "")
        {
            try
            {
                if (string.IsNullOrEmpty(dateStartSearch))
                {
                    dateStartSearch = DateTimePersonalized.NowPeru.ToString();
                }
                if (string.IsNullOrEmpty(dateFinishSearch))
                {
                    dateFinishSearch = DateTimePersonalized.NowPeru.ToString();
                }

                if (!DateTime.TryParse(dateStartSearch, out DateTime dateStart))
                    return BadRequest(CommonStatic.MsgErrorConvertDateStart);

                if (!DateTime.TryParse(dateFinishSearch, out DateTime dateFinish))
                    return BadRequest(CommonStatic.MsgErrorConvertDateFinish);


                var (emailContent, paginationMetadata) = _emailContentApplicationServices.GetList(pageNumber, pageSize, status, fromAddress ?? "", toAddress ?? "", emailTagTemplateType, dateStart, dateFinish, subject);

                Dictionary<string, object> result = new()
                {
                    { "data", emailContent },
                    { "pagination", paginationMetadata }
                };
                return Ok(result);
            }
            catch (Exception ex)
            {
                string msg = ex.InnerException == null ? "" : "| Inner Msg Error: " + ex.InnerException.Message.ToString(); ConsoleLog.WriteLine(ex.StackTrace + "| Msg Error:" + ex.Message.ToString() + msg);
                return ServerError();
            }
        }

        [HttpPost("resend/{id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SendEmailContent(Guid id)
        {
            try
            {
                var userId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "");


                var emailContentDto = _emailContentApplicationServices.GetDtoById(id);

                if (emailContentDto?.ReferenceId == null)
                    return NotFound();


                if (emailContentDto.EmailTagTemplateType == EmailTagTemplateType.OCCUPATIONAL_ORDER)
                {
                    var result = await _apiApplicationService.GetOccupationalMasterData<EmailMessage>($"occupation/order/send/{(Guid)emailContentDto.ReferenceId}");
                    if(result != null)
                        await _emailService.SendAsync(result, userId, result.PersonId);

                    return Ok(true);

                }
                else if (emailContentDto.EmailTagTemplateType == EmailTagTemplateType.OCCUPATIONAL_APPOINTMENT)
                {
                    var result = await _apiApplicationService.GetOccupationalMasterData<EmailMessage>($"occupation/appointments/send/{(Guid)emailContentDto.ReferenceId}");
                    if (result != null)
                        await _emailService.SendAsync(result, userId, result.PersonId);
                    return Ok(true);
                }


                return Ok(false);
            }
            catch (Exception ex)
            {
                string msg = (ex.InnerException == null) ? "" : "| Inner Msg Error: " + ex.InnerException.Message.ToString(); ConsoleLog.WriteLine(ex.StackTrace + "| Msg Error:" + ex.Message.ToString() + msg);
                return ServerError();
            }
        }

        [HttpPost("resend/massive/")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SendEmailContent(MassiveRequestIds request)
        {
            try
            {
                var userId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "");
                List<bool> results = [];
                if (request.Ids != null)
                {
                    foreach (var id in request.Ids)
                    {
                        var emailContentDto = _emailContentApplicationServices.GetDtoById(id);
                        if (emailContentDto?.ReferenceId != null)
                        {
                            if (emailContentDto.EmailTagTemplateType == EmailTagTemplateType.OCCUPATIONAL_ORDER)
                            {
                                var result = await _apiApplicationService.GetOccupationalMasterData<EmailMessage>($"occupation/order/send/{(Guid)emailContentDto.ReferenceId}");
                                if (result != null)
                                    await _emailService.SendAsync(result, userId, result.PersonId);
                                results.Add(true);

                            }
                            else if (emailContentDto.EmailTagTemplateType == EmailTagTemplateType.OCCUPATIONAL_APPOINTMENT)
                            {
                                var result = await _apiApplicationService.GetOccupationalMasterData<EmailMessage>($"occupation/appointments/send/{(Guid)emailContentDto.ReferenceId}");
                                if (result != null)
                                    await _emailService.SendAsync(result, userId, result.PersonId);
                                return Ok(true);
                            }
                            else
                            {
                                results.Add(true);
                            }
                        }
                        else
                        {
                            results.Add(false);
                        }
                    }
                }
                return Ok(results);
            }
            catch (Exception ex)
            {
                string msg = (ex.InnerException == null) ? "" : "| Inner Msg Error: " + ex.InnerException.Message.ToString(); ConsoleLog.WriteLine(ex.StackTrace + "| Msg Error:" + ex.Message.ToString() + msg);
                return ServerError();
            }
        }
    }
}

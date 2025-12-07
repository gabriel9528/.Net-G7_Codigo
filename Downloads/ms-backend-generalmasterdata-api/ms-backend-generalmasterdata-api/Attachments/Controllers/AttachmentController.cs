using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using AnaPrevention.GeneralMasterData.Api.Attachments.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.Attachments.Application.Services;
using AnaPrevention.GeneralMasterData.Api.Common.API;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Services;
using AnaPrevention.GeneralMasterData.Api.Common.Domain.Entities;
using System.Security.Claims;
using AnaPrevention.GeneralMasterData.Api.Attachments.Domain.Enums;
using AnaPrevention.GeneralMasterData.Api.Attachments.Application.Dtos.S3;

namespace AnaPrevention.GeneralMasterData.Api.Attachments.Controllers
{

    [ApiController]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/attachment")]
    public class AttachmentController(IHttpContextAccessor httpContextAccessor, IConfiguration configuration, AttachmentApplicationService attachmentApplicationService, S3AmazonService s3bucketService) : ApplicationController(httpContextAccessor, configuration)
    {
        private readonly AttachmentApplicationService _attachmentApplicationService = attachmentApplicationService;
        private readonly S3AmazonService _s3bucketService = s3bucketService;

        [HttpPost("onlyUpload")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateOnlyUpload(RegisterUploadFilePostRequest request)
        {
            try
            {
                var userId = Guid.Empty;
                if (!Guid.TryParse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value, out userId))
                {
                    if (Request.Headers.TryGetValue("userId", out var userIdToken))
                    {
                        _ = !Guid.TryParse(userIdToken, out userId);
                    }
                }

                var response = await _attachmentApplicationService.RegisterAttachmentOnlyUpload(request, userId);

                if (response.IsFailure)
                {
                    return BadRequest(response.Error.GetErrors());
                }

                return Created(response.Value);
            }
            catch (Exception ex)
            {
                string msg = (ex.InnerException == null) ? "" : "| Inner Msg Error: " + ex.InnerException.Message.ToString(); ConsoleLog.WriteLine(ex.StackTrace + "| Msg Error:" + ex.Message.ToString() + msg);
                return ServerError();
            }
        }
        [HttpPost("upload")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateAttachmentUpload(RegisterAttachmentUploadPostRequest request)
        {
            try
            {
                var userId = Guid.Empty;
                if(!Guid.TryParse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value, out userId))
                {
                    if (Request.Headers.TryGetValue("userId", out var userIdToken))
                    {
                        _ = !Guid.TryParse(userIdToken, out userId);
                    }
                }
                var response = await _attachmentApplicationService.RegisterAttachmentUpload(request, userId);

                if(response.IsFailure)
                {
                    return BadRequest(response.Error.GetErrors());
                }

                return Created(response.Value);
            }
            catch (Exception ex)
            {
                string msg = (ex.InnerException == null) ? "" : "| Inner Msg Error: " + ex.InnerException.Message.ToString(); ConsoleLog.WriteLine(ex.StackTrace + "| Msg Error:" + ex.Message.ToString() + msg);
                return ServerError();
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateAttachment(RegisterAttachmenPostRequest request)
        {
            try
            {
                var userId = Guid.Empty;
                if (!Guid.TryParse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value, out userId))
                {
                    if (Request.Headers.TryGetValue("userId", out var userIdToken))
                    {
                        _ = !Guid.TryParse(userIdToken, out userId);
                    }
                }

                var response = _attachmentApplicationService.RegisterAttachment(request, userId);

                if (response.IsFailure)
                {
                    return BadRequest(response.Error.GetErrors());
                }

                return Created(response.Value);
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
        public IActionResult EditAttachment(Guid id, EditAttachmentRequest1 request)
        {
            try
            {
                request.Id = id;
                var userId = Guid.Empty;
                _ = Guid.TryParse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value, out userId);
               
                var attachment = _attachmentApplicationService.GetById(request.Id);

                if (attachment == null)
                {
                    return NotFound();
                }

                Notification notification = _attachmentApplicationService.ValidateEditAttachmentRequest(request);
                if (notification.HasErrors())
                    return BadRequest(notification.GetErrors());

                EditAttachmentResponse response = _attachmentApplicationService.EditAttachment(request, attachment, userId);

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
        public IActionResult RemoveAttachment(Guid id)
        {
            try
            {
                var userId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "");
                var attachment = _attachmentApplicationService.GetById(id);

                if (attachment == null)
                    return NotFound();

                EditAttachmentResponse response = _attachmentApplicationService.RemoveAttachment(attachment, userId);

                return Ok(response);
            }
            catch (Exception ex)
            {
                string msg = (ex.InnerException == null) ? "" : "| Inner Msg Error: " + ex.InnerException.Message.ToString(); ConsoleLog.WriteLine(ex.StackTrace + "| Msg Error:" + ex.Message.ToString() + msg);
                return ServerError();
            }
        }

        [HttpPost("removeAttachments")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult RemoveAttachment(AttachmentIdsRequest request)
        {
            try
            {
                var userId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "");
                var attachments = _attachmentApplicationService.GetByIds(request.Ids);

                if (attachments == null)
                    return NotFound();

                var response = _attachmentApplicationService.RemoveAttachments(attachments, userId);

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
        public IActionResult ActiveAttachment(Guid id)
        {
            try
            {
                var userId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "");
                var attachment = _attachmentApplicationService.GetById(id);

                if (attachment == null)
                    return NotFound();


                EditAttachmentResponse response = _attachmentApplicationService.ActiveAttachment(attachment, userId);

                return Ok(response);
            }
            catch (Exception ex)
            {
                string msg = (ex.InnerException == null) ? "" : "| Inner Msg Error: " + ex.InnerException.Message.ToString(); ConsoleLog.WriteLine(ex.StackTrace + "| Msg Error:" + ex.Message.ToString() + msg);
                return ServerError();
            }
        }

        [HttpGet("get/{entityId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetByEntityId(Guid entityId, EntityType? entityType = null)
        {
            try
            {
                var result = _attachmentApplicationService.GetByEntityId(entityId, entityType);


                return Ok(result);
            }
            catch (Exception ex)
            {
                string msg = (ex.InnerException == null) ? "" : "| Inner Msg Error: " + ex.InnerException.Message.ToString(); ConsoleLog.WriteLine(ex.StackTrace + "| Msg Error:" + ex.Message.ToString() + msg);
                return ServerError();
            }
        }


        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetById(Guid id)
        {
            try
            {
                var result = _attachmentApplicationService.GetById(id);


                return Ok(result);
            }
            catch (Exception ex)
            {
                string msg = (ex.InnerException == null) ? "" : "| Inner Msg Error: " + ex.InnerException.Message.ToString(); ConsoleLog.WriteLine(ex.StackTrace + "| Msg Error:" + ex.Message.ToString() + msg);
                return ServerError();
            }
        }

        [HttpPost("S3-Download")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DownloadObjectS3(S3DownloadFileRequest request)
        {
            try
            {
                var result = await _s3bucketService.DownloadObjectS3(request);
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

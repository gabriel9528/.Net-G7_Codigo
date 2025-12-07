using CSharpFunctionalExtensions;
using AnaPrevention.GeneralMasterData.Api.Attachments.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.Attachments.Application.Validators;
using AnaPrevention.GeneralMasterData.Api.Attachments.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Attachments.Domain.Enums;
using AnaPrevention.GeneralMasterData.Api.Attachments.Infrastructure.Repositories;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Services;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Common.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Common.Tools.Class;
using AnaPrevention.GeneralMasterData.Api.Common.Infrastructure.EF;
using AnaPrevention.GeneralMasterData.Api.Attachments.Application.Dtos.S3;

namespace AnaPrevention.GeneralMasterData.Api.Attachments.Application.Services
{
    public class AttachmentApplicationService(AttachmentRepository attachmentRepository, AnaPreventionContext context, EditAttachmentValidator editAttachmentValidator, S3AmazonService s3bucketService)
    {
        private readonly AttachmentRepository _attachmentRepository = attachmentRepository;
        private readonly AnaPreventionContext _context = context;
        private readonly EditAttachmentValidator _editAttachmentValidator = editAttachmentValidator;
        private readonly S3AmazonService _s3bucketService = s3bucketService;

        public Result<string, Notification> RegisterAttachment(RegisterAttachmentRequest request, Guid entityId, EntityType entityType, Guid userId)
        {
            if (string.IsNullOrEmpty(request.Directory))
            {
                request.Directory = "attachments";
            }

            string url = $"{request.Directory}/{Guid.NewGuid()}";
            long fileSize = request.FileSize;
            Attachment attachment = new(request.Name, url, entityId, entityType, FileType.Other, fileSize, DateTimePersonalized.NowPeru);
            _attachmentRepository.Save(attachment);
            _context.SaveChanges(userId);
            return url;
        }
        public Result<List<string>, Notification> RegisterAttachment(RegisterAttachmenPostRequest requestMain, Guid userId)
        {
            var response = new List<string>();
            if (requestMain.Requests != null)
            {
                foreach (var request in requestMain.Requests)
                {


                    string url = request.Url;

                    long fileSize = request.FileSize;

                    Attachment attachment = new(request.Name, url, requestMain.EntityId, requestMain.EntityType, request.FileType, fileSize, DateTimePersonalized.NowPeru);
                    _attachmentRepository.Save(attachment);
                    _context.SaveChanges(userId);
                    response.Add(url);

                }
            }
            return response;
        }
        public async Task<Result<List<string>, Notification>> RegisterAttachmentOnlyUpload(RegisterUploadFilePostRequest request, Guid userId)
        {
            var response = new List<string>();

            Notification notification = new();

            string fileBase64 = request.Base64;

            if (!string.IsNullOrEmpty(fileBase64))
            {
                var _fileApplicationsService = new FileApplicationsService();
                Result<FileDto, Notification> result = _fileApplicationsService.ConverterBase64InBytes(fileBase64, "");

                if (result.IsFailure)
                    return result.Error;
                using var stream = new MemoryStream(result.Value.Bytes);

                string url = request.FileName;
                var fileS3 = new S3FileDto
                {
                    FileName = url,
                    stream = stream,
                };

                S3ResponseDto signUpload = await _s3bucketService.UploadFileAsync(fileS3, CommonStatic.BucketNameFiles);

                if (signUpload.StatusCode != 200)
                {
                    notification.AddError(signUpload.Message);
                    return notification;
                }
                response.Add(url);
            }

            return response;
        }

        public async Task<Result<List<string>, Notification>> RegisterAttachmentUpload(RegisterAttachmentUploadPostRequest requestMain,Guid userId)
        {
            var response = new List<string>();
            if (requestMain.Requests != null)
            {
                foreach (var request in requestMain.Requests)
                {
                    Notification notification = new();

                    string fileBase64 = request.Base64;

                    if (!string.IsNullOrEmpty(fileBase64))
                    {

                        string descriptionFile = request.Name;

                        var _fileApplicationsService = new FileApplicationsService();

                        Result<FileDto, Notification> result = _fileApplicationsService.ConverterBase64InBytes(fileBase64, "");


                        if (result.IsFailure)
                            return result.Error;

                        using var stream = new MemoryStream(result.Value.Bytes);

                        string url = requestMain.Directory + Guid.NewGuid();
                        var fileS3 = new S3FileDto
                        {
                            FileName = url,
                            stream = stream,
                        };

                        S3ResponseDto signUpload = await _s3bucketService.UploadFileAsync(fileS3, CommonStatic.BucketNameFiles);

                        if (signUpload.StatusCode != 200)
                        {
                            notification.AddError(signUpload.Message);
                            return notification;
                        }


                        if (result.Value == null)
                        {
                            url = "";
                        }

                        FileType formatType = FileType.Other;

                        //RegisterAttachmentRequest request1 = request;
                        if (request.FileType.Contains("PDF", StringComparison.CurrentCultureIgnoreCase))
                            formatType = FileType.PDF;
                        else if (request.FileType.Contains("PNG", StringComparison.CurrentCultureIgnoreCase))
                            formatType = FileType.PNG;
                        else if (request.FileType.Contains("JPG", StringComparison.CurrentCultureIgnoreCase))
                            formatType = FileType.JPG;
                        else if (request.FileType.Contains("GIF", StringComparison.CurrentCultureIgnoreCase))
                            formatType = FileType.GIF;
                        else if (request.FileType.Contains("BPM", StringComparison.CurrentCultureIgnoreCase))
                            formatType = FileType.BMP;

                        long fileSize = request.FileSize;

                        Attachment attachment = new(descriptionFile, url, requestMain.EntityId, requestMain.EntityType, formatType, fileSize, DateTimePersonalized.NowPeru);
                        _attachmentRepository.Save(attachment);
                        _context.SaveChanges(userId);
                        response.Add(url);
                    }
                }
            }
            return response;
        }

        public EditAttachmentResponse EditAttachment(EditAttachmentRequest1 request, Attachment attachment, Guid userId)
        {
            attachment.Name = request.Name.Trim();


            _context.SaveChanges(userId);

            var response = new EditAttachmentResponse
            {
                Id = attachment.Id,
                Name = attachment.Name,
                Status = attachment.Status
            };

            return response;
        }

        public EditAttachmentResponse ActiveAttachment(Attachment attachment, Guid userId)
        {
            attachment.Status = true;

            _context.SaveChanges(userId);

            var response = new EditAttachmentResponse
            {
                Id = attachment.Id,
                Name = attachment.Name,
                Status = attachment.Status
            };

            return response;
        }
        public Notification ValidateEditAttachmentRequest(EditAttachmentRequest1 request)
        {
            return _editAttachmentValidator.Validate(request);
        }

        public List<EditAttachmentResponse> RemoveAttachments(List<Attachment> attachments, Guid userId)
        {
            List<EditAttachmentResponse> responses = new();
            foreach (var attachment in attachments)
            {
                attachment.Status = false;

                responses.Add(new EditAttachmentResponse
                {
                    Id = attachment.Id,
                    Name = attachment.Name,
                    Status = attachment.Status
                });
            }

            _context.SaveChanges(userId);
            return responses;
        }

        public EditAttachmentResponse RemoveAttachment(Attachment attachment, Guid userId)
        {
            attachment.Status = false;
            _context.SaveChanges(userId);

            var response = new EditAttachmentResponse
            {
                Id = attachment.Id,
                Name = attachment.Name,
                Status = attachment.Status
            };

            return response;
        }

        public List<AttachmentDto>? GetByEntityId(Guid entityId, EntityType? entityType)
        {
            if (entityType == null)
            {
                return _attachmentRepository.GetDtoByIdEntity(entityId);
            }
            else
            {
                return _attachmentRepository.GetDtoByIdEntity(entityId,(EntityType)entityType);
            }
        }

        public Attachment? GetById(Guid id)
        {
            return _attachmentRepository.GetById(id);
        }

        public List<Attachment>? GetByIds(List<Guid> ids)
        {
            return _attachmentRepository.GetByIds(ids);
        }
    }
}

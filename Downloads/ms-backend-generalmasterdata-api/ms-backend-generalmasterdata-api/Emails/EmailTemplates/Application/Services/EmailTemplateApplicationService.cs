using CSharpFunctionalExtensions;
using AnaPrevention.GeneralMasterData.Api.Attachments.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.Attachments.Application.Services;
using AnaPrevention.GeneralMasterData.Api.Attachments.Domain.Enums;
using AnaPrevention.GeneralMasterData.Api.Common.API;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Common.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Common.Helper.Mail;
using AnaPrevention.GeneralMasterData.Api.Common.Infrastructure.EF;
using AnaPrevention.GeneralMasterData.Api.Emails.EmailTags.Domain.Enum;
using AnaPrevention.GeneralMasterData.Api.Emails.EmailTemplates.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.Emails.EmailTemplates.Application.Validators;
using AnaPrevention.GeneralMasterData.Api.Emails.EmailTemplates.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Emails.EmailTemplates.Infrastructure.Repositories;
using System.Text.RegularExpressions;
using AnaPrevention.GeneralMasterData.Api.Attachments.Application.Dtos.S3;
using MailKit.Security;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Services;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Dtos.OccupationalMasterData.Dto;

namespace AnaPrevention.GeneralMasterData.Api.Emails.EmailTemplates.Application.Services
{
    public class EmailTemplateApplicationService(AnaPreventionContext context, EmailTemplateRepository emailTemplateRepository, EmailTemplateValidator emailTemplateValidator, EmailService emailService, S3AmazonService s3bucketService, AttachmentApplicationService attachmentApplicationService, ApiApplicationService apiApplicationService)
    {
        private readonly AnaPreventionContext _context = context;
        private readonly EmailTemplateRepository _emailTemplateRepository = emailTemplateRepository;
        private readonly EmailTemplateValidator _emailTemplateValidator = emailTemplateValidator;
        private readonly EmailService _emailService = emailService;
        private readonly S3AmazonService _s3bucketService = s3bucketService;
        private readonly AttachmentApplicationService _attachmentApplicationService = attachmentApplicationService;
        private readonly ApiApplicationService _apiApplicationService = apiApplicationService;

        public async Task<Result<EmailTemplateResponse, Notification>> RegisterEmailTemplateAsync(EmailTemplateRequest request, Guid userId)
        {
            Notification notification = _emailTemplateValidator.ValidateRegister(request);

            if (notification.HasErrors())
                return notification;


            string description = request.Description.Trim();
            string body = request.Body.Trim();
            string subject = request.Subject.Trim();
            Guid emailUserId = request.EmailUserId;
            EmailTagTemplateType emailTagTemplateType = request.EmailTagTemplateType;
            bool isDefault = request.IsDefault;

            if (isDefault)
            {
                var emailTemplateCurrentDefault = _emailTemplateRepository.GetDefault(emailTagTemplateType);

                if (emailTemplateCurrentDefault != null)
                {
                    emailTemplateCurrentDefault.IsDefault = false;
                }
            }
            else
            {
                //si no encuentra uno por defecto, lo pone al actual como por defecto #JAS
                var emailTemplateCurrentDefault = _emailTemplateRepository.GetDefault(emailTagTemplateType);

                if (emailTemplateCurrentDefault == null)
                {
                    isDefault = true;
                }
            }


            EmailTemplate emailTemplate = new(Guid.NewGuid(), description, subject, emailUserId, body, emailTagTemplateType, isDefault);

            _emailTemplateRepository.Save(emailTemplate);

            if (request.Attachments != null)
            {
                var result = await SaveAttachmentsAsync(request.Attachments, emailTemplate.Id,userId);

                if (result.IsFailure)
                    return await Task.FromResult(result.Error);
            }

            _context.SaveChanges(userId);

            var response = new EmailTemplateResponse
            {
                Id = emailTemplate.Id,
                Body = emailTemplate.Body,
                Description = emailTemplate.Description,
                EmailUserId = emailTemplate.EmailUserId,
                Subject = emailTemplate.Subject,
                Status = emailTemplate.Status,
            };

            return response;
        }

        public async Task<Result<EmailTemplateResponse, Notification>> EditEmailTemplateAsync(EditEmailTemplateRequest request, EmailTemplate emailTemplate, Guid userId)
        {
            emailTemplate.Description = request.Description.Trim();
            emailTemplate.Subject = request.Subject.Trim();
            emailTemplate.Body = request.Body.Trim();
            emailTemplate.EmailUserId = request.EmailUserId;
            emailTemplate.EmailTagTemplateType = request.EmailTagTemplateType;
            emailTemplate.IsDefault = request.IsDefault;

            if (request.IsDefault)
            {
                var emailTemplateCurrentDefault = _emailTemplateRepository.GetDefaultTakeEdit(request.EmailTagTemplateType, emailTemplate.Id);

                if (emailTemplateCurrentDefault != null)
                {
                    emailTemplateCurrentDefault.IsDefault = false;
                }
            }
            else
            {
                //si no encuentra uno por defecto, lo pone al actual como por defecto #JAS
                var emailTemplateCurrentDefault = _emailTemplateRepository.GetDefaultTakeEdit(request.EmailTagTemplateType, emailTemplate.Id);

                if (emailTemplateCurrentDefault == null)
                {
                    emailTemplate.IsDefault = true;
                }
            }

            if (request.Attachments != null)
            {
                var result = await SaveAttachmentsAsync(request.Attachments, emailTemplate.Id, userId);

                if (result.IsFailure)
                    return await Task.FromResult(result.Error);
            }

            _context.SaveChanges(userId);

            var response = new EmailTemplateResponse
            {
                Id = emailTemplate.Id,
                Body = emailTemplate.Body,
                Description = emailTemplate.Description,
                EmailUserId = emailTemplate.EmailUserId,
                Subject = emailTemplate.Subject,
                Status = emailTemplate.Status,
            };

            return response;
        }

        public async Task<Result<string, Notification>> SaveAttachmentsAsync(List<RegisterAttachmentRequest> Attachments,Guid emailTemplateId,Guid userId)
        {
            Result<string, Notification> result = new();
      
            foreach (var attachment in Attachments)
            {
                attachment.Directory = "emailTemplate/";
                result = _attachmentApplicationService.RegisterAttachment(attachment, emailTemplateId, EntityType.EMAIL_TEMPLATE, userId);

                if (result.IsFailure)
                    return await Task.FromResult(result.Error);
            }

            return result;
        }
        public EmailTemplateResponse ActiveEmailTemplate(EmailTemplate emailTemplate, Guid userId)
        {
            emailTemplate.Status = true;

            _context.SaveChanges(userId);

            var response = new EmailTemplateResponse
            {
                Id = emailTemplate.Id,
                Body = emailTemplate.Body,
                Description = emailTemplate.Description,
                EmailUserId = emailTemplate.EmailUserId,
                Subject = emailTemplate.Subject,
                Status = emailTemplate.Status,
            };

            return response;
        }

        public bool SetDefault(RegisterDefaultTemplateDto request, Guid userId)
        {
            foreach (var id in request.Ids)
            {
                EmailTemplate? emailTemplate = _emailTemplateRepository.GetById(id);
                if (emailTemplate != null)
                {
                    var defaultTemplate = _emailTemplateRepository.GetDefault(emailTemplate.EmailTagTemplateType);
                    if (defaultTemplate != null && defaultTemplate.Id != emailTemplate.Id)
                    {
                        defaultTemplate.IsDefault = false;
                    }

                    emailTemplate.IsDefault = true;
                }
            }

            _context.SaveChanges(userId);

            return true;
        }

        public Notification ValidateEditEmailTemplateRequest(EditEmailTemplateRequest request)
        {
            return _emailTemplateValidator.ValidateEdit(request);
        }

        public EmailTemplateResponse RemoveEmailTemplate(EmailTemplate emailTemplate, Guid userId)
        {
            emailTemplate.Status = false;
            _context.SaveChanges(userId);

            var response = new EmailTemplateResponse
            {
                Id = emailTemplate.Id,
                Body = emailTemplate.Body,
                Description = emailTemplate.Description,
                EmailUserId = emailTemplate.EmailUserId,
                Subject = emailTemplate.Subject,
                Status = emailTemplate.Status,
            };

            return response;
        }

        public EmailTemplate? GetById(Guid id)
        {
            return _emailTemplateRepository.GetById(id);
        }

        public EmailTemplateDto? GetDtoById(Guid id)
        {
            return _emailTemplateRepository.GetDtoById(id);
        }

        public List<EmailTemplateDto> GetListAll()
        {
            return _emailTemplateRepository.GetListAll();
        }

        public async Task SendEmail(Guid id, Guid referenceId, Guid userId)
        {
            var emailTemplateDto = await GetConvert(id, referenceId);

            if (emailTemplateDto != null && emailTemplateDto.EmailUser != null && emailTemplateDto.ToEmailAddress != null)
            {

                List<AttachmentEmailDto> attachmentEmailDtos = new();
                if (emailTemplateDto.Attachments != null && emailTemplateDto.Attachments.Count > 0)
                {
                    foreach (var attachment in emailTemplateDto.Attachments)
                    {


                        var s3Object = new S3DownloadFileRequest()
                        {
                            BucketName = CommonStatic.BucketNameFiles,
                            KeyName =attachment.Url,
                        };
                        var base64 = await _s3bucketService.DownloadObjectS3(s3Object);
                        var mimeType = CommonStatic.GetMimeType(Path.GetExtension(attachment.Url)).Split('/');
                        attachmentEmailDtos.Add(new()
                        {
                            Base64 = base64,
                            Name = attachment.Name,
                            TypeMedia = mimeType[0],
                            SubTypeMedia = mimeType[1],
                        });

                    }
                }

                EmailMessage emailMessage = new()
                {
                    Content = emailTemplateDto.Body,
                    Subject = emailTemplateDto.Subject,
                    FromAddresses = new()
                    {
                        new()
                        {
                            Address = emailTemplateDto.EmailUser.Email,
                            Name= emailTemplateDto.EmailUser.Name,
                        }
                    },
                    ToAddresses = emailTemplateDto.ToEmailAddress,
                    Attachments = attachmentEmailDtos
                };


                EmailConfig emailConfig = new()
                {
                    Email = emailTemplateDto.EmailUser.Email,
                    Host = emailTemplateDto.EmailUser.Host,
                    Password = emailTemplateDto.EmailUser.Password,
                    Port = emailTemplateDto.EmailUser.Port,
                    SecureSocketOptions = (emailTemplateDto.EmailUser.ProtocolType == EmailUsers.Domain.Enums.ProtocolType.SSL ? SecureSocketOptions.SslOnConnect : SecureSocketOptions.StartTls)
                };
                //JsonSerializer.Serialize(emailTemplateDto.Attachments
                await _emailService.SendAsync(emailMessage, userId, null, emailConfig);
            }
        }



        public async Task<EmailTemplateDto?> GetConvert(Guid id, Guid referenceId)
        {
            var emailTemplateDto = _emailTemplateRepository.GetDtoById(id);

            if (emailTemplateDto != null)
            {
                Regex regex = new(@"\[(.*?)\]");

                MatchCollection matches = regex.Matches(emailTemplateDto.Body);

                List<string> stringsfound = new();

                for (int i = 0; i < matches.Count; i++)
                {
                    stringsfound.Add(matches[i].Groups[1].Value);
                }

                Dictionary<string, string> stringsfoundUnique = stringsfound.Distinct().ToDictionary(key => key, value => "");

                object? obj = null;

                if (emailTemplateDto.EmailTagTemplateType == EmailTagTemplateType.OCCUPATIONAL_ORDER)
                {
                    var order = await _apiApplicationService.GetOccupationalMasterData<OrderMinDto>($"occupation/order/header/{referenceId}");
                    if (order != null)
                    {
                        emailTemplateDto.ToEmailAddress = new()
                        {
                            new()
                                {
                                    Address = order.PersonalEmail??"",
                                    Name = (order.Person).Trim()
                                }
                        };


                        obj = order;
                    }
                    else if (emailTemplateDto.EmailTagTemplateType == EmailTagTemplateType.OCCUPATIONAL_APPOINTMENT)
                    {
                        var appointment =  await _apiApplicationService.GetOccupationalMasterData<AppointmentDetailMinDto>($"occupation/appointments/detail/{referenceId}");
                        if (appointment != null && appointment.PersonalEmail != null)
                        {
                            emailTemplateDto.ToEmailAddress = new()
                            {
                                new()
                                {
                                    Address = appointment.PersonalEmail,
                                    Name = (appointment.Names + " " + appointment.LastName + " " + appointment.SecondLastName).Trim()
                                }
                            };
                        }

                        obj = appointment;
                    }

                    foreach (var row in stringsfoundUnique)
                    {
                        stringsfoundUnique[row.Key] = CommonStatic.GetValueAtribute(obj, row.Key);
                    }

                    emailTemplateDto.Body = CommonStatic.Replace(emailTemplateDto.Body, stringsfoundUnique);
                }
            }

            return emailTemplateDto;
        }

        public Tuple<IEnumerable<EmailTemplateDto>, PaginationMetadata> GetList(int pageNumber, int pageSize, bool status, string? descriptionSearch = "", string? emailSearch = "")
        {
            return _emailTemplateRepository.GetList(pageNumber, pageSize, status, descriptionSearch, emailSearch);
        }
    }
}

using CSharpFunctionalExtensions;
using AnaPrevention.GeneralMasterData.Api.Common.API;
using AnaPrevention.GeneralMasterData.Api.Common.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Common.Infrastructure.EF;
using AnaPrevention.GeneralMasterData.Api.Emails.EmailContents.Applications.Dtos;
using AnaPrevention.GeneralMasterData.Api.Emails.EmailContents.Applications.Validators;
using AnaPrevention.GeneralMasterData.Api.Emails.EmailContents.Domain.Entitites;
using AnaPrevention.GeneralMasterData.Api.Emails.EmailContents.Infraestructure.Repository;
using AnaPrevention.GeneralMasterData.Api.Emails.EmailTags.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Emails.EmailTags.Domain.Enum;
using System.Text.Json;

namespace AnaPrevention.GeneralMasterData.Api.Emails.EmailContents.Applications.Services
{
    public class EmailContentApplicationServices
    {
        private readonly EmailContentRepository _emailContentRepository;
        private readonly RegisterEmailContentValidator _registerEmailContentValidator;
       
        private readonly AnaPreventionContext _context;
        public EmailContentApplicationServices(AnaPreventionContext context, EmailContentRepository emailContentRepository, RegisterEmailContentValidator registerEmailContentValidator)
        {
            _context = context;
            _emailContentRepository = emailContentRepository;
            _registerEmailContentValidator = registerEmailContentValidator;
        }


        public async Task<Result<RegisterEmailContentResponse, Notification>> RegisterEmailContentAsync(RegisterEmailContentRequest request, Guid userId)
        {
            Notification notification = _registerEmailContentValidator.ValidateRegister(request);
            if (notification.HasErrors())
                return notification;

            TimeZoneInfo zonaHoraria = TimeZoneInfo.FindSystemTimeZoneById("SA Pacific Standard Time");
            DateTime horaActualPE = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, zonaHoraria);

            EmailContent emailContent = new
                    (
                        userId: userId,
                        fromAddress: request.FromAddress!,
                        toAddress: request.ToAddress!,
                        dateSend: horaActualPE,
                        body: request.Body!,
                        attachmentUrls: JsonSerializer.Serialize(request.Attachments),
                        emailTemplateId: request.EmailTemplateId,
                        result: request.Result,
                        toPersonId: request.ToPersonId,
                        referenceId: request.ReferenceId,
                        subject: request.Subject
                    );
            AnaPreventionContext context = new(_context.GetConnectionString(), _context.GetUseConsoleLogger(), _context.GetInitialHasData(), _context.GetBucketName());

            EmailContentRepository emailContentRepository = new(context);
            emailContentRepository.Save(emailContent);
            context.SaveChanges(userId);

            var response = new RegisterEmailContentResponse
            {
                Id = emailContent.Id,
                Status = emailContent.Status,
            };

            return await Task.FromResult(response);
        }

        public RegisterEmailContentResponse ActiveEmailContent(EmailContent emailContent, Guid userId)
        {
            emailContent.Status = true;

            _context.SaveChanges(userId);

            var response = new RegisterEmailContentResponse
            {
                Id = emailContent.Id,
                Status = emailContent.Status,
            };

            return response;
        }
        public RegisterEmailContentResponse RemoveEmailContent(EmailContent emailContent, Guid userId)
        {
            emailContent.Status = false;
            _context.SaveChanges(userId);

            var response = new RegisterEmailContentResponse
            {
                Id = emailContent.Id,
                Status = emailContent.Status,
            };
            return response;
        }
        public object? GetEmailTagTemplateType()
        {
            var result = EmailTagStatic.GetEmailTagTemplateType();
            List<object> PayloadDetails = new();

            foreach (var item in result)
            {
                var payload = new
                {
                    Id = (int)item.Key,
                    Description = item.Value,
                };
                PayloadDetails.Add(payload);
            }

            return PayloadDetails;
        }
        public EmailContent? GetById(Guid id)
        {
            return _emailContentRepository.GetById(id);
        }
        public EmailContentDto? GetDtoById(Guid id)
        {
            return _emailContentRepository.GetDtoById(id);
        }
        public Tuple<IEnumerable<EmailContentDto>, PaginationMetadata> GetList(int pageNumber, int pageSize, bool status = true, string fromAddress = "", string toAddress = "", EmailTagTemplateType? emailTagTemplateType = null, DateTime? dateStartSend = null, DateTime? dateFinishSend = null, string? subject = "")
        {
            return _emailContentRepository.GetList(pageNumber, pageSize, status, fromAddress, toAddress,emailTagTemplateType, dateStartSend, dateFinishSend,subject);
        }

        public List<EmailContentDto> GetListFilter(bool status = true, string fromAddress = "", string toAddress = "", EmailTagTemplateType? emailTagTemplateType = null, DateTime? dateStartSend = null, DateTime? dateFinishSend = null)
        {
            return _emailContentRepository.GetListFilter(status, fromAddress, toAddress,emailTagTemplateType, dateStartSend, dateFinishSend);
        }
    }
}

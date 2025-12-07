using AnaPrevention.GeneralMasterData.Api.Attachments.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.Attachments.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Common.Helper.Mail;
using AnaPrevention.GeneralMasterData.Api.Emails.EmailTags.Domain.Enum;
using AnaPrevention.GeneralMasterData.Api.Emails.EmailUsers.Application.Dtos;

namespace AnaPrevention.GeneralMasterData.Api.Emails.EmailTemplates.Application.Dtos
{
    public class EmailTemplateDto
    {
        public Guid Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public Guid EmailUserId { get; set; }
        public EmailUserDto? EmailUser { get; set; }
        public string Body { get; set; } = string.Empty;
        public EmailTagTemplateType EmailTagTemplateType { get; set; }
        public List<Attachment>? Attachments { get; set; }
        public List<EmailAddress>? ToEmailAddress { get; set; } = new();
        public bool IsDefault { get; set; }
        public bool Status { get; set; }
    }
}
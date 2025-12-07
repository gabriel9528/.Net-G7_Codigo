using AnaPrevention.GeneralMasterData.Api.Attachments.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.Emails.EmailTags.Domain.Enum;

namespace AnaPrevention.GeneralMasterData.Api.Emails.EmailTemplates.Application.Dtos
{
    public class EditEmailTemplateRequest
    {
        public Guid Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public Guid EmailUserId { get; set; }
        public string Body { get; set; } = string.Empty;
        public bool IsDefault { get; set; }
        public EmailTagTemplateType EmailTagTemplateType { get; set; }
        public List<RegisterAttachmentRequest>? Attachments { get; set; }
    }
}

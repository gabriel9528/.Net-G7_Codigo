using AnaPrevention.GeneralMasterData.Api.Emails.EmailUsers.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Emails.EmailTags.Domain.Enum;

namespace AnaPrevention.GeneralMasterData.Api.Emails.EmailTemplates.Domain.Entities
{
    public class EmailTemplate
    {
        public EmailTemplate(Guid id, string description, string subject, Guid emailUserId, string body, EmailTagTemplateType emailTagTemplateType, bool isDefault)
        {
            Id = id;
            Description = description;
            Subject = subject;
            EmailUserId = emailUserId;
            Body = body;
            Status = true;
            EmailTagTemplateType = emailTagTemplateType;
            IsDefault = isDefault;
        }

        public EmailTemplate() { }

        public Guid Id { get; set; }
        public string Description { get; set; }
        public string Subject { get; set; }
        public Guid EmailUserId { get; set; }
        public EmailUser EmailUser { get; set; }
        public EmailTagTemplateType EmailTagTemplateType { get; set; }
        public string Body { get; set; }
        public bool Status { get; set; }
        public bool IsDefault { get; set; }
    }
}

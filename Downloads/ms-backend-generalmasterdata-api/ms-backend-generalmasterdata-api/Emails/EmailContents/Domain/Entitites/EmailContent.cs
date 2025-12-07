using AnaPrevention.GeneralMasterData.Api.Persons.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Emails.EmailTemplates.Domain.Entities;

namespace AnaPrevention.GeneralMasterData.Api.Emails.EmailContents.Domain.Entitites
{
    public class EmailContent
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string FromAddress { get; set; }
        public string ToAddress { get; set; }
        public DateTime DateSend { get; set; }
        public string Body { get; set; }
        public string? AttachmentUrls { get; set; }
        public Guid? EmailTemplateId { get; set; }
        public EmailTemplate? EmailTemplate { get; set; }
        public Person ToPerson { get; set; }
        public Guid? ToPersonId { get; set; }
        public bool Status { get; set; }
        public string? Result { get; set; }
        public Guid? ReferenceId { get; set; }
        public string? Subject { get; set; }

        public EmailContent()
        {
        }

        public EmailContent(Guid userId, string fromAddress, string toAddress, DateTime dateSend, string body, string? attachmentUrls = "", Guid? emailTemplateId = null, string? result = null, Guid? toPersonId = null, Guid? referenceId = null, string? subject = null)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            FromAddress = fromAddress;
            ToAddress = toAddress;
            DateSend = dateSend;
            Body = body;
            AttachmentUrls = attachmentUrls;
            Status = true;
            EmailTemplateId = emailTemplateId;
            Result = result;
            ToPersonId = toPersonId;
            ReferenceId = referenceId;
            Subject = subject;
        }
    }
}

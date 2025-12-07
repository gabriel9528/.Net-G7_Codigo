using AnaPrevention.GeneralMasterData.Api.Emails.EmailTags.Domain.Enum;

namespace AnaPrevention.GeneralMasterData.Api.Emails.EmailContents.Applications.Dtos
{
    public class EmailContentDto
    {
        public Guid Id { get; set; }
        public string? FromAddress { get; set; }
        public string? Subject { get; set; }
        public string? ToAddress { get; set; }
        public string? DateSend { get; set; }
        public DateTime DateSendformat { get; set; }
        public string? ToPerson { get; set; }
        public string? Body { get; set; }
        public Guid? ToPersonId { get; set; }
        public bool Status { get; set; }
        public string? Result { get; set; }
        public string? EmailTemplate { get; set; }
        public Guid? EmailTemplateId { get; set; }
        public EmailTagTemplateType? EmailTagTemplateType { get; set; }
        public List<AttachmentEmailContent>? AttachmentUrls { get; set; }
        public Guid? ReferenceId { get; set; }
    }
}

namespace AnaPrevention.GeneralMasterData.Api.Emails.EmailContents.Applications.Dtos
{
    public class RegisterEmailContentRequest
    {
        public string? FromAddress { get; set; }
        public string? ToAddress { get; set; }
        public string? Subject { get; set; }
        public Guid? ToPersonId {get; set;}
        public string? Body { get; set; }
        public Guid? EmailTemplateId { get; set; }
        public List<AttachmentEmailContent>? Attachments { get; set; }
        public string? Result { get; set; }
        public Guid? ReferenceId { get; set; }
    }
}

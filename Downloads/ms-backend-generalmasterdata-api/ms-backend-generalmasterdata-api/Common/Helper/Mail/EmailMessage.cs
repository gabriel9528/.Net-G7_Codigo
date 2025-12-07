using AnaPrevention.GeneralMasterData.Api.Attachments.Application.Dtos;

namespace AnaPrevention.GeneralMasterData.Api.Common.Helper.Mail
{
    public class EmailMessage
    {
		public EmailMessage()
		{
			ToAddresses = new List<EmailAddress>();
			FromAddresses = new List<EmailAddress>();
			Attachments = new List<AttachmentEmailDto>();
		}

		public List<EmailAddress> ToAddresses { get; set; }
		public List<EmailAddress> FromAddresses { get; set; }
		public string Subject { get; set; }
		public string Content { get; set; }
        public List<AttachmentEmailDto>? Attachments { get; set; }
        public Guid? EmailTemplateId { get; set; }
        public Guid? ReferenceId { get; set; }
        public Guid? PersonId { get; set; }
    }
}

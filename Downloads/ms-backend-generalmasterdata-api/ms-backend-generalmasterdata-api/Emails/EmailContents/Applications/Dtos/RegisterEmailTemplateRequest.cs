using AnaPrevention.GeneralMasterData.Api.Attachments.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.Attachments.Domain.Enums;
using AnaPrevention.GeneralMasterData.Api.Common.Helper.Mail;
using System.Net.Mail;

namespace AnaPrevention.GeneralMasterData.Api.Emails.EmailContents.Applications.Dtos
{
    public class RegisterEmailTemplateRequest
    {
        public Guid? OccupationalHealthId { get; set; }
        public EntityType EntityType { get; set; }
        public string? Body { get; set; }
        public string? Subject { get; set; }
        public EmailAddress? ToAddress { get; set; }
        public List<AttachmentEmailDto>? Attachments { get; set; }
    }
}

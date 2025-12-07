using AnaPrevention.GeneralMasterData.Api.Attachments.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.Emails.EmailTags.Domain.Enum;

namespace AnaPrevention.GeneralMasterData.Api.Emails.EmailTags.Application.Dtos
{
    public class RegisterEmailTagRequest
	{
        public string Description { get; set; } = String.Empty;
        public string Tag { get; set; } = String.Empty;
        public EmailTagTemplateType EmailTagTemplateType { get; set; }
    }
}

using AnaPrevention.GeneralMasterData.Api.Emails.EmailTags.Domain.Enum;

namespace AnaPrevention.GeneralMasterData.Api.Emails.EmailTags.Domain.Entities
{
    public class EmailTag
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public string Tag { get; set; }
        public bool Status { get; set; }
        public EmailTagTemplateType EmailTagTemplateType { get; set; }

        public EmailTag() { }

        public EmailTag(Guid id, string description, string tag, EmailTagTemplateType emailTagType)
        {
            Id = id;
            Status = true;
            Tag = tag;
            Description = description;
            EmailTagTemplateType = emailTagType;
        }
    }
}

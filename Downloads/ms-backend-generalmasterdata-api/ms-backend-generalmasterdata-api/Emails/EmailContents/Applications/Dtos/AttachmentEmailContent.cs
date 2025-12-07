namespace AnaPrevention.GeneralMasterData.Api.Emails.EmailContents.Applications.Dtos
{
    public class AttachmentEmailContent
    {
        public string Name { get; set; } = String.Empty;
        public string? TypeMedia { get; set; }
        public string? SubTypeMedia { get; set; }
        public string? Url { get; set; }
    }
}

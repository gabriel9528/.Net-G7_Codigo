namespace AnaPrevention.GeneralMasterData.Api.Attachments.Application.Dtos
{
    public class AttachmentEmailDto
    {
        public string Name { get; set; } = String.Empty;
        public string Base64 { get; set; } = String.Empty;
        public string? TypeMedia { get; set; }
        public string? SubTypeMedia { get; set; }
        public string? Url { get; set; }

    }
}

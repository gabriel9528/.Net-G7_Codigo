namespace AnaPrevention.GeneralMasterData.Api.Attachments.Application.Dtos
{
    public class RegisterAttachmentRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Directory { get; set; } = string.Empty;
        public long FileSize { get; set; }
        public string FileType { get; set; } = string.Empty;
        public string Base64 { get; set; } = string.Empty;
        public byte[]? Bytes { get; set; }
    }
}

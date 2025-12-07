namespace AnaPrevention.GeneralMasterData.Api.Attachments.Application.Dtos
{
    public class RegisterUploadFilePostRequest
    {
        public string FileName { get; set; } = string.Empty;
        public string Base64 { get; set; } = string.Empty;
    }
}

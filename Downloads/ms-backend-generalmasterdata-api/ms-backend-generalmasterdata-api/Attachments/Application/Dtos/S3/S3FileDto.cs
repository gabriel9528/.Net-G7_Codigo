namespace AnaPrevention.GeneralMasterData.Api.Attachments.Application.Dtos.S3
{
    public class S3FileDto
    {
        public string FileName { get; set; } = String.Empty;
        public MemoryStream stream { get; set; } = new();
    }
}

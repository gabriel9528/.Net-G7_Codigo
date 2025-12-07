namespace AnaPrevention.GeneralMasterData.Api.Attachments.Application.Dtos.S3
{
    public class S3Objects
    {
        public string Name { get; set; } = null!;
        public MemoryStream InputStream { get; set; } = null!;
        public string BucketName { get; set; } = null!;
    }
}

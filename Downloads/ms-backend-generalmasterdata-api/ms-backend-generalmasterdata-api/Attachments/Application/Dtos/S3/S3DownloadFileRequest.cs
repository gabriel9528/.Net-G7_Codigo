using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;

namespace AnaPrevention.GeneralMasterData.Api.Attachments.Application.Dtos.S3
{
    public class S3DownloadFileRequest
    {
        public string? BucketName { get; set; } = CommonStatic.BucketNameFiles;
        public string KeyName { get; set; }
    }
}

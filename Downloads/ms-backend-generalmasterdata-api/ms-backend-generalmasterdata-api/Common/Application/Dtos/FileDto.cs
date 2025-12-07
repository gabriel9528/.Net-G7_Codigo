namespace AnaPrevention.GeneralMasterData.Api.Common.Application.Dtos
{
    public class FileDto
    {
        public byte[]? Bytes { get; set; }
        public string? FileName { get; set; }
        public string? FileExtension { get; set; }
        public string? ContentType{ get; set; }
        public string? Url { get; set; }
    }
}

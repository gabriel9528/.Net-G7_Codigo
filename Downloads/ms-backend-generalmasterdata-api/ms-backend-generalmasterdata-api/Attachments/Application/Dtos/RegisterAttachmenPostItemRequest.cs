using AnaPrevention.GeneralMasterData.Api.Attachments.Domain.Enums;

namespace AnaPrevention.GeneralMasterData.Api.Attachments.Application.Dtos
{
    public class RegisterAttachmenPostItemRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public long FileSize { get; set; }
        public FileType FileType { get; set; }
    }
}

using AnaPrevention.GeneralMasterData.Api.Attachments.Domain.Enums;

namespace AnaPrevention.GeneralMasterData.Api.Attachments.Application.Dtos
{
    public class AttachmentDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public string Url { get; set; } = String.Empty;
        public Guid EntityId { get; set; }
        public EntityType EntityType { get; set; }
        public FileType FileType { get; set; }
        public long FileSize { get; set; }
        public string DateCreated { get; set; } = String.Empty;
        public bool Status { get; set; }
    }
}

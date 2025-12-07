using AnaPrevention.GeneralMasterData.Api.Attachments.Domain.Enums;

namespace AnaPrevention.GeneralMasterData.Api.Attachments.Domain.Entities
{
    public class Attachment
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public Guid EntityId { get; set; }
        public EntityType EntityType { get; set; }
        public FileType FileType { get; set; }
        public long FileSize { get; set; }
        public DateTime DateCreated { get; set; }
        public bool Status { get; set; }

        public Attachment() { }
        public Attachment(string name, string url, Guid entityId, EntityType entityType, FileType fileType, long fileSize, DateTime dateCreated)
        {
            Name = name;
            Url = url;
            EntityId = entityId;
            EntityType = entityType;
            FileType = fileType;
            FileSize = fileSize;
            DateCreated = dateCreated;
            Status = true;
        }
    }
}


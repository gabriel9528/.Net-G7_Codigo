using AnaPrevention.GeneralMasterData.Api.Attachments.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.Attachments.Domain.Enums;

namespace AnaPrevention.GeneralMasterData.Api.Attachments.Application.Dtos
{
    public class RegisterAttachmenPostRequest
    {
        public List<RegisterAttachmenPostItemRequest>? Requests { get; set; }
        public EntityType EntityType { get; set; }
        public Guid EntityId { get; set; }
        public string Directory { get; set; } = string.Empty;
    }
}

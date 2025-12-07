namespace AnaPrevention.GeneralMasterData.Api.Attachments.Application.Dtos
{
    public class AttachmentIdsRequest
    {
        public List<Guid> Ids { get; set; } = new();
    }
}

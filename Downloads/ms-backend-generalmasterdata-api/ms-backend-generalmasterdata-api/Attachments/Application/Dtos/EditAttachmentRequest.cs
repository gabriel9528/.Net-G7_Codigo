namespace AnaPrevention.GeneralMasterData.Api.Attachments.Application.Dtos
{
    public class EditAttachmentRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}

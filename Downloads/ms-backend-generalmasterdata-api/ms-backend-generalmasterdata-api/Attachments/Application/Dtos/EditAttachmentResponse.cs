namespace AnaPrevention.GeneralMasterData.Api.Attachments.Application.Dtos
{
    public class EditAttachmentResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool Status { get; set; } 
    }
}

namespace AnaPrevention.GeneralMasterData.Api.Emails.EmailTemplates.Application.Dtos
{
    public class EmailTemplateResponse
    {
        public Guid Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public Guid EmailUserId { get; set; }
        public string Body { get; set; } = string.Empty;
        public bool Status { get; set; } 
    }
}

using AnaPrevention.GeneralMasterData.Api.Emails.EmailUsers.Domain.Enums;

namespace AnaPrevention.GeneralMasterData.Api.Emails.EmailUsers.Application.Dtos
{
    public class EditEmailUserResponse
    {

        public Guid Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public ProtocolType ProtocolType { get; set; }
        public bool Status { get; set; }
    }
}

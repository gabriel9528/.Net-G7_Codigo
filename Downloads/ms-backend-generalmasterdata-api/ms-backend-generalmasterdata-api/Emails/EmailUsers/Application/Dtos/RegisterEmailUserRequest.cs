using AnaPrevention.GeneralMasterData.Api.Emails.EmailUsers.Domain.Enums;

namespace AnaPrevention.GeneralMasterData.Api.Emails.EmailUsers.Application.Dtos
{
    public class RegisterEmailUserRequest
    {
        public string Email { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public int Port { get; set; }
        public string Host { get; set; } = string.Empty;
        public ProtocolType ProtocolType { get; set; }
    }
}

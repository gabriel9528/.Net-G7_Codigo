using MailKit.Security;

namespace AnaPrevention.GeneralMasterData.Api.Common.Helper.Mail
{
    public class EmailConfig
    {
        public string Email { get; set; } = String.Empty;
        public string Password { get; set; } = String.Empty;
        public int Port { get; set; } 
        public string Host { get; set; } = String.Empty;
        public SecureSocketOptions SecureSocketOptions { get; set; }
    }
}

using AnaPrevention.GeneralMasterData.Api.Common.Domain.ValueObjects;
using AnaPrevention.GeneralMasterData.Api.Emails.EmailUsers.Domain.Enums;

namespace AnaPrevention.GeneralMasterData.Api.Emails.EmailUsers.Domain.Entities
{
    public class EmailUser
    {
        public EmailUser(Guid id, Email email, string name, string password, int port, string host, ProtocolType protocolType)
        {
            Id = id;
            Email = email;
            Name = name;
            Password = password;
            Port = port;
            Host = host;
            ProtocolType = protocolType;
            Status = true;
        }

        public Guid Id { get; set; }
        public Email Email { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public int Port { get; set; }
        public string Host { get; set; }
        public ProtocolType ProtocolType { get; set; }
        public bool Status { get; set; }
    }
}

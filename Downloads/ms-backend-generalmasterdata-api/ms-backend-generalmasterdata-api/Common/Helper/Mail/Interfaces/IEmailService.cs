namespace AnaPrevention.GeneralMasterData.Api.Common.Helper.Mail.Interfaces
{
    public interface IEmailService
    {
        void Send(EmailMessage emailMessage);
        void SendAsync(EmailMessage emailMessage);
        List<EmailMessage> ReceiveEmail(int maxCount = 10);
    }
}

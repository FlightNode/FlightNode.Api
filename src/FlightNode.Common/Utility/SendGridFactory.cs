using SendGrid;

namespace FlightNode.Common.Utility
{
    public interface IEmailFactory
    {
        ISendGrid CreateMessage();
        ITransport CreateTransport();
        IEmailNotifier CreateNotifier();
    }

    public class EmailFactory : IEmailFactory
    {
        public ISendGrid CreateMessage()
        {
            return new SendGridMessage();
        }

        public ITransport CreateTransport()
        {
            return new Web(Properties.Settings.Default.SendGridApiKey);
        }

        public IEmailNotifier CreateNotifier()
        {
            return new EmailNotifier(this);
        }
    }
}

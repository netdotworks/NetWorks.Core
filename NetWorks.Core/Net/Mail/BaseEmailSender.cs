using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace NetWorks.Core.Net.Mail
{
    public abstract class BaseEmailSender : IEmailSender
    {
        public IEmailSenderConfiguration Configuration { get; }

        public BaseEmailSender(IEmailSenderConfiguration senderConfiguration)
        {
            Configuration = senderConfiguration;
        }

        public virtual void Send(string to, string subject, string body, bool isBodyHtml = true)
        {
            Send(new MailMessage()
            {
                To = { to },
                Subject = subject,
                Body = body,
                IsBodyHtml = isBodyHtml
            });
        }

        public virtual void Send(string from, string to, string subject, string body, bool isBodyHtml = true)
        {
            Send(new MailMessage(from, to, subject, body)
            {
                IsBodyHtml = isBodyHtml
            });
        }

        public virtual void Send(MailMessage message, bool normalize = true)
        {
            if (normalize)
            {
                NormalizeEmail(message);
            }

            SendMail(message);
        }

        public virtual async Task SendAsync(string to, string subject, string body, bool isBodyHtml = true)
        {
            await SendAsync(new MailMessage
            {
                To = { to },
                Subject = subject,
                Body = body,
                IsBodyHtml = isBodyHtml
            });
        }

        public virtual async Task SendAsync(string from, string to, string subject, string body, bool isBodyHtml = true)
        {
            await SendAsync(new MailMessage(from, to, subject, body)
            {
                IsBodyHtml = isBodyHtml
            });
        }

        public virtual async Task SendAsync(MailMessage message, bool normalize = true)
        {
            if (normalize)
            {
                NormalizeEmail(message);
            }

            await SendMailAsync(message);
        }

        public abstract void SendMail(MailMessage mailMessage);

        public abstract Task SendMailAsync(MailMessage mailMessage);

        protected virtual void NormalizeEmail(MailMessage mailMessage)
        {
            if (mailMessage.From == null || string.IsNullOrEmpty(mailMessage.From.Address))
            {
                mailMessage.From = new MailAddress(Configuration.DefaultFromAddress, Configuration.DefaultFromDisplayName, Encoding.UTF8);
            }

            if (mailMessage.HeadersEncoding == null)
            {
                mailMessage.HeadersEncoding = Encoding.UTF8;
            }

            if (mailMessage.SubjectEncoding == null)
            {
                mailMessage.SubjectEncoding = Encoding.UTF8;
            }

            if (mailMessage.BodyEncoding == null)
            {
                mailMessage.BodyEncoding = Encoding.UTF8;
            }
        }
    }
}
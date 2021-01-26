using System.Net.Mail;
using System.Threading.Tasks;

namespace NetWorks.Core.Net.Mail
{
    public interface IEmailSender
    {
        Task SendAsync(string to, string subject, string body, bool isBodyHtml = true);

        void Send(string to, string subject, string body, bool isBodyHtml = true);

        Task SendAsync(string from, string to, string subject, string body, bool isBodyHtml = true);

        void Send(string from, string to, string subject, string body, bool isBodyHtml = true);

        Task SendAsync(MailMessage message, bool normalize = true);

        void Send(MailMessage message, bool normalize = true);
    }
}
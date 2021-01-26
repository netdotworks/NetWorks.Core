using System.Net.Mail;

namespace NetWorks.Core.Net.Mail.Smtp
{
    public interface ISmtpEmailSender : IEmailSender
    {
        SmtpClient BuildSmtpClient();
    }
}
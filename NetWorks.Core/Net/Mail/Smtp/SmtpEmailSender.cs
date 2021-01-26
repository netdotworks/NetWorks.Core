using Microsoft.Extensions.Options;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace NetWorks.Core.Net.Mail.Smtp
{
    public class SmtpEmailSender : BaseEmailSender, ISmtpEmailSender
    {
        private readonly ISmtpEmailSenderConfiguration _configuration;

        public SmtpEmailSender(IOptions<SmtpEmailSenderConfiguration> configAccessor) : this(configAccessor.Value)
        {
        }

        public SmtpEmailSender(ISmtpEmailSenderConfiguration senderConfiguration) : base(senderConfiguration)
        {
            _configuration = senderConfiguration;
        }

        public SmtpClient BuildSmtpClient()
        {
            var host = _configuration.Host;
            var port = _configuration.Port;

            var smtpClient = new SmtpClient(host, port);
            try
            {
                if (_configuration.EnableSsl)
                {
                    smtpClient.EnableSsl = true;
                }

                if (_configuration.UseDefaultCredentials)
                {
                    smtpClient.UseDefaultCredentials = true;
                }
                else
                {
                    smtpClient.UseDefaultCredentials = false;
                    var username = _configuration.UserName;
                    var password = _configuration.Password;
                    smtpClient.Credentials = new NetworkCredential()
                    {
                        UserName = username,
                        Password = password
                    };
                }

                return smtpClient;
            }
            catch (Exception)
            {
                smtpClient.Dispose();
                throw;
            }
        }

        public override void SendMail(MailMessage mailMessage)
        {
            using (var client = BuildSmtpClient())
            {
                client.Send(mailMessage);
            }
        }

        public override async Task SendMailAsync(MailMessage mailMessage)
        {
            using (var client = BuildSmtpClient())
            {
                await client.SendMailAsync(mailMessage);
            }
        }
    }
}
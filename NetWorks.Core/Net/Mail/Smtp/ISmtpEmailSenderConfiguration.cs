namespace NetWorks.Core.Net.Mail.Smtp
{
    public interface ISmtpEmailSenderConfiguration : IEmailSenderConfiguration
    {
        string Host { get; set; }

        int Port { get; set; }

        string UserName { get; set; }

        string Password { get; set; }

        bool EnableSsl { get; set; }

        bool UseDefaultCredentials { get; set; }
    }
}
namespace NetWorks.Core.Net.Mail
{
    public class EmailSenderConfiguration : IEmailSenderConfiguration
    {
        public string DefaultFromAddress { get; set; }

        public string DefaultFromDisplayName { get; set; }
    }
}
namespace NetWorks.Core.Net.Mail
{
    public static class EmailSettingNames
    {
        public const string DefaultFromAddress = "Net.Email.DefaultFromAddress";
        public const string DefaultFromDisplayName = "Net.Email.DefaultFromDisplayName";

        public static class Smtp
        {
            public const string Host = "Net.Mail.Smtp.Host";

            public const string Port = "Net.Mail.Smtp.Port";

            public const string UserName = "Net.Mail.Smtp.UserName";

            public const string Password = "Net.Mail.Smtp.Password";

            public const string EnableSsl = "Net.Mail.Smtp.EnableSsl";

            public const string UseDefaultCredentials = "Net.Mail.Smtp.UseDefaultCredentials";
        }
    }
}
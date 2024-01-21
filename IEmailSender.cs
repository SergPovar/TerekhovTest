namespace TerekhovTest
{
    
    public interface IEmailSender
    {
        Task<string> SendEmailAsync (string subject, string body, string[] recipients);
    }
}

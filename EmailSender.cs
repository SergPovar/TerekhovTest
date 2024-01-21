using MimeKit;
using MailKit.Net.Smtp;

namespace TerekhovTest
{ /// <summary>
  ///  This class is responsible for sending messages
  /// </summary>
    public class EmailSender : IEmailSender  
    {
        private readonly IConfiguration _config;
        public EmailSender(IConfiguration config)
        {
            _config = config;
        }
        /// <param name="subject">
        ///  Letter subject
        /// </param>
        /// /// <param name="body">
        ///  Letter to be sent
        /// </param>
        /// /// /// <param name="recipients">
        /// List of recipients
        /// </param>
        /// <returns>
        /// Returns "Ok" if the message was sent successfully and "Failde" with a description of the error if sending failed
        /// </returns>
        public async Task<string> SendEmailAsync(string subject, string body,  string[] recipients)
        {
          
            try
            {
                using var emailMessage = new MimeMessage();
                foreach (var item in recipients)
                {
                    emailMessage.From.Add(new MailboxAddress(_config.GetSection("SenderName").Value, _config.GetSection("EmailLogin").Value));
                    emailMessage.To.Add(new MailboxAddress("", item));
                    emailMessage.Subject = subject;
                    emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
                    {
                        Text = body
                    };
                    using (var client = new SmtpClient())
                    {
                        await client.ConnectAsync(_config.GetSection("EmailHost").Value, 465, true);
                        await client.AuthenticateAsync(_config.GetSection("EmailLogin").Value, _config.GetSection("EmailPassword").Value);
                        await client.SendAsync(emailMessage);

                        await client.DisconnectAsync(true);
                    }
                }
                return "Ok";
            }
            catch (Exception e)
            {
                return e.Message;
            }                    
        }
    }
}

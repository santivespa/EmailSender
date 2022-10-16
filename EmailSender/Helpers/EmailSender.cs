using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net;
using System.Net.Mail;

namespace EmailSender.Helpers
{
    public class EmailSender :  IEmailSender
    {
        public string host { get; set; }
        public int port { get; set; }

        public bool enableSSL { get; set; }
        public string userName { get; set; }
        public string password { get; set; }

        public EmailSender(string host, int port, bool enableSSL, string userName, string password)
        {
            this.host = host;
            this.port = port;
            this.enableSSL = enableSSL;
            this.userName = userName;
            this.password = password;
        }

        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var client = new SmtpClient(host, port)
            {
                Credentials = new NetworkCredential(userName, password),
                EnableSsl = enableSSL,
                
            };

            return client.SendMailAsync(
                
                new MailMessage(userName, email, subject, htmlMessage)
                {
                    IsBodyHtml = true
                }
            );
        }
    }
}

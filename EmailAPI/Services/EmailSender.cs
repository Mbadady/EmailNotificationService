using EmailAPI.Contracts;
using EmailAPI.Util;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using System.Net;

namespace EmailAPI.Services
{
    public class EmailSender : IEmailSender
    {
        public async Task SendEmailAsync(string toEmail, string subject, string message)
        {
            var mail = Constants.emailFrom;
            var pw = Constants.emailPassword;

            try
            {
                var email = new MimeMessage();

                email.From.Add(MailboxAddress.Parse(mail));
                email.Subject = subject;
                email.To.Add(MailboxAddress.Parse(toEmail));
                email.Body = new TextPart(TextFormat.Html) { Text = message };


                using var smtp = new SmtpClient();

                await smtp.ConnectAsync(Constants.emailHost, Constants.emailPort, SecureSocketOptions.StartTls);

                await smtp.AuthenticateAsync(Constants.emailFrom, Constants.emailPassword);

                await smtp.SendAsync(email);

                await smtp.DisconnectAsync(true);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
           
        }
    }
}

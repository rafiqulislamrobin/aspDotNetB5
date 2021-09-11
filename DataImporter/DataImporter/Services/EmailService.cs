
using Microsoft.AspNetCore.Identity.UI.Services;

using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading.Tasks;


namespace DataImporter.Services
{
    public class EmailSender : IEmailService
    {
        public EmailSender()
        {

        }

        public void SendEmailAsync(string email, string subject, string htmlMessage)
        {
            string fromMail = "kratosrobin467@gmail.com";
            string fromPassword = "letthewarbegin946";

            MailMessage message = new MailMessage();
            message.From = new MailAddress(fromMail);
            message.Subject = subject;
            message.To.Add(new MailAddress(email));
            message.Body = "<html><body> " + htmlMessage + " </body></html>";
            message.IsBodyHtml = true;

            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(fromMail, fromPassword),
                EnableSsl = true,
            };
            smtpClient.Send(message);
        }

    }
}

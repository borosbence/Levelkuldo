using System.Net;
using System.Net.Mail;

namespace Levelkuldo.Services
{
    public class EmailService
    {
        public async Task SendEmailAsync(string from, string to, string subject, string body)
        {
            // https://blog.elmah.io/how-to-send-emails-from-csharp-net-the-definitive-tutorial/
            using (SmtpClient client = new SmtpClient())
            {
                string username = "FELHASZNÁLÓNÉV";
                string password = "JELSZÓ";

                client.Host = "smtp.gmail.com";
                client.Port = 587;
                // client.Host = "smtp.gmail.com";
                // client.Port = 587;
                // client.Timeout = 10000;
                // client.DeliveryMethod = SmtpDeliveryMethod.Network;
                // client.UseDefaultCredentials = false;
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential(username, password);

                await client.SendMailAsync(from, to, subject, body);
            }
        }
    }
}

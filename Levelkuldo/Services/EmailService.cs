using System.Net;
using System.Net.Mail;

namespace Levelkuldo.Services
{
    public class EmailService
    {
        public async Task SendEmailAsync(string from, string to, string subject, string body)
        {
            using (SmtpClient client = new SmtpClient())
            {
                string username = "boros.bence@vasvari.org";
                string password = "C6IO5Gpcw7Nhf9F0";

                client.Host = "smtp-relay.sendinblue.com";
                client.Port = 587;
                // client.Host = "smtp.gmail.com";
                // client.Port = 587;
                // client.Timeout = 10000;
                // client.DeliveryMethod = SmtpDeliveryMethod.Network;
                // client.UseDefaultCredentials = false;
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential(username, password);

                try
                {
                    await client.SendMailAsync(from, to, subject, body);
                    LogService.Insert($"Sikeres üzenet küldés: {to} .");
                }
                catch (Exception ex)
                {
                    LogService.Insert($"Hiba: {ex.Message}");
                }
            }
        }
    }
}

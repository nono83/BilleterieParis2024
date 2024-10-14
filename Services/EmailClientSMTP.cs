using System.Net.Mail;

namespace BilleterieParis2024.Services
{
    public class EmailClientSMTP
    {
        public async Task<bool> SendEmailAsync(string email, string subject, string body)
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.To.Add(email);
                mail.From = new MailAddress("arnault_gianati@hotmail.com");
                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = true;

                SmtpClient smtpClient = new SmtpClient();
                smtpClient.Host = "smtp-mail.outlook.com";
                smtpClient.Port = 587;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new System.Net.NetworkCredential("arnault_gianati@hotmail.com", "xxxxxxxxx"); // Enter seders User name and password  
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.EnableSsl = true;
                smtpClient.Send(mail);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Erreur sur l'envoi du mail : '{e}'");
                return false;
            }
        }
    }
}

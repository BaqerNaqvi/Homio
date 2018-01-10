using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace HmsServices
{
    public static class HmsEmailService
    {
        public static void SendEmail(string toEmail, string subject, string body)
        {
            try
            {
                const string fromEmail = "doctors@delivers.pk";
                var message = new MailMessage
                {
                    From = new MailAddress(fromEmail),
                    To = { toEmail },
                    Subject = subject,
                    Body = body,
                    DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure
                };              
                using (SmtpClient smtpClient = new SmtpClient("webmail.delivers.pk")) 
                {                   
                    smtpClient.Credentials = new NetworkCredential("doctors@delivers.pk", "id2Eq*70");
                    smtpClient.Port = 25;
                    smtpClient.EnableSsl = false;
                    smtpClient.Send(message);
                }
            }
            catch (Exception ffg)
            {

            }
        }
    }
}

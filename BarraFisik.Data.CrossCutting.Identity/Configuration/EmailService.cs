using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;

namespace BarraFisik.Data.CrossCutting.Identity.Configuration
{
    public class EmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            //return ConfigSendGridasync(message);
            return SendMail(message);
        }

        // Implementação de e-mail manual
        private Task SendMail(IdentityMessage message)
        {
            if (ConfigurationManager.AppSettings["Internet"] == "true")
            {
                var text = HttpUtility.HtmlEncode(message.Body);

                var msg = new MailMessage();
                msg.From = new MailAddress("jeffersonshibuya@yahoo.com.br", "EnglishForAll");
                msg.To.Add(new MailAddress(message.Destination));
                msg.Subject = message.Subject;
                msg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(text, null, MediaTypeNames.Text.Plain));
                msg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(text, null, MediaTypeNames.Text.Html));

                var smtpClient = new SmtpClient("smtp.mail.yahoo.com", Convert.ToInt32(587));
                var credentials = new NetworkCredential(ConfigurationManager.AppSettings["ContaDeEmail"],
                    ConfigurationManager.AppSettings["SenhaEmail"]);
                smtpClient.Credentials = credentials;
                smtpClient.EnableSsl = true;
                smtpClient.Send(msg);
            }

            return Task.FromResult(0);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FilmDukkani.Common
{
    public static class MailSender
    {
        public static void SendEmail(string email, string subject, string message)
        {
            //Mail Mesaj
            MailMessage sender = new MailMessage();
            sender.From = new MailAddress("yzl3166@outlook.com", "YZL3166");
            sender.Subject = subject;
            sender.Body = message;
            sender.To.Add(email);



            //Smtp 
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Credentials = new NetworkCredential("yzl3166@outlook.com", "KadikoyYzl--34");
            smtpClient.Port = 587;
            smtpClient.Host = "smtp-mail.outlook.com";
            smtpClient.EnableSsl = true;

            //mail gönderimi
            smtpClient.Send(sender);

        }
    }
}

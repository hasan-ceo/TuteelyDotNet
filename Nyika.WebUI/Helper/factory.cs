using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace Nyika.WebUI.Helper
{
    [Authorize]
    public static class CheckDate
    {
        public static bool chkDate(String date)
        {
            try
            {
                DateTime dt = DateTime.Parse(date);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }

  
    public class EmailSender
    {
        public void Send(string Destination,string Subject, string Body)
        {
            var smtpClient = new SmtpClient();

            smtpClient.EnableSsl = true;
            smtpClient.Host = "auth.smtp.1and1.co.uk";
            smtpClient.Port = 587;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential("info@nyika.co", "Sweet1500!");

            MailMessage mailMessage = new MailMessage(
                                   "Nyika <info@nyika.co>",   // From
                                   Destination,     // To
                                   Subject,          // Subject
                                   Body);                // Body
            mailMessage.IsBodyHtml = true;
            smtpClient.SendMailAsync(mailMessage);

        }
        
    }
}
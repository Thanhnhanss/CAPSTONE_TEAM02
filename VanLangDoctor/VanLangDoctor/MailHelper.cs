using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace VanLangDoctor
{
    public class MailHelper
    {
        public void SendMail(string toEmail, string subject, string content)
        {
            var fromEmail = ConfigurationManager.AppSettings["fromEmail"].ToString();
            var fromEmailName = ConfigurationManager.AppSettings["fromEmailName"].ToString();
            var fromEmailPassword = ConfigurationManager.AppSettings["fromEmailPassword"].ToString();
            var smtpHost = ConfigurationManager.AppSettings["smtpHost"].ToString();
            var smtpPost = ConfigurationManager.AppSettings["smtpPost"].ToString();

            bool EnableSsl = bool.Parse(ConfigurationManager.AppSettings["EnableSsl"].ToString());

            string body = content;
            MailMessage mm = new MailMessage(new MailAddress(fromEmail, fromEmailName), new MailAddress(toEmail));
            mm.Subject = subject;
            mm.IsBodyHtml = true;
            mm.Body = body;

            var client = new SmtpClient();
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential(fromEmail, fromEmailPassword);
            client.Host = smtpHost;
            client.Port = !string.IsNullOrEmpty(smtpPost) ? Convert.ToInt32(smtpPost) : 0;
            client.Send(mm);
        }
    }
}
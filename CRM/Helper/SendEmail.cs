using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CRM.Helper
{
    public class SendEmail
    {
        public bool Compose(string from, string to, string subject, string message)
        {
            try
            {
                using (MailMessage BodyMessage = new MailMessage(from, to))
                {
                    BodyMessage.Subject = subject;
                    //BodyMessage.Bcc.Add("nigam-keshari@priyanet.com");
                    BodyMessage.Priority = MailPriority.High;
                    BodyMessage.IsBodyHtml = true;

                    BodyMessage.Body = message;
                    SmtpClient smtp = new SmtpClient();

                    smtp.Port = 25;
                    smtp.Host = ConfigurationManager.AppSettings["SmtpServer"].ToString();
                    smtp.EnableSsl = false;
                    NetworkCredential NetworkCred = new NetworkCredential(ConfigurationManager.AppSettings["FromEmail"].ToString(), ConfigurationManager.AppSettings["FromPassword"].ToString());
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = NetworkCred;
                    smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                    //smtp.Timeout = 30000;
                    smtp.Send(BodyMessage);
                    return true;

                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        //public bool SendAsyncMail(string from, string to, string subject, string message)
        //{
        //    using (MailMessage BodyMessage = new MailMessage(from, to))
        //    {
        //        BodyMessage.Subject = subject;
        //        //BodyMessage.Bcc.Add("nigam-keshari@priyanet.com");
        //        BodyMessage.Priority = MailPriority.High;
        //        BodyMessage.IsBodyHtml = true;

        //        BodyMessage.Body = message;
        //        SmtpClient smtp = new SmtpClient();

        //        smtp.Port = 25;
        //        smtp.Host = ConfigurationManager.AppSettings["SmtpServer"].ToString();
        //        smtp.EnableSsl = false;
        //        NetworkCredential NetworkCred = new NetworkCredential(ConfigurationManager.AppSettings["FromEmail"].ToString(), ConfigurationManager.AppSettings["FromPassword"].ToString());
        //        smtp.UseDefaultCredentials = true;
        //        smtp.Credentials = NetworkCred;
        //        smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;

        //        Object state = BodyMessage;


        //        //event handler for asynchronous call
        //        smtp.SendCompleted += new SendCompletedEventHandler(smtpClient_SendCompleted);
        //        try
        //        {
        //            smtp.SendAsync(BodyMessage, state);
        //            return true;
        //        }

        //        catch (Exception ex)
        //        {
        //            return false;

        //        }
        //    }
        //}

        public async Task SendEmailasync(string from, string to, string subject, string message)
        {
            using (MailMessage BodyMessage = new MailMessage(from, to))
            {       
                BodyMessage.Subject = subject;        
                //BodyMessage.Bcc.Add("nigam-keshari@priyanet.com");     }
               BodyMessage.Priority = MailPriority.High;
               BodyMessage.IsBodyHtml = true;

                BodyMessage.Body = message;
                SmtpClient smtp = new SmtpClient();

                smtp.Port = 25;
                smtp.Host = ConfigurationManager.AppSettings["SmtpServer"].ToString();
                smtp.EnableSsl = false;
                NetworkCredential NetworkCred = new NetworkCredential(ConfigurationManager.AppSettings["FromEmail"].ToString(), ConfigurationManager.AppSettings["FromPassword"].ToString());
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = NetworkCred;
                smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;

                Object state = BodyMessage;
                await smtp.SendMailAsync(BodyMessage);

               
            }
        }
    }
}
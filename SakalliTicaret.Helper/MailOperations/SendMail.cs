using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using SakalliTicaret.Core.Model;
using SakalliTicaret.Core.Model.Entity;

namespace SakalliTicaret.Helper.MailOperations
{
    public class SendMail:BaseHelper
    {

        public void MailSender(string subject, string body,string fromMail)
        {
            var mailSetting = db.MailSettings.First();
            var fromAddress = new MailAddress(mailSetting.Mail);
            var toAddress = new MailAddress(fromMail);
            using (var smtp = new SmtpClient
            {
                Host = mailSetting.Host,
                Port = 587,
                EnableSsl = false,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, mailSetting.Password)
            })
            {
                using (var message = new MailMessage(fromAddress, toAddress) { Subject = subject, Body = body })
                {
                    message.IsBodyHtml = true;
                    smtp.Send(message);
                }
            }
        }
    }
}

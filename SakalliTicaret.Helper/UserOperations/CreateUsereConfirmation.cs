using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SakalliTicaret.Core.Model.Entity;


namespace SakalliTicaret.Helper.UserOperations
{
    class CreateUsereConfirmation: BaseHelper
    {
        public void SendConfirmationEmail(User user)
        {


            string confirmationGuid = user.UserKey;
            string verifyUrl = "/Account/Verify?Id=" +
                               confirmationGuid;

            string bodyMessage = string.Format("üyeliğiniz başarıyla oluşturulmuştur. Aşağıdaki linke tıkladığınızda hesabınızın aktif olacaktır.\n");
            bodyMessage += verifyUrl;

            //var message = new System.Net.Mail.MailMessage(Models.Configuration.SystemMail, user.Email)
            //{
            //    Subject = "Üyeliğinizi doğrulayın.",
            //    Body = bodyMessage
            //};

            //var client = new System.Net.Mail.SmtpClient();
            //client.Send(message);

        }
    }
}

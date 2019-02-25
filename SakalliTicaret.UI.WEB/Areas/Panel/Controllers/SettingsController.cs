using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SakalliTicaret.Core.Model.Entity;

namespace SakalliTicaret.UI.WEB.Areas.Panel.Controllers
{
    public class SettingsController : AdminControlerBase
    {
        // GET: Panel/Settings
        public ActionResult MailSettings()
        {
            MailSetting mailSettings=new MailSetting();
            try
            {
                mailSettings = db.MailSettings.First();
            }
            catch (Exception e)
            {
                return View();
            }
       
            return View(mailSettings);

        }
        [HttpPost]
        public ActionResult MailSettings(MailSetting mailSetting)
        {
            if (mailSetting.Id == 0)
            {
                mailSetting.CreateDateTime=DateTime.Now;
                db.MailSettings.Add(mailSetting);
            }
            else
            {
                db.Entry(mailSetting).State = EntityState.Modified;
            }
            db.SaveChanges();
            var mailSettings = db.MailSettings.First();
            return View(mailSettings);
        }
    }
}
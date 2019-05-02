using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SakalliTicaret.Core.Model.Entity;

namespace SakalliTicaret.UI.WEB.Areas.Panel.Controllers
{
    public class SettingsController : AdminControlerBase
    {
        private Functions _functions = new Functions();
        public ActionResult Settings()
        {
            return View();
        }
        public ActionResult GoogleSettings()
        {
            Settings settings = new Settings();
            try
            {
                settings = db.Settings.First();
            }
            catch (Exception e)
            {
                return View();
            }

            return View(settings);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult GoogleSettings(Settings setting)
        {
            if (setting.Id == 0)
            {
                setting.CreateDateTime = DateTime.Now;
                db.Settings.Add(setting);
            }
            else
            {
                //if (logoImage != null)
                //{
                //    string dosyaYolu = Path.GetFileName(logoImage.FileName);
                //    var yuklemeYeri = Path.Combine(Server.MapPath("~/Dosyalar"), dosyaYolu);
                //    logoImage.SaveAs(yuklemeYeri);
                //}
             
                db.Entry(setting).State = EntityState.Modified;
            }
            db.SaveChanges();
            var dbSetting = db.Settings.First();
            return View(dbSetting);
        }
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
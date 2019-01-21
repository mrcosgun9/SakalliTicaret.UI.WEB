using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SakalliTicaret.Core.Model;
using SakalliTicaret.Core.Model.Entity;

namespace SakalliTicaret.UI.WEB.Areas.Panel.Controllers
{
    public class PortalController : AdminControlerBase
    {
        private SakalliTicaretDb db = new SakalliTicaretDb();
        // GET: Panel/Portal
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ContactEdit()
        {
            Contact contact = db.Contacts.Find(1);
            if (contact == null)
            {
                return View();
            }
            return View(contact);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ContactEdit([Bind(Include = "ID,Phone,Phone1,Mail,Mail2,Adrress,CreateDateTime,CreateUserId,UpdateDateTime,UpdateUserId,IframeUrl")] Contact contact)
        {
            if (contact.Id != 0)
            {
                db.Entry(contact).State = EntityState.Modified;
            }
            else
            {
                contact.CreateUserId = AdminLoginUserId;
                contact.CreateDateTime=DateTime.Now;
                db.Contacts.Add(contact);
            }
            db.SaveChanges();
            return View(contact);
        }
    }
}
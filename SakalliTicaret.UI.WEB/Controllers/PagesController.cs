using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SakalliTicaret.Core.Model;
using SakalliTicaret.Core.Model.Entity;

namespace SakalliTicaret.UI.WEB.Controllers
{
    public class PagesController : Controller
    {
        private SakalliTicaretDb db = new SakalliTicaretDb();
        // GET: Pages
        [Route("Iletisim")]
        public ActionResult Contact()
        {
            Contact contact = db.Contacts.FirstOrDefault();
            ViewBag.iframe = contact.IframeUrl;
            ViewBag.adrress = contact.Adrress;
            ViewBag.mail = contact.Mail;
            ViewBag.phone = contact.Phone;
            return View();
        }
        [HttpPost]
        public ActionResult Contact(Messages messages)
        {
            return View();
        }
    }
}
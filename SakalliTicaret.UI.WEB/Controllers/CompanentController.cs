using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SakalliTicaret.Core.Model;
using SakalliTicaret.UI.WEB.Controllers.Base;
using SakalliTicaret.UI.WEB.Models;

namespace SakalliTicaret.UI.WEB.Controllers
{
    public class CompanentController : BaseControler
    {
        SakalliTicaretDb db = new SakalliTicaretDb();
        // GET: Companent
        public ActionResult SliderMenu()
        {
            var Category = db.Categories.OrderBy(x=>x.Name).ToList();
            return View(Category);
        }

        public ActionResult IndexProcudt(FilterModel filterModel)
        {

            var model = db.Products.OrderBy(x=>x.Name).ToList();
            return View(model);
        }
        public ActionResult SearcCategory()
        {
            var Category = db.Categories.ToList();
            return View(Category);
        }
        public ActionResult FooterSocialMedia()
        {
            var socialMedia = db.SocialMediae.ToList();
            return View(socialMedia);
        }

        public ActionResult HomeFooter()
        {
            var contact = db.Contacts.Find(1);
            return View(contact);
        }

        public ActionResult HomeUserInfo()
        {
            return View();
        }

        public ActionResult HomeTopMenu()
        {
            var model = db.Pages.Where(x => x.IsActive).ToList();
            return View(model);
        }


    }
}
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SakalliTicaret.Core.Model;
using SakalliTicaret.Core.Model.Entity;
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
            List<Category> Category;
            var categoryId = RouteData.Values["Category"];
            if (categoryId != null)
            {
                int cId = Convert.ToInt16(categoryId);
                Category = db.Categories.Where(x => x.ParentCategoryId == cId).OrderBy(x => x.Name).ToList();
                Category parentCategory = db.Categories.Find(cId);
                ViewBag.ParentCategory = parentCategory;

            }
            else
            {
                Category = db.Categories.Where(x=>x.ParentCategoryId==null).OrderBy(x => x.Name).ToList();
            }
            return View(Category);
        }

        public ActionResult IndexProcudt(FilterModel filterModel)
        {

            var model = db.Products.OrderBy(x => x.Name).ToList();
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
        public ActionResult Script()
        {
            Settings settings = db.Settings.First();
            return View(settings);
        }
    }
}
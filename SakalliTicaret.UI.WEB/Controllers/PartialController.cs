using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SakalliTicaret.Core.Model;

namespace SakalliTicaret.UI.WEB.Controllers
{
    public class PartialController : Controller
    {
        SakalliTicaretDb db=new SakalliTicaretDb();
        // GET: Partial
        public ActionResult SliderMenu()
        {
            var Category = db.Categories.ToList();
            return View(Category);
        }

     
    }
}
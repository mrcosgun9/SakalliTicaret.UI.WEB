using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SakalliTicaret.Core.Model;
using SakalliTicaret.UI.WEB.Controllers.Base;

namespace SakalliTicaret.UI.WEB.Controllers
{
    public class HomeController : BaseControler
    {
        private SakalliTicaretDb db = new SakalliTicaretDb();
        // GET: Home
        [Route("Anasayfa")]
        [Route]
        public ActionResult Index()
        {
            return View();
        }
        [Route("Sayfa/{id}/{title}")]
        public ActionResult Page(int id)
        {
            var model = db.Pages.Find(id);
            return View(model);
        }
    }
}
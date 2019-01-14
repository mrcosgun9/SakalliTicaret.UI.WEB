using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SakalliTicaret.Core.Model;
using SakalliTicaret.UI.WEB.Controllers.Base;

namespace SakalliTicaret.UI.WEB.Controllers
{
    public class CategoryController : BaseControler
    {
        private SakalliTicaretDb db = new SakalliTicaretDb();
        // GET: Category
        [Route("Kategori/{id}/{name}")]
        public ActionResult Index(int id, string name)
        {
            var model = db.Products.Where(x => x.CategoryId == id).ToList();
            return View(model);
        }
    }
}
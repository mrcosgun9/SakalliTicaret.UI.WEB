using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using SakalliTicaret.Core.Model;
using SakalliTicaret.UI.WEB.Controllers.Base;

namespace SakalliTicaret.UI.WEB.Controllers
{
    public class ProductController : BaseControler
    {
        SakalliTicaretDb db = new SakalliTicaretDb();
        // GET: Product


        [Route("Urun/{id}/{title}")]
        public ActionResult Detail(int id, string title)
        {
            var model = db.Products
                .Include(p => p.Category).Select(x => x.Category.CategoryProperties).First();

            return View(model);
        }
        [Route("Urunler/YeniUrunler")]
        public ActionResult NewProductList()
        {
            var model = db.Products.OrderBy(x => x.Id).ToList().Take(30);
            return View(model);
        }
    }

}
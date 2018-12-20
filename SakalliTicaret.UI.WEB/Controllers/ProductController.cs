using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SakalliTicaret.Core.Model;
using SakalliTicaret.UI.WEB.Controllers.Base;

namespace SakalliTicaret.UI.WEB.Controllers
{
    public class ProductController : BaseControler
    {
        SakalliTicaretDb db = new SakalliTicaretDb();
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }
        [Route("Urun/{id}/{title}")]
        public ActionResult Detail(int id,string title)
        {
            var model = db.Products.Find(id);
            return View(model);
        }
        [Route("Urunler/YeniUrunler")]
        public ActionResult NewProductList()
        {
            var model = db.Products.OrderBy(x=>x.ID).ToList().Take(30);
            return View(model);
        }
    }
}
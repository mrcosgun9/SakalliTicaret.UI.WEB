using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using SakalliTicaret.Core.Model;
using SakalliTicaret.Core.Model.Entity;
using SakalliTicaret.UI.WEB.Controllers.Base;
using SakalliTicaret.UI.WEB.Models;

namespace SakalliTicaret.UI.WEB.Controllers
{
    public class ProductController : BaseControler
    {
        SakalliTicaretDb db = new SakalliTicaretDb();
        // GET: Product


        [Route("Urun/{id}/{title}")]
        public ActionResult Detail(int id, string title)
        {
            ProductDetailModel detailModel=new ProductDetailModel();
            detailModel.Product= db.Products
                .Include(p => p.Category).FirstOrDefault(x => x.Id == id);
            detailModel.CategoryProperties= db.CategoryProperties.Include(c => c.Category)
                .Where(x => x.CategoryId == detailModel.Product.CategoryId).ToList();
            
            return View(detailModel);
        }
        [Route("Urunler/YeniUrunler")]
        public ActionResult NewProductList()
        {
            var model = db.Products.OrderBy(x => x.Id).ToList().Take(30);
            return View(model);
        }
    }

}
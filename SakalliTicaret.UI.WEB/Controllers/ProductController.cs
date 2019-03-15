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
            ProductDetailModel detailModel = new ProductDetailModel();

            detailModel.Product = db.Products
                .Include(p => p.Category).FirstOrDefault(x => x.Id == id);
            detailModel.ProductProperties = db.ProductProperties.Where(x => x.ProductId == id).Include(x => x.Product).Include(x => x.PropertyPropertyValues).ToList();

            var propIdList = db.CategoryProperties.Where(x => x.CategoryId == detailModel.Product.CategoryId).Select(x => x.Id).ToList();


            detailModel.PropertyPropertyValueses = db.PropertyPropertyValueses
                .Include(x => x.CategoryProperty).Include(x => x.CategoryPropertyValue)
                .Where(x => propIdList.Contains(x.CategoryPropertyId) && x.CategoryProperty.Eligible).ToList();

            detailModel.ProductProperties =
                db.ProductProperties.Where(x => x.ProductId == id).Include(x => x.PropertyPropertyValues).Include(x => x.PropertyPropertyValues.CategoryProperty).Include(x => x.PropertyPropertyValues.CategoryPropertyValue).ToList();

            detailModel.FeaturedProduct = db.Products
                .Where(x => x.CategoryId == detailModel.Product.CategoryId && x.Id != id).OrderBy(r => Guid.NewGuid())
                .Take(4).ToList();



            detailModel.ProductImageses = db.ProductImageses.Where(x => x.ProductId == id).ToList();

            return View(detailModel);
        }
        [Route("Urunler/YeniUrunler")]
        public ActionResult NewProductList()
        {
            var model = db.Products.OrderByDescending(x => x.Id).ToList().Take(30);
            return View(model);
        }
    }

}
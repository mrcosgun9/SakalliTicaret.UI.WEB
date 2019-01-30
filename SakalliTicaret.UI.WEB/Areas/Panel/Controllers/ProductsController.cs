using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SakalliTicaret.Core.Model;
using SakalliTicaret.Core.Model.Entity;
using SakalliTicaret.UI.WEB.App_Class;
using SakalliTicaret.UI.WEB.Areas.Panel.Models;

namespace SakalliTicaret.UI.WEB.Areas.Panel.Controllers
{
    public class ProductsController : AdminControlerBase
    {
        private SakalliTicaretDb db = new SakalliTicaretDb();
        LogClass _logClass = new LogClass();
        private Functions _functions = new Functions();
        // GET: Panel/Products
        public ActionResult Index()
        {
            var products = db.Products.Include(p => p.Category).OrderByDescending(x => x.Id);
            return View(products.ToList());
        }

        // GET: Panel/Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Panel/Products/Create
        public ActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(db.Categories, "ID", "Name");
            return View();
        }

        // POST: Panel/Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(Product product, HttpPostedFileBase ProductImg, int[] SelectedProperty)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var User = Session["AdminLoginUser"] as User;
                    product.CreateDateTime = DateTime.Now;
                    product.CreateUserId = User.Id;
                    if (ProductImg != null)
                    {
                        product.ImageUrl = "/Content/Images/Products/" + _functions.ImageUpload(ProductImg, 300, "~/Content/Images/Products/");
                    }
                    else
                    {
                        product.ImageUrl = "/Content/Images/Products/resimyok.jpg";
                    }
                    db.Products.Add(product);
                    db.SaveChanges();
                    List<PropertyPropertyValues> propertyPropertyValues = new List<PropertyPropertyValues>();
                    for (int i = 0; i < SelectedProperty.Length; i++)
                    {
                        ProductProperty productProperty = new ProductProperty();
                        productProperty.ProductId = product.Id;
                        productProperty.CreateDateTime = DateTime.Now;
                        productProperty.CreateUserId = User.Id;
                        int propId = SelectedProperty[i];
                        productProperty.PropertyPropertyValuesId = db.PropertyPropertyValueses.FirstOrDefault(x => x.CategoryPropertyValueId == propId).Id;
                        db.ProductProperties.Add(productProperty);
                    }

                    db.SaveChanges();
                    //_logClass.ProductLog(product, "Ekleme");
                    return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                ViewBag.hata = e.Message;
            }

            ViewBag.CategoryID = new SelectList(db.Categories, "ID", "Name", product.CategoryId);
            return View(product);
        }

        // GET: Panel/Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "ID", "Name", product.CategoryId);
            return View(product);
        }

        // POST: Panel/Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(Product product, HttpPostedFileBase ProductImg, int[] SelectedProperty)
        {
            if (ModelState.IsValid)
            {
                var User = Session["AdminLoginUser"] as User;
                if (ProductImg != null && product.ImageUrl == "/Content/Images/Products/resimyok.jpg")
                {
                    product.ImageUrl = "/Content/Images/Products/" + _functions.ImageUpload(ProductImg, 300, "~/Content/Images/Products/");
                }

                db.Entry(product).State = EntityState.Modified;
                db.ProductProperties.RemoveRange(db.ProductProperties.Where(x => x.ProductId == product.Id));

                db.SaveChanges();
                for (int i = 0; i < SelectedProperty.Length; i++)
                {
                    ProductProperty productProperty = new ProductProperty();
                    productProperty.ProductId = product.Id;
                    productProperty.CreateDateTime = DateTime.Now;
                    productProperty.CreateUserId = User.Id;
                    int propId = SelectedProperty[i];
                    productProperty.PropertyPropertyValuesId = db.PropertyPropertyValueses.FirstOrDefault(x => x.CategoryPropertyValueId == propId).Id;
                    db.ProductProperties.Add(productProperty);
                }
                db.SaveChanges();
                //_logClass.ProductLog(product, "Düzenleme");
                return RedirectToAction("Index");
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "ID", "Name", product.CategoryId);
            return View(product);
        }

        // GET: Panel/Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);

            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Panel/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            if (product.ImageUrl != "/Content/Images/Products/resimyok.jpg")
            {
                if (System.IO.File.Exists(Server.MapPath(product.ImageUrl)))
                    System.IO.File.Delete(Server.MapPath(product.ImageUrl));
            }
            db.ProductProperties.RemoveRange(db.ProductProperties.Where(x => x.ProductId == product.Id));
            db.Products.Remove(product);
            db.SaveChanges();
            //_logClass.ProductLog(product, "Silme");
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult CategoryPropertyGet(int id)
        {
            int pid = -1;
            try
            {
                string strid = Request.UrlReferrer.Segments[4];
                pid = Convert.ToInt16(strid);
            }
            catch (Exception e)
            {


            }

            //List<CategoryProperty> categoryProperties = db.CategoryProperties.Where(x => x.CategoryId == id).ToList();
            //List<PropertyPropertyValues> propertyPropertyValues =new List<PropertyPropertyValues>();
            //foreach (var item in categoryProperties)
            //{
            // propertyPropertyValues.Add(db.PropertyPropertyValueses.Where(x => x.CategoryPropertyId == item.Id).Include(x => x.CategoryProperty).Include(x => x.CategoryPropertyValue).FirstOrDefault());   
            //}
            ProductPropertiesModel propertiesModel = new ProductPropertiesModel();
            var propIdList = db.CategoryProperties.Where(x => x.CategoryId == id).Select(x => x.Id).ToList();
            propertiesModel.PropertyPropertyValueses = db.PropertyPropertyValueses
               .Include(x => x.CategoryProperty).Include(x => x.CategoryPropertyValue)
               .Where(x => propIdList.Contains(x.CategoryPropertyId) && x.CategoryProperty.Eligible).ToList();

            propertiesModel.ProductProperties = db.ProductProperties.Where(x => x.ProductId == pid).ToList();


            return PartialView("CategoryPropertyGet", propertiesModel);
        }

        public ActionResult MultipleImageUpload(int id)
        {
            List<ProductImages> productImages = db.ProductImageses.Where(x => x.ProductId == id).ToList();
            return View(productImages);
        }

        public ActionResult ImagesRemove(int id)
        {
            ProductImages productImages = db.ProductImageses.FirstOrDefault(x => x.Id == id);
            if (System.IO.File.Exists(Server.MapPath(productImages.ImagesUrl)))
                System.IO.File.Delete(Server.MapPath(productImages.ImagesUrl));
            db.ProductImageses.Remove(productImages);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult ImageUpload()
        {

            var User = Session["AdminLoginUser"] as User;
            int pid = Convert.ToInt16(Request.UrlReferrer.Segments[4]);
            bool isSavedSuccessfully = true;
            string fName = "";
            try
            {
                foreach (string fileName in Request.Files)
                {
                    HttpPostedFileBase file = Request.Files[fileName];
                    fName = file.FileName;
                    if (file != null && file.ContentLength > 0)
                    {

                        //var path = Path.Combine(Server.MapPath("~/MyImages"));
                        //string pathString = System.IO.Path.Combine(path.ToString());
                        //bool isExists = System.IO.Directory.Exists(pathString);
                        //if (!isExists) System.IO.Directory.CreateDirectory(pathString);
                        //var uploadpath = string.Format("{0}\\{1}", pathString, file.FileName);
                        //file.SaveAs(uploadpath);

                        ProductImages productImages = new ProductImages();
                        productImages.ProductId = pid;
                        productImages.CreateDateTime = DateTime.Now;
                        productImages.CreateUserId = User.Id;
                        productImages.ImagesUrl = "/Content/Images/Products/" + _functions.ImageUpload(file, 1000, "~/Content/Images/Products/");
                        db.ProductImageses.Add(productImages);
                    }
                }
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                isSavedSuccessfully = false;
            }
            if (isSavedSuccessfully)
            {
                return Json(new
                {
                    Message = fName
                });
            }
            else
            {
                return Json(new
                {
                    Message = "Error in saving file"
                });
            }
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

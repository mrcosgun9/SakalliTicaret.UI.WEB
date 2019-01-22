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
            var products = db.Products.Include(p => p.Category);
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
        public ActionResult Create([Bind(Include = "ID,Name,CategoryID,Brand,Model,ImageUrl,Description,Definition,Price,Tax,Discount,Stock,IsActive,CreateDateTime,CreateUserID,UpdateDateTime,UpdateUserID")] Product product, HttpPostedFileBase ProductImg)
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
                        //string ImageName = Path.GetFileName(ProductImg.FileName);
                        //string physicalPath = Server.MapPath("~/Content/Images/Products/" + ImageName);

                        //// save image in folder
                        //ProductImg.SaveAs(physicalPath);
                        product.ImageUrl = "/Content/Images/Products/" + _functions.ImageUpload(ProductImg, 300);
                    }
                    else
                    {
                        product.ImageUrl = "/Content/Images/Products/resimyok.jpg";
                    }
                    db.Products.Add(product);
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
        public ActionResult Edit([Bind(Include = "ID,Name,CategoryID,Brand,Model,ImageUrl,Description,Price,Tax,Discount,Stock,IsActive,CreateDateTime,CreateUserID,UpdateDateTime,UpdateUserID")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                _logClass.ProductLog(product, "Düzenleme");
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

            db.Products.Remove(product);
            db.SaveChanges();
            //_logClass.ProductLog(product, "Silme");
            return RedirectToAction("Index");
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

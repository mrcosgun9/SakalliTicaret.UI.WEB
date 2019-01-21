using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SakalliTicaret.Core.Model;
using SakalliTicaret.Core.Model.Entity;

namespace SakalliTicaret.UI.WEB.Areas.Panel.Controllers
{
    public class LogProductsController : AdminControlerBase
    {
        private SakalliTicaretDb db = new SakalliTicaretDb();

        // GET: Panel/LogProducts
        public ActionResult Index()
        {
            //var logProducts = db.LogProducts.Include(l => l.Category);
            var logProducts = db.LogProducts;
            return View(logProducts.ToList());
        }

        // GET: Panel/LogProducts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LogProduct logProduct = db.LogProducts.Find(id);
            if (logProduct == null)
            {
                return HttpNotFound();
            }
            return View(logProduct);
        }

        // GET: Panel/LogProducts/Create
        public ActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(db.Categories, "ID", "Name");
            return View();
        }

        // POST: Panel/LogProducts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,CategoryID,Brand,Model,ImageUrl,Description,Definition,Price,Tax,Discount,Stock,IsActive,CreateDateTime,CreateUserId,Actions")] LogProduct logProduct)
        {
            if (ModelState.IsValid)
            {
                db.LogProducts.Add(logProduct);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryID = new SelectList(db.Categories, "ID", "Name", logProduct.CategoryId);
            return View(logProduct);
        }

        // GET: Panel/LogProducts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LogProduct logProduct = db.LogProducts.Find(id);
            if (logProduct == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "ID", "Name", logProduct.CategoryId);
            return View(logProduct);
        }

        // POST: Panel/LogProducts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,CategoryID,Brand,Model,ImageUrl,Description,Definition,Price,Tax,Discount,Stock,IsActive,CreateDateTime,CreateUserId,Actions")] LogProduct logProduct)
        {
            if (ModelState.IsValid)
            {
                db.Entry(logProduct).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "ID", "Name", logProduct.CategoryId);
            return View(logProduct);
        }

        // GET: Panel/LogProducts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LogProduct logProduct = db.LogProducts.Find(id);
            if (logProduct == null)
            {
                return HttpNotFound();
            }
            return View(logProduct);
        }

        // POST: Panel/LogProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LogProduct logProduct = db.LogProducts.Find(id);
            db.LogProducts.Remove(logProduct);
            db.SaveChanges();
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

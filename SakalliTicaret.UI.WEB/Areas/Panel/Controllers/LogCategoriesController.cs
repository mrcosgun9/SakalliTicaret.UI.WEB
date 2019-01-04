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
    public class LogCategoriesController : AdminControlerBase
    {
        private SakalliTicaretDb db = new SakalliTicaretDb();

        // GET: Panel/LogCategories
        public ActionResult Index()
        {
            return View(db.LogCategories.ToList());
        }

        // GET: Panel/LogCategories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LogCategory logCategory = db.LogCategories.Find(id);
            if (logCategory == null)
            {
                return HttpNotFound();
            }
            return View(logCategory);
        }

        // GET: Panel/LogCategories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Panel/LogCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ParentId,Name,IsActive,CreateDateTime,CreateUserID,Actions")] LogCategory logCategory)
        {
            if (ModelState.IsValid)
            {
                db.LogCategories.Add(logCategory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(logCategory);
        }

        // GET: Panel/LogCategories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LogCategory logCategory = db.LogCategories.Find(id);
            if (logCategory == null)
            {
                return HttpNotFound();
            }
            return View(logCategory);
        }

        // POST: Panel/LogCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ParentId,Name,IsActive,CreateDateTime,CreateUserID,Actions")] LogCategory logCategory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(logCategory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(logCategory);
        }

        // GET: Panel/LogCategories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LogCategory logCategory = db.LogCategories.Find(id);
            if (logCategory == null)
            {
                return HttpNotFound();
            }
            return View(logCategory);
        }

        // POST: Panel/LogCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LogCategory logCategory = db.LogCategories.Find(id);
            db.LogCategories.Remove(logCategory);
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

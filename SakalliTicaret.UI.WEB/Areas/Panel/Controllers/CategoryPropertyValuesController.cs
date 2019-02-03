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
    public class CategoryPropertyValuesController : AdminControlerBase
    {
        private SakalliTicaretDb db = new SakalliTicaretDb();

        // GET: Panel/CategoryPropertyValues
        public ActionResult Index()
        {
            var categoryPropertyValues = db.CategoryPropertyValues.Include(c => c.CategoryProperty);
            return View(categoryPropertyValues.ToList());
        }

        // GET: Panel/CategoryPropertyValues/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoryPropertyValue categoryPropertyValue = db.CategoryPropertyValues.Find(id);
            if (categoryPropertyValue == null)
            {
                return HttpNotFound();
            }
            return View(categoryPropertyValue);
        }

        // GET: Panel/CategoryPropertyValues/Create
        public ActionResult Create()
        {
            ViewBag.CategoryPropertyId = new SelectList(db.CategoryProperties, "ID", "Name");
            return View();
        }

        // POST: Panel/CategoryPropertyValues/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,CategoryPropertyId,Value,CreateDateTime,CreateUserId,UpdateDateTime,UpdateUserId")] CategoryPropertyValue categoryPropertyValue)
        {
            if (ModelState.IsValid)
            {
                //Property Create
                categoryPropertyValue.CreateDateTime = DateTime.Now;
                categoryPropertyValue.CreateUserId = AdminLoginUserId;
                db.CategoryPropertyValues.Add(categoryPropertyValue);
                db.SaveChanges();
                //PropVal Create
                PropertyPropertyValues propertyPropertyValues = new PropertyPropertyValues();
                propertyPropertyValues.CategoryPropertyId = categoryPropertyValue.CategoryPropertyId;
                propertyPropertyValues.CategoryPropertyValueId = categoryPropertyValue.Id;
                propertyPropertyValues.CreateDateTime = DateTime.Now;
                propertyPropertyValues.CreateUserId = AdminLoginUserId;
                db.PropertyPropertyValueses.Add(propertyPropertyValues);

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryPropertyId = new SelectList(db.CategoryProperties, "ID", "Name", categoryPropertyValue.CategoryPropertyId);
            return View(categoryPropertyValue);
        }

        // GET: Panel/CategoryPropertyValues/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoryPropertyValue categoryPropertyValue = db.CategoryPropertyValues.Find(id);
            if (categoryPropertyValue == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryPropertyId = new SelectList(db.CategoryProperties, "ID", "Name", categoryPropertyValue.CategoryPropertyId);
            return View(categoryPropertyValue);
        }

        // POST: Panel/CategoryPropertyValues/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,CategoryPropertyId,Value,CreateDateTime,CreateUserId,UpdateDateTime,UpdateUserId")] CategoryPropertyValue categoryPropertyValue)
        {
            if (ModelState.IsValid)
            {
                db.Entry(categoryPropertyValue).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryPropertyId = new SelectList(db.CategoryProperties, "ID", "Name", categoryPropertyValue.CategoryPropertyId);
            return View(categoryPropertyValue);
        }

        // GET: Panel/CategoryPropertyValues/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoryPropertyValue categoryPropertyValue = db.CategoryPropertyValues.Find(id);
            if (categoryPropertyValue == null)
            {
                return HttpNotFound();
            }
            return View(categoryPropertyValue);
        }

        // POST: Panel/CategoryPropertyValues/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            CategoryPropertyValue categoryPropertyValue = db.CategoryPropertyValues.Find(id);
            db.CategoryPropertyValues.Remove(categoryPropertyValue);
            PropertyPropertyValues propertyPropertyValues =
                db.PropertyPropertyValueses.FirstOrDefault(
                    x => x.CategoryPropertyId == categoryPropertyValue.CategoryPropertyId &&
                         x.CategoryPropertyValueId == categoryPropertyValue.Id);
            if (propertyPropertyValues!=null)
            {
                db.PropertyPropertyValueses.Remove(propertyPropertyValues);

            }
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

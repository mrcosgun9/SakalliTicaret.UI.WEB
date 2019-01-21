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
    public class CategoryPropertiesController : AdminControlerBase
    {
        private SakalliTicaretDb db = new SakalliTicaretDb();

        // GET: Panel/CategoryProperties
        public ActionResult Index()
        {
            var categoryProperties = db.CategoryProperties.Include(c => c.Category);
            return View(categoryProperties.ToList());
        }

        // GET: Panel/CategoryProperties/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoryProperty categoryProperty = db.CategoryProperties.Find(id);
            if (categoryProperty == null)
            {
                return HttpNotFound();
            }
            return View(categoryProperty);
        }

        // GET: Panel/CategoryProperties/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "ID", "Name");
            return View();
        }

        // POST: Panel/CategoryProperties/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,CategoryId,Name,Eligible,CreateDateTime,CreateUserId,UpdateDateTime,UpdateUserId,")] CategoryProperty categoryProperty)
        {
            if (ModelState.IsValid)
            {
                categoryProperty.CreateDateTime = DateTime.Now;
                categoryProperty.CreateUserId = AdminLoginUserId;
                db.CategoryProperties.Add(categoryProperty);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "ID", "Name", categoryProperty.CategoryId);
            return View(categoryProperty);
        }

        // GET: Panel/CategoryProperties/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoryProperty categoryProperty = db.CategoryProperties.Find(id);
            if (categoryProperty == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "ID", "Name", categoryProperty.CategoryId);
            return View(categoryProperty);
        }

        // POST: Panel/CategoryProperties/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,CategoryId,Name,Eligible,CreateDateTime,CreateUserId,UpdateDateTime,UpdateUserId")] CategoryProperty categoryProperty)
        {
            if (ModelState.IsValid)
            {
                db.Entry(categoryProperty).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "ID", "Name", categoryProperty.CategoryId);
            return View(categoryProperty);
        }

        // GET: Panel/CategoryProperties/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoryProperty categoryProperty = db.CategoryProperties.Find(id);
            if (categoryProperty == null)
            {
                return HttpNotFound();
            }
            return View(categoryProperty);
        }

        // POST: Panel/CategoryProperties/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                CategoryProperty categoryProperty = db.CategoryProperties.Find(id);
                List<CategoryPropertyValue> categoryPropertyValue =
                    db.CategoryPropertyValues.Where(x => x.CategoryPropertyId == id).ToList();
                foreach (var item in categoryPropertyValue)
                {
                    db.CategoryPropertyValues.Remove(item);
                  
                    PropertyPropertyValues propertyPropertyValues =
                        db.PropertyPropertyValueses.FirstOrDefault(x => x.CategoryPropertyId == categoryProperty.Id &&
                                                                        x.CategoryPropertyValueId == item.Id);
                    db.PropertyPropertyValueses.Remove(propertyPropertyValues);
                }
                db.CategoryProperties.Remove(categoryProperty);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                return View();
            }
           
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

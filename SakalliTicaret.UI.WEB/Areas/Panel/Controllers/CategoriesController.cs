using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using SakalliTicaret.Core.Model;
using SakalliTicaret.Core.Model.Entity;
using SakalliTicaret.UI.WEB.App_Class;

namespace SakalliTicaret.UI.WEB.Areas.Panel.Controllers
{
    public class CategoriesController : AdminControlerBase
    {
        private SakalliTicaretDb db = new SakalliTicaretDb();
        LogClass _logClass = new LogClass();
        // GET: Panel/Categories
        public ActionResult Index()
        {
            var categories = db.Categories.Include(p => p.ParentCategory).Include(x => x.CreateUser);
            return View(categories.ToList());
        }

        // GET: Panel/Categories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }


        private string AgacOlustur(int parantId)
        {
            string html = "";
            List<Category> subCategories = new List<Category>();
            subCategories = db.Categories.Where(x => x.ParentId == parantId).ToList();
            //DataRow[] altKategoriler = dt.Select("UstKategoriId=" + ustKategoriId);
            if (subCategories.Count == 0) return html;
            html += "<ul>";
            foreach (var item in subCategories)
            {
                int id = item.Id;
                html += "<li>" + @item.Name + "</li>";
                AgacOlustur(id);

            }
            html += "</ul>";
            return html;
        }
        // GET: Panel/Categories/Create
        public ActionResult Create()
        {

            ViewBag.UstKategoriList = db.Categories.ToList();
            return View();
        }

        // POST: Panel/Categories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ParentId,Name,IsActive,CreateDateTime,CreateUserId,UpdateDateTime,UpdateUserId")] Category category)
        {

            var User = Session["AdminLoginUser"] as User;
            category.CreateDateTime = DateTime.Now;
            category.CreateUserId = User.Id;
            if (ModelState.IsValid)
            {
                db.Categories.Add(category);
                db.SaveChanges();
                _logClass.CategoryLog(category, "Ekleme");
                return RedirectToAction("Index");
            }
            ViewBag.UstKategoriList = db.Categories.ToList();
            return View(category);
        }

        // GET: Panel/Categories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            ViewBag.UstKategoriList = db.Categories.ToList();
            return View(category);
        }

        // POST: Panel/Categories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ParentId,Name,IsActive,CreateDateTime,CreateUserId,UpdateDateTime,UpdateUserId")] Category category)
        {
            var User = Session["AdminLoginUser"] as User;
            category.UpdateDateTime = DateTime.Now;
            category.UpdateUserId = User.Id;
            if (ModelState.IsValid)
            {
                db.Entry(category).State = EntityState.Modified;
                db.SaveChanges();
                _logClass.CategoryLog(category, "Düzenleme");
                return RedirectToAction("Index");
            }
            return View(category);
        }

        // GET: Panel/Categories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Include(c => c.CreateUser).FirstOrDefault(x => x.Id == id);


            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Panel/Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Category category = db.Categories.Find(id);
            db.Categories.Remove(category);
            List<CategoryProperty> categoryProperty = db.CategoryProperties.Where(x => x.CategoryId == id).ToList();
            List<CategoryPropertyValue> categoryPropertyValues = new List<CategoryPropertyValue>();
            foreach (var categoryProp in categoryProperty)
            {
                db.Database.ExecuteSqlCommand("delete from PropertyPropertyValues where CategoryPropertyId=" + categoryProp.Id);

                db.Database.ExecuteSqlCommand("delete from CategoryPropertyValues where CategoryPropertyId=" + categoryProp.Id);
            }
            db.Database.ExecuteSqlCommand("delete from CategoryProperties where CategoryId=" + category.Id);
            List<Product> product = db.Products.Where(x => x.CategoryId == id).ToList();
            foreach (var item in product)
            {
                item.CategoryId = null;
                db.Entry(item).State = EntityState.Modified;
            }

            db.SaveChanges();
            _logClass.CategoryLog(category, "Silme");
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

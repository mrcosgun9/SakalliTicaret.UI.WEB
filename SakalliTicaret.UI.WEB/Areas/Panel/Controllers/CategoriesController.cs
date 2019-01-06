﻿using System;
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
            return View(db.Categories.ToList());
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

        private void KategorileriGetir()
        {
            
        }

        private void CategoryGet()
        {
            
        }
        private string AgacOlustur(int parantId)
        {
            string html = "";
            List<Category> subCategories=new List<Category>();
            subCategories = db.Categories.Where(x => x.ParentId == parantId).ToList();
            //DataRow[] altKategoriler = dt.Select("UstKategoriId=" + ustKategoriId);
            if (subCategories.Count == 0) return html;
            html += "<ul>";
            foreach (var item  in subCategories)
            {
                int id = item.ID;
                html += "<li>"+@item.Name+"</li>";
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
        public ActionResult Create([Bind(Include = "ID,ParentId,Name,IsActive,CreateDateTime,CreateUserID,UpdateDateTime,UpdateUserID")] Category category)
        {

            var User = Session["AdminLoginUser"] as User;
            category.CreateDateTime = DateTime.Now;
            category.CreateUserID = User.ID;
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
        public ActionResult Edit([Bind(Include = "ID,ParentId,Name,IsActive,CreateDateTime,CreateUserID,UpdateDateTime,UpdateUserID")] Category category)
        {
            var User = Session["AdminLoginUser"] as User;
            category.UpdateDateTime = DateTime.Now;
            category.UpdateUserID = User.ID;
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
            Category category = db.Categories.Find(id);
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

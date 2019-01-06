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
    public class PosEntegrationsController : Controller
    {
        private SakalliTicaretDb db = new SakalliTicaretDb();

        // GET: Panel/PosEntegrations
        public ActionResult Index()
        {
            return View(db.PosEntegrations.ToList());
        }

        // GET: Panel/PosEntegrations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PosEntegration posEntegration = db.PosEntegrations.Find(id);
            if (posEntegration == null)
            {
                return HttpNotFound();
            }
            return View(posEntegration);
        }

        // GET: Panel/PosEntegrations/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Panel/PosEntegrations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,StoreCode,UserName,Password,Installments,CreateDateTime,CreateUserID,UpdateDateTime,UpdateUserID")] PosEntegration posEntegration)
        {
            if (ModelState.IsValid)
            {
                db.PosEntegrations.Add(posEntegration);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(posEntegration);
        }

        // GET: Panel/PosEntegrations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PosEntegration posEntegration = db.PosEntegrations.Find(id);
            if (posEntegration == null)
            {
                return HttpNotFound();
            }
            return View(posEntegration);
        }

        // POST: Panel/PosEntegrations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,StoreCode,UserName,Password,Installments,CreateDateTime,CreateUserID,UpdateDateTime,UpdateUserID")] PosEntegration posEntegration)
        {
            if (ModelState.IsValid)
            {
                db.Entry(posEntegration).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(posEntegration);
        }

        // GET: Panel/PosEntegrations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PosEntegration posEntegration = db.PosEntegrations.Find(id);
            if (posEntegration == null)
            {
                return HttpNotFound();
            }
            return View(posEntegration);
        }

        // POST: Panel/PosEntegrations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PosEntegration posEntegration = db.PosEntegrations.Find(id);
            db.PosEntegrations.Remove(posEntegration);
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

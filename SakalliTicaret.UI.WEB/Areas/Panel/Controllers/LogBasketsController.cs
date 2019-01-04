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
    public class LogBasketsController : AdminControlerBase
    {
        private SakalliTicaretDb db = new SakalliTicaretDb();

        // GET: Panel/LogBaskets
        public ActionResult Index()
        {
            return View(db.LogBaskets.ToList());
        }

        // GET: Panel/LogBaskets/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LogBasket logBasket = db.LogBaskets.Find(id);
            if (logBasket == null)
            {
                return HttpNotFound();
            }
            return View(logBasket);
        }

        // GET: Panel/LogBaskets/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Panel/LogBaskets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,UserId,ProductId,Quantity,Amount,BasketKey,CreateDateTime,CreateUserID,Actions")] LogBasket logBasket)
        {
            if (ModelState.IsValid)
            {
                db.LogBaskets.Add(logBasket);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(logBasket);
        }

        // GET: Panel/LogBaskets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LogBasket logBasket = db.LogBaskets.Find(id);
            if (logBasket == null)
            {
                return HttpNotFound();
            }
            return View(logBasket);
        }

        // POST: Panel/LogBaskets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,UserId,ProductId,Quantity,Amount,BasketKey,CreateDateTime,CreateUserID,Actions")] LogBasket logBasket)
        {
            if (ModelState.IsValid)
            {
                db.Entry(logBasket).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(logBasket);
        }

        // GET: Panel/LogBaskets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LogBasket logBasket = db.LogBaskets.Find(id);
            if (logBasket == null)
            {
                return HttpNotFound();
            }
            return View(logBasket);
        }

        // POST: Panel/LogBaskets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LogBasket logBasket = db.LogBaskets.Find(id);
            db.LogBaskets.Remove(logBasket);
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

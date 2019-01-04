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
    public class LogUsersController : AdminControlerBase
    {
        private SakalliTicaretDb db = new SakalliTicaretDb();

        // GET: Panel/LogUsers
        public ActionResult Index()
        {
            return View(db.LogUsers.ToList());
        }

        // GET: Panel/LogUsers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LogUsers logUsers = db.LogUsers.Find(id);
            if (logUsers == null)
            {
                return HttpNotFound();
            }
            return View(logUsers);
        }

        // GET: Panel/LogUsers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Panel/LogUsers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,LastName,Email,Telephone,Password,TCKN,Adress,CreateDateTime,CreateUserID,Actions")] LogUsers logUsers)
        {
            if (ModelState.IsValid)
            {
                db.LogUsers.Add(logUsers);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(logUsers);
        }

        // GET: Panel/LogUsers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LogUsers logUsers = db.LogUsers.Find(id);
            if (logUsers == null)
            {
                return HttpNotFound();
            }
            return View(logUsers);
        }

        // POST: Panel/LogUsers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,LastName,Email,Telephone,Password,TCKN,Adress,CreateDateTime,CreateUserID,Actions")] LogUsers logUsers)
        {
            if (ModelState.IsValid)
            {
                db.Entry(logUsers).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(logUsers);
        }

        // GET: Panel/LogUsers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LogUsers logUsers = db.LogUsers.Find(id);
            if (logUsers == null)
            {
                return HttpNotFound();
            }
            return View(logUsers);
        }

        // POST: Panel/LogUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LogUsers logUsers = db.LogUsers.Find(id);
            db.LogUsers.Remove(logUsers);
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

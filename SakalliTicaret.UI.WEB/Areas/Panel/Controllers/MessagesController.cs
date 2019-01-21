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
    public class MessagesController : Controller
    {
        private SakalliTicaretDb db = new SakalliTicaretDb();

        // GET: Panel/Messages
        public ActionResult Index()
        {
            return View(db.Messageses.ToList());
        }

        // GET: Panel/Messages/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Messages messages = db.Messageses.Find(id);
            if (messages == null)
            {
                return HttpNotFound();
            }
            return View(messages);
        }

        // GET: Panel/Messages/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Panel/Messages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,SurName,Phone,Email,MessageTitle,Message,CreateDateTime,CreateUserId,UpdateDateTime,UpdateUserId")] Messages messages)
        {
            if (ModelState.IsValid)
            {
                db.Messageses.Add(messages);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(messages);
        }

        // GET: Panel/Messages/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Messages messages = db.Messageses.Find(id);
            if (messages == null)
            {
                return HttpNotFound();
            }
            return View(messages);
        }

        // POST: Panel/Messages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,SurName,Phone,Email,MessageTitle,Message,CreateDateTime,CreateUserId,UpdateDateTime,UpdateUserId")] Messages messages)
        {
            if (ModelState.IsValid)
            {
                db.Entry(messages).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(messages);
        }

        // GET: Panel/Messages/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Messages messages = db.Messageses.Find(id);
            if (messages == null)
            {
                return HttpNotFound();
            }
            return View(messages);
        }

        // POST: Panel/Messages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Messages messages = db.Messageses.Find(id);
            db.Messageses.Remove(messages);
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

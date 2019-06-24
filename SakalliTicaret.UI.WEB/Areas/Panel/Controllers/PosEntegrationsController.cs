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
        public ActionResult Edit(PosEntegration posEntegration)
        {
            if (ModelState.IsValid)
            {
                db.Entry(posEntegration).State = EntityState.Modified;
                db.SaveChanges();
                return View(posEntegration);
            }
            return View(posEntegration);
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

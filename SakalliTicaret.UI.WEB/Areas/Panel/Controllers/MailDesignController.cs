﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SakalliTicaret.Core.Model.Entity;

namespace SakalliTicaret.UI.WEB.Areas.Panel.Controllers
{
    public class MailDesignController : AdminControlerBase
    {
        // GET: Panel/MailDesign
        public ActionResult Index()
        {
           List<MailDesign> mailDesign = db.MailDesigns.ToList();
            return View(mailDesign);
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MailDesign mailDesign = db.MailDesigns.Find(id);
            if (mailDesign == null)
            {
                return HttpNotFound();
            }
            return View(mailDesign);
        }

        // POST: Panel/Sliders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(MailDesign mailDesign)
        {
            if (ModelState.IsValid)
            {
                var User = Session["AdminLoginUser"] as User;
                db.Entry(mailDesign).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(mailDesign);
        }

    }
}
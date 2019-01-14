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
    public class SocialMediasController : AdminControlerBase
    {
        private SakalliTicaretDb db = new SakalliTicaretDb();

        // GET: Panel/SocialMedias
        public ActionResult Index()
        {
            return View(db.SocialMediae.ToList());
        }

        // GET: Panel/SocialMedias/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SocialMedia socialMedia = db.SocialMediae.Find(id);
            if (socialMedia == null)
            {
                return HttpNotFound();
            }
            return View(socialMedia);
        }

        // GET: Panel/SocialMedias/Create
        public ActionResult Create()
        {

            List<DrIcon> IconList = new List<DrIcon>
            {
                new DrIcon {FaClass = "fa-facebook-f", Name = "FaceBook"},
                new DrIcon {FaClass = "fa-instagram", Name = "Instagram"},
                new DrIcon {FaClass = "fa-youtube", Name = "Youtube"},
                new DrIcon {FaClass = "fa-google-plus", Name = "Google Plus"},
                new DrIcon {FaClass = "fa-pinterest", Name = "Pinterest"},
                new DrIcon {FaClass = "fa-twitter", Name = "Twitter"}
            };
            ViewBag.IconList = IconList;
            return View();
        }

        // POST: Panel/SocialMedias/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Icon,Name,Url,CreateDateTime,CreateUserID,UpdateDateTime,UpdateUserID")] SocialMedia socialMedia)
        {
            if (ModelState.IsValid)
            {
                User user=Session["AdminLoginUser"] as User;
                socialMedia.CreateDateTime = DateTime.Now;
                socialMedia.CreateUserId = user.Id;
                db.SocialMediae.Add(socialMedia);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(socialMedia);
        }

        // GET: Panel/SocialMedias/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SocialMedia socialMedia = db.SocialMediae.Find(id);
            if (socialMedia == null)
            {
                return HttpNotFound();
            }
            return View(socialMedia);
        }

        // POST: Panel/SocialMedias/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Icon,Name,Url,CreateDateTime,CreateUserID,UpdateDateTime,UpdateUserID")] SocialMedia socialMedia)
        {
            if (ModelState.IsValid)
            {
                db.Entry(socialMedia).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(socialMedia);
        }

        // GET: Panel/SocialMedias/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SocialMedia socialMedia = db.SocialMediae.Find(id);
            if (socialMedia == null)
            {
                return HttpNotFound();
            }
            return View(socialMedia);
        }

        // POST: Panel/SocialMedias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SocialMedia socialMedia = db.SocialMediae.Find(id);
            db.SocialMediae.Remove(socialMedia);
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
        public class DrIcon
        {
            public string FaClass { get; set; }

            public string Name { get; set; }
        }

    }
}

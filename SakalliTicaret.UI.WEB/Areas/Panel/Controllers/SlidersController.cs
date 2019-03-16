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
    public class SlidersController : AdminControlerBase
    {
        private SakalliTicaretDb db = new SakalliTicaretDb();
        private Functions _functions = new Functions();
        // GET: Panel/Sliders
        public ActionResult Index()
        {
            var sliders = db.Sliders.Include(s => s.CreateUser);
            return View(sliders.ToList());
        }

        // GET: Panel/Sliders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slider slider = db.Sliders.Find(id);
            if (slider == null)
            {
                return HttpNotFound();
            }
            return View(slider);
        }

        // GET: Panel/Sliders/Create
        public ActionResult Create()
        {
            ViewBag.CreateUserId = new SelectList(db.Users, "Id", "Name");
            return View();
        }

        // POST: Panel/Sliders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Slider slider, HttpPostedFileBase sliderImg)
        {
            if (ModelState.IsValid)
            {
                var User = Session["AdminLoginUser"] as User;
                slider.CreateDateTime = DateTime.Now;
                slider.CreateUserId = User.Id;
                if (sliderImg != null)
                {
                    slider.ImageUrl = "/Content/Images/Sliders/" + _functions.ImageUpload(sliderImg, 700, "~/Content/Images/Sliders/");
                }
                else
                {
                    slider.ImageUrl = "/Content/Images/Products/resimyok.jpg";
                }
                db.Sliders.Add(slider);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(slider);
        }

        // GET: Panel/Sliders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slider slider = db.Sliders.Find(id);
            if (slider == null)
            {
                return HttpNotFound();
            }
            return View(slider);
        }

        // POST: Panel/Sliders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Slider slider, HttpPostedFileBase sliderImg)
        {
            if (ModelState.IsValid)
            {
                var User = Session["AdminLoginUser"] as User;
                if (sliderImg != null || slider.ImageUrl == "/Content/Images/Products/resimyok.jpg")
                {
                    slider.ImageUrl = "/Content/Images/Sliders/" + _functions.ImageUpload(sliderImg, 700, "~/Content/Images/Sliders/");
                }

                db.Entry(slider).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(slider);
        }

        // GET: Panel/Sliders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slider slider = db.Sliders.Find(id);
            if (slider == null)
            {
                return HttpNotFound();
            }
            return View(slider);
        }

        // POST: Panel/Sliders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Slider slider = db.Sliders.Find(id);
            db.Sliders.Remove(slider);
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

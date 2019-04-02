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
using SakalliTicaret.UI.WEB.Controllers.Base;

namespace SakalliTicaret.UI.WEB.Areas.Panel.Controllers
{
    public class CargoCompaniesController : AdminControlerBase
    {


        // GET: Panel/CargoCompanies
        public ActionResult Index()
        {
            var cargoCompanies = db.CargoCompanies.Include(c => c.CreateUser);
            return View(cargoCompanies.ToList());
        }

        // GET: Panel/CargoCompanies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CargoCompany cargoCompany = db.CargoCompanies.Find(id);
            if (cargoCompany == null)
            {
                return HttpNotFound();
            }
            return View(cargoCompany);
        }

        // GET: Panel/CargoCompanies/Create
        public ActionResult Create()
        {
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CargoCompany cargoCompany)
        {
            cargoCompany.CreateDateTime=DateTime.Now;
            cargoCompany.CreateUserId = AdminLoginUserId;
            if (ModelState.IsValid)
            {
                db.CargoCompanies.Add(cargoCompany);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

        
            return View(cargoCompany);
        }

        // GET: Panel/CargoCompanies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CargoCompany cargoCompany = db.CargoCompanies.Find(id);
            if (cargoCompany == null)
            {
                return HttpNotFound();
            }
            ViewBag.CreateUserId = new SelectList(db.Users, "Id", "Name", cargoCompany.CreateUserId);
            return View(cargoCompany);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CargoCompany cargoCompany)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cargoCompany).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CreateUserId = new SelectList(db.Users, "Id", "Name", cargoCompany.CreateUserId);
            return View(cargoCompany);
        }

        // GET: Panel/CargoCompanies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CargoCompany cargoCompany = db.CargoCompanies.Find(id);
            if (cargoCompany == null)
            {
                return HttpNotFound();
            }
            return View(cargoCompany);
        }

        // POST: Panel/CargoCompanies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CargoCompany cargoCompany = db.CargoCompanies.Find(id);
            db.CargoCompanies.Remove(cargoCompany);
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

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SakallaTicaret.Helper;
using SakalliTicaret.Core.Model;
using SakalliTicaret.Core.Model.Entity;
using SakalliTicaret.UI.WEB.App_Class;
using SakalliTicaret.UI.WEB.Controllers.Base;

namespace SakalliTicaret.UI.WEB.Controllers
{
    //todo:Cache aktif
    //[OutputCache(VaryByParam = "none", Duration = 3600)]
    public class UsersController : BaseControler
    {
        private SakalliTicaretDb db = new SakalliTicaretDb();

        // GET: Users
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        [Route("Kullanici/Kayit")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Kullanici/Kayit")]
        public ActionResult Create([Bind(Include = "ID,Name,LastName,Email,ImageUrl,Telephone,Password,TCKN,IsActive,IsAdmin,CreateDateTime,CreateUserID,UpdateDateTime,UpdateUserID")] User user)
        {
            if (ModelState.IsValid)
            {
                user.CreateDateTime = DateTime.Now;
                user.CreateUserID = 0;
                user.IsActive = true;
                db.Users.Add(user);
                db.SaveChanges();
                return Redirect("/Kullanici/KayitBasarili");

            }

            return View(user);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,LastName,Email,ImageUrl,Telephone,Password,TCKN,IsActive,IsAdmin,CreateDateTime,CreateUserID,UpdateDateTime,UpdateUserID")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [Route("Kullanici/KayitBasarili")]
        public ActionResult CreateSuccess()
        {
            return View();
        }
        [Route("Kullanici/Giris")]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [Route("Kullanici/Giris")]
        public ActionResult Login([Bind(Include = "Email,Password")]User user)
        {
            if (new LoginState().IsLoginSucces(user.Email, user.Password, false))
            {
                ViewBag.ResultType = "success";
                ViewBag.ResultMessage = "Giriş İşlemi Başarılı. İyi Alışverişler.";
                return Redirect("/Anasayfa");
            }
            ViewBag.ResultType = "danger";
            ViewBag.ResultMessage = "Giriş Başarısız! Mail Adresi yada Şifre Hatalı.";
            return View();
        }
        [Route("Kullanici/Cikis")]
        public ActionResult LogOut()
        {
            Session["LoginUser"] = null;
            return Redirect("/Anasayfa");
        }

        [Route("Kullanici/Profil/{Page?}")]
        [ControlLogin]
        public ActionResult UserInfo(string Page)
        {
            switch (Page)
            {
                case "Bilgiler":
                    return View("_ProfileInfo");
                    break;
                case "Adresler":
                    return View("_AddressList");
                    break;
                default:
                    break;
            }
            return View();
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

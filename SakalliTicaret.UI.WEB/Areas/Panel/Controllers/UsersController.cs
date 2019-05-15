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
using SakalliTicaret.UI.WEB.App_Class;

namespace SakalliTicaret.UI.WEB.Areas.Panel.Controllers
{
    public class UsersController : AdminControlerBase
    {
        private SakalliTicaretDb db = new SakalliTicaretDb();
        LogClass _logClass = new LogClass();
        // GET: Panel/Users
        public ActionResult Index(int? id)
        {
            List<User> user = new List<User>();

                switch (id)
                {
                    case 1:
                        //Admin Kullanıcıları
                        user = db.Users.Where(x => x.IsAdmin).ToList();
                        break;
                    case 2:
                        //Engellenen Admin Kullanıcıları
                        user = db.Users.Where(x => x.IsAdmin && x.IsActive==false).ToList();
                        break;
                    case 3:
                        //Müşteriler
                        user = db.Users.Where(x => x.IsAdmin==false).ToList();
                        break;
                    case 4:
                        //Engellenen Müşteriler
                        user = db.Users.Where(x => x.IsActive==false && x.IsAdmin==false).ToList();
                        break;
                    case 5:
                        //Aktif Müşteriler
                        user = db.Users.Where(x => x.IsActive && x.IsAdmin == false).ToList();
                        break;
                    default:
                        user = db.Users.ToList();
                        break;
                }
        
            
            return View(user);
        }

        // GET: Panel/Users/Details/5
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

        // GET: Panel/Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Panel/Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,LastName,Email,ImageUrl,Telephone,Password,TCKN,IsActive,IsAdmin,CreateDateTime,CreateUserID,UpdateDateTime,UpdateUserID")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                User sessions = Session["AdminLoginUser"] as User;
                if (sessions != null) _logClass.UserLog(user, "Ekleme", sessions.Id);
                return RedirectToAction("Index");
            }

            return View(user);
        }

        // GET: Panel/Users/Edit/5
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

        // POST: Panel/Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Edit(User user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(user).State = EntityState.Modified;
                    db.SaveChanges();
                    User sessions = Session["AdminLoginUser"] as User;
                    if (sessions != null) _logClass.UserLog(user, "Düzenleme", sessions.Id);
                    return RedirectToAction("Index");
                }
                return View(user);
            }
            catch (Exception e)
            {
                return View();
            }

        }

        // GET: Panel/Users/Delete/5
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

        // POST: Panel/Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            User sessions = Session["AdminLoginUser"] as User;
            if (sessions != null) _logClass.UserLog(user, "Silme", sessions.Id);
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

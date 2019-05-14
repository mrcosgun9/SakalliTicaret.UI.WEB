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
using SakalliTicaret.UI.WEB.Controllers.Base;
using SakalliTicaret.UI.WEB.Models;
using SakalliTicaret.Helper;
using SakalliTicaret.Helper.MailOperations;
using SakalliTicaret.UI.WEB.Areas.Panel.Models;

namespace SakalliTicaret.UI.WEB.Controllers
{
    //todo:Cache aktif
    //[OutputCache(VaryByParam = "none", Duration = 3600)]
    public class UsersController : BaseControler
    {
        private SakalliTicaretDb db = new SakalliTicaretDb();
        LogClass _logClass = new LogClass();
        Functions _functions = new Functions();

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
        [ControlLoginPerformed]
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
        [ControlLoginPerformed]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                User dbuser = db.Users.FirstOrDefault(x => x.Email == user.Email && x.IsAdmin == false);
                if (dbuser == null)
                {
                    EncodeDecode encodeDecode = new EncodeDecode();
                    SendMail sendMail = new SendMail();
                    user.CreateDateTime = DateTime.Now;
                    user.IsActive = true;
                    user.UserKey = encodeDecode.EnCode(user.Email);
                    user.IsMailSuccess = false;
                    string verifyUrl = System.Web.HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) +
                                       "/MailOnayla?Id=" + user.UserKey;
                    sendMail.MailSender("Sakalli Ticaret | Üyelik Aktivasyonu", "Üyelik Aktivasyonu İçin Aşağıdaki Linke Tıklayınız<br><br><br><a href='" + verifyUrl + "'>Aktivasyın Linki</a>", user.Email);
                    db.Users.Add(user);
                    db.SaveChanges();
                    return Redirect("/Kullanici/KayitBasarili");
                }
                else
                {
                    ViewBag.ResultType = "danger";
                    ViewBag.ResultMessage = "Mail Adresi Zaten Sistemde Kayıtlı.";
                }
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
                _logClass.UserLog(user, "Düzenleme", LoginUserId);
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
            _logClass.UserLog(user, "Silme", LoginUserId);
            return RedirectToAction("Index");
        }
        [Route("Kullanici/KayitBasarili")]
        [ControlLoginPerformed]
        public ActionResult CreateSuccess()
        {
            return View();
        }
        [Route("Kullanici/Giris/{ret?}")]
        [ControlLoginPerformed]
        public ActionResult Login(string ret)
        {
            return View();
        }
        [HttpPost]
        [Route("Kullanici/Giris/{ret?}")]
        [ControlLoginPerformed]
        public ActionResult Login([Bind(Include = "Email,Password")]User user, string ret)
        {
            if (new LoginState().IsLoginSucces(user.Email, user.Password, false))
            {
                ViewBag.ResultType = "success";
                ViewBag.ResultMessage = "Giriş İşlemi Başarılı. İyi Alışverişler.";
                _functions.GirisSepetControl();
                if (ret == "Sepet")
                {
                    return Redirect("/Sepet/Tamamla/Adres");
                }
                return Redirect("/Anasayfa");
            }
            ViewBag.ResultType = "danger";
            ViewBag.ResultMessage = "Giriş Başarısız! Mail Adresi yada Şifre Hatalı.";
            return View();
        }
        [Route("Kullanici/Cikis")]
        public ActionResult LogOut()
        {
            Session["LoginUserId"] = null;
            Session["LoginUser"] = null;
            return Redirect("/Anasayfa");
        }

        [Route("Kullanici/Profil/{Page?}")]
        [ControlLogin]
        public ActionResult UserInfo(string Page)
        {
            var loginUser = Session["LoginUser"];
            User user = null;
            if (loginUser != null)
            {
                user = loginUser as User;
            }
            else
            {
                return Redirect("/Anasayfa");
            }
            switch (Page)
            {
                case "Bilgiler":

                    return View("_ProfileInfo", user);
                case "Adresler":
                    return View("_AddressList");
                case "Siparislerim":
                    var basket = db.Baskets.Include(x => x.User).Include(x => x.OrderProducts).Include(x => x.Status).Where(x => x.UserId == user.Id).OrderByDescending(x => x.Id).ToList();
                    return View("_Baskets", basket);
                default:
                    break;
            }
            return View();
        }
        [Route("UyeOlmadanOnayla")]
        public ActionResult NotUserBasketSuccess()
        {
            NotUserBasketModel model = new NotUserBasketModel();
            model.BasketClass = (BasketClass)HttpContext.Session["AktifSepet"];
            if (model.BasketClass.Products.Count == 0)
            {
                return Redirect("/Sepetim");
            }
            return View(model);
        }
        [Route("SiparisDetayi/{id}")]
        public ActionResult BasketDetail(int id)
        {
            BasketDetailModel basketDetailModel = new BasketDetailModel();

            basketDetailModel.OrderProducts = db.OrderProducts.Include(x => x.User).Include(x => x.Product).OrderByDescending(x => x.Id).ToList();

            basketDetailModel.Basket = db.Baskets.FirstOrDefault(x => x.Id == id);
            return View(basketDetailModel);
        }
        [Route("UyeOlmadanOnayla")]
        [HttpPost]
        public ActionResult NotUserBasketSuccess(NotUserBasketModel basketModel)
        {
            basketModel.User.Password = _functions.RandomKey(12);
            basketModel.BasketClass = (BasketClass)HttpContext.Session["AktifSepet"];
            if (ModelState.IsValid)
            {
                User user = basketModel.User;
                user.CreateDateTime=DateTime.Now;
                db.Users.Add(user);
                db.SaveChanges();
                UserAddress userAddress = basketModel.UserAddress;
                userAddress.CreateDateTime=DateTime.Now;
                db.UserAddresses.Add(userAddress);
                db.SaveChanges();
                Basket basket = new Basket();
                basket.BasketKey = basketModel.BasketClass.BasketKey;
                basket.StatusId = 1;
                basket.UserId = user.Id;
                basket.Amount = basketModel.BasketClass.TotalAmount;
                basket.UserAddressId = userAddress.Id;
                basket.CreateDateTime=DateTime.Now;
                db.Baskets.Add(basket);
                db.SaveChanges();
                foreach (var item in basketModel.BasketClass.BasketItems)
                {
                    OrderProduct orderProduct = new OrderProduct();
                    orderProduct.UserId = user.Id;
                    orderProduct.BasketId = basket.Id;
                    orderProduct.InTheBasket = true;
                    orderProduct.ProductId = item.Product.Id;
                    orderProduct.Amount = (double) item.Total;
                    orderProduct.Quantity = item.Quantity;
                    orderProduct.CreateDateTime=DateTime.Now;
                    orderProduct.CreateUserId = user.Id;
                    db.OrderProducts.Add(orderProduct);
                    db.SaveChanges();
                }
                Session["NotUser"] = basketModel;
                try
                {
                    return Redirect("Sepet/Tamamla/Odeme");
                }
                catch (Exception e)
                {
                    return View();
                }
            }
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            basketModel.BasketClass = (BasketClass)HttpContext.Session["AktifSepet"];
            if (basketModel.BasketClass.Products.Count == 0)
            {
                return Redirect("/Sepetim");
            }
            
            return View(basketModel);

        }
        [Route("MailOnayla")]
        public ActionResult MailConfirmation(string Id)
        {
            User user = db.Users.FirstOrDefault(x => x.UserKey == Id);
            if (user != null)
            {
                user.IsMailSuccess = true;
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("MailSuccess");
            }
            else
            {
                return RedirectToAction("MailUnsuccess");
            }
        }
        [Route("MailOnaylaBasarili")]
        public ActionResult MailSuccess()
        {
            return View();
        }
        [Route("MailOnaylaBasarisiz")]
        public ActionResult MailUnsuccess()
        {
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using SakalliTicaret.Core.Model;
using SakalliTicaret.Core.Model.Entity;
using SakalliTicaret.UI.WEB.App_Class;


namespace SakalliTicaret.UI.WEB.Controllers
{
    public class BasketController : Controller
    {
        SakalliTicaretDb db = new SakalliTicaretDb();
        private LoginState _loginState = new LoginState();
        [Route("Sepetim")]
        public ActionResult Index()
        {
            if (_loginState.IsLogin())
            {
                User user = _loginState.IsLoginUser();
                //GirisSepetControl();
                var model = db.UserAddresses.Where(x => x.UserId == user.ID).ToList();
                List<UserAddress> addresses = model;
                ViewBag.AddressList = addresses;
            }
            return View((BasketClass)HttpContext.Session["AktifSepet"]);
        }
        public void Create(int productId)
        {
            try
            {
                BasketClass.BasketItem basketItem = new BasketClass.BasketItem();
                Product product = db.Products.FirstOrDefault(x => x.ID == productId);
                basketItem.Product = product;
                basketItem.Quantity = 1;
                basketItem.Tax = 0;
                BasketClass s = new BasketClass();
                s.SepeteEkle(basketItem);
                if (Session["userId"] != null)
                {
                    int musteriId = (int)HttpContext.Session["userId"];

                }
            }
            catch (Exception e)
            {

            }
        }
        [Route("Sepet/Temizle")]
        public ActionResult SepetTemizle()
        {
            BasketClass s = new BasketClass();
            s.AllClear();
            if (Session["userId"] != null)
            {
                int userId = Convert.ToInt32(Session["LoginUserId"]);
                Basket basket = db.Baskets.Where(x => x.UserId == userId && x.BasketKey == null).FirstOrDefault();
                //List<tblSiparisDetay> detays = db.tblSiparisDetay.Where(x => x.siparis == siparis.satisId).ToList();
                //foreach (var item in detays)
                //{
                //    db.tblSiparisDetay.Remove(item);
                //    db.SaveChanges();
                //}
            }

            return Redirect("/Sepetim");
        }
        [Route("Sepetim/Sil/{id}")]
        public ActionResult SepetItemSil(int id)
        {
            Basket basket = db.Baskets.Find(id);
            if (basket != null)
            {
                db.Baskets.Remove(basket);
                db.SaveChanges();
            }
            BasketClass s = new BasketClass();
            s.BasketItemRemove(id);
            return Redirect("/Sepetim");
        }

        [Route("Sepet/Tamamla")]
        public ActionResult BasketComplete()
        {
            BasketClass s = (BasketClass)Session["AktifSepet"];
            if (s != null)
            {
                if (s.Products.Count != 0)
                {
                    if (_loginState.IsLogin())
                    {
                      
                        return View("Sepet/Tamamla/Adres");

                    }
                    else
                    {
                        return Redirect("/Kullanici/Giris/Sepet");
                    }
                }
                else
                {
                    return RedirectToAction("Sepetim", "Home");
                }
            }
            //if (s.Products.Count != 0)
            //{
            //    if (s.Products.Count != 0)
            //    {
            //        if (Session["userId"] != null)
            //        {
            //            //                int userId = Convert.ToInt32(Session["userId"]);
            //            //                GirisSepetControl();
            //            //                var model = db.tblAdres.Where(x => x.musteriId == userId).ToList();
            //            //                List<tblAdres> adresList;
            //            //                adresList = model;
            //            //                ViewBag.adreslistesi = adresList;
            //            //                ViewBag.adresCount = adresList.Count();
            //            return View();

            //        }
            //        else
            //        {
            //            return RedirectToAction("LoginAndRegistery", "Kullanici", new { ret = "sepet" });
            //        }
            //    }
            //    else
            //    {
            //        return RedirectToAction("Sepetim", "Home");
            //    }
            //}
            //else
            //{
            //    return RedirectToAction("Index", "Home");
            //}
            return View();
        }


        //private void GirisSepetControl()
        //{
        //    int userId = Convert.ToInt32(Session["userId"]);
        //    tblSiparis dbSiparis = db.tblSiparis.Where(x => x.MusteriId == userId && x.SiparisNo == null).FirstOrDefault();
        //    tblSiparis siparisKayit = new tblSiparis();
        //    Sepet s;
        //    s = (Sepet)HttpContext.Session["AktifSepet"];
        //    if (dbSiparis == null)
        //    {

        //        siparisKayit.MusteriId = userId;
        //        siparisKayit.satisTarihi = DateTime.Now;
        //        siparisKayit.sepetteMi = true;
        //        db.tblSiparis.Add(siparisKayit);
        //        db.SaveChanges();
        //        s.sepetId = siparisKayit.satisId;
        //        Session["AktifSepet"] = s;
        //    }
        //    else
        //    {
        //        s.musteriId = userId;
        //        s.sepetId = dbSiparis.satisId;
        //        Session["AktifSepet"] = s;
        //        List<tblSiparisDetay> detays = db.tblSiparisDetay.Where(x => x.siparis == dbSiparis.satisId).ToList();
        //        foreach (var item in detays)
        //        {
        //            db.tblSiparisDetay.Remove(item);
        //            db.SaveChanges();
        //        }
        //        foreach (var item in s.Urunler)
        //        {
        //            tblSiparisDetay siparisDetayKayit = new tblSiparisDetay();
        //            siparisDetayKayit.Urun = item.Urun.urunId;
        //            siparisDetayKayit.Adet = item.Adet;
        //            siparisDetayKayit.siparis = dbSiparis.satisId;
        //            siparisDetayKayit.Tutar = (double?)item.Tutar;
        //            db.tblSiparisDetay.Add(siparisDetayKayit);
        //            db.SaveChanges();
        //        }
        //    }
        //    KargoKontrol();
        //}
        public ActionResult MiniBasketWidget()
        {
            if (HttpContext.Session["AktifSepet"] != null)
            {
                return PartialView((BasketClass)HttpContext.Session["AktifSepet"]);
            }
            return PartialView();
        }
    }
}
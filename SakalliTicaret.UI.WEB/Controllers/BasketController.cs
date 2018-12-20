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
        SakalliTicaretDb db=new SakalliTicaretDb();
        [Route("Sepetim")]
        public ActionResult Index()
        {

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
                basketItem.Tax= 0;
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
        public ActionResult SepetTemizle()
        {
            BasketClass s = new BasketClass();
            s.SepetTemizle();
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
        //public ActionResult Tamamla()
        //{

        //    Sepet s = (Sepet)Session["AktifSepet"];
        //    if (s != null && s.Urunler.Count != 0)
        //    {
        //        if (s.Urunler.Count != 0)
        //        {
        //            if (Session["userId"] != null)
        //            {
        //                int userId = Convert.ToInt32(Session["userId"]);
        //                GirisSepetControl();
        //                var model = db.tblAdres.Where(x => x.musteriId == userId).ToList();
        //                List<tblAdres> adresList;
        //                adresList = model;
        //                ViewBag.adreslistesi = adresList;
        //                ViewBag.adresCount = adresList.Count();
        //                return View();

        //            }
        //            else
        //            {
        //                return RedirectToAction("LoginAndRegistery", "Kullanici", new { ret = "sepet" });
        //            }
        //        }
        //        else
        //        {
        //            return RedirectToAction("Sepetim", "Home");
        //        }
        //    }
        //    else
        //    {
        //        return RedirectToAction("Index", "Home");
        //    }
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
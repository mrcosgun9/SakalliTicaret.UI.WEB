using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using SakalliTicaret.Core.Model;
using SakalliTicaret.Core.Model.Entity;

namespace SakalliTicaret.UI.WEB.App_Class
{
    public class Fonctions
    {
        SakalliTicaretDb db = new SakalliTicaretDb();
        private LoginState _loginState = new LoginState();
        public string CharactertReplace(string character)
        {
            string characterReplace= Regex.Replace(character, "[^0-9a-zA-Z- ]+", "");
            return characterReplace;
        }
        public void GirisSepetControl()
        {
            Basket basket = new Basket();
            BasketClass basketClass = (BasketClass)HttpContext.Current.Session["AktifSepet"];
            if (basketClass != null)
            {
                if (_loginState.IsLogin())
                {
                    basket = db.Baskets.FirstOrDefault(x => x.UserId == _loginState.IsLoginUser().ID && x.StatusId == 5);
                }
                else
                {

                    basket = db.Baskets.FirstOrDefault(x => x.BasketKey == basketClass.BasketKey);
                }
                if (basket == null)
                {

                    Basket newBasket = new Basket();
                    newBasket.StatusId = 5;
                    if (_loginState.IsLogin())
                    {
                        newBasket.UserId = _loginState.IsLoginUser().ID;
                        newBasket.CreateUserID = _loginState.IsLoginUser().ID;
                    }
                    else
                    {
                        newBasket.UserId = -1;
                        newBasket.CreateUserID = -1;
                    }
                    newBasket.CreateDateTime = DateTime.Now;
                    newBasket.Amount = basketClass.TotalAmount;
                    newBasket.BasketKey = basketClass.BasketKey;
                    db.Baskets.Add(newBasket);
                    db.SaveChanges();

                }
                else
                {
                    if (_loginState.IsLogin())
                    {
                        basket.UserId = _loginState.IsLoginUser().ID;
                        basket.CreateUserID = _loginState.IsLoginUser().ID;
                    }
                    basket.Amount = basketClass.TotalAmount;
                    db.Entry(basket).State = EntityState.Modified;
                    db.SaveChanges();


                }
            }

            //    int userId = _loginState.IsLoginUser().ID;
            //    OrderProduct orderProduct = db.OrderProducts.Where(x => x.UserId == userId && x.BasketId == null).FirstOrDefault();
            //    OrderProduct orderProductCreate = new OrderProduct();
            //    BasketClass basketClass= (BasketClass)HttpContext.Session["AktifSepet"];
            //    if (orderProduct == null)
            //    {
            //        orderProductCreate.UserId = userId;
            //        orderProductCreate.CreateDateTime= DateTime.Now;
            //        orderProductCreate.InTheBasket = true;
            //        db.OrderProducts.Add(orderProductCreate);
            //        db.SaveChanges();
            //        basketClass.BasketId = orderProductCreate.BasketId;
            //        Session["AktifSepet"] = basketClass;
            //    }
            //    else
            //    {
            //basketClass.UserId= userId;
            //basketClass.BasketId= orderProduct.BasketId;
            //Session["AktifSepet"] = s;
            //List<tblSiparisDetay> detays = db.tblSiparisDetay.Where(x => x.siparis == dbSiparis.satisId).ToList();
            //foreach (var item in detays)
            //{
            //    db.tblSiparisDetay.Remove(item);
            //    db.SaveChanges();
            //}
            //foreach (var item in s.Urunler)
            //{
            //    tblSiparisDetay siparisDetayKayit = new tblSiparisDetay();
            //    siparisDetayKayit.Urun = item.Urun.urunId;
            //    siparisDetayKayit.Adet = item.Adet;
            //    siparisDetayKayit.siparis = dbSiparis.satisId;
            //    siparisDetayKayit.Tutar = (double?)item.Tutar;
            //    db.tblSiparisDetay.Add(siparisDetayKayit);
            //    db.SaveChanges();
            //}
            //}
            //KargoKontrol();
        }
    }
}
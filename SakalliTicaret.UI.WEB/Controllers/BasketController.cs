using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Newtonsoft.Json.Linq;
using SakalliTicaret.Core.Model;
using SakalliTicaret.Core.Model.Entity;
using SakalliTicaret.UI.WEB.App_Class;
using Newtonsoft.Json.Utilities;

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

                        return Redirect("/Sepet/Tamamla/Adres");

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
        [Route("Sepet/Tamamla/Adres")]
        public ActionResult BasketCompleteAddress()
        {
            User user = _loginState.IsLoginUser();
            BasketClass s = (BasketClass)Session["AktifSepet"];
            //GirisSepetControl();
            if (s == null)
            {
                return Redirect("/Anasayfa");
            }
            if (user == null)
            {
                return Redirect("/Kullanici/Giris");
            }
            var model = db.UserAddresses.Include(u => u.User).Where(x => x.UserId == user.ID).ToList();
            //var model = db.UserAddresses.Include(x => x.Users).Where(x => x.UserId == user.ID).ToList();

            return View(model);
        }
        [Route("Sepet/Tamamla/Adres")]
        [HttpPost]
        public ActionResult BasketCompleteAddress(int ID)
        {
            BasketClass s = (BasketClass)Session["AktifSepet"];
            if (s == null)
            {
                return Redirect("/Anasayfa");
            }
            else
            {
                s.AddressId = ID;
                Session["AktifSepet"] = s;
                return Redirect("/Sepet/Tamamla/Ödeme");
            }
            return View();
        }
        [Route("Sepet/Tamamla/Ödeme")]
        public ActionResult BasketCompletePayment()
        {
            BasketClass s = (BasketClass)Session["AktifSepet"];
            posVoid();
            return View();
        }

        #region Pos İşlemleri
        public string GetIPAddress()
        {
            //var strHostName = "";
            //strHostName = Dns.GetHostName();
            //IPHostEntry ipEntry = Dns.GetHostEntry(strHostName);
            //var addr = ipEntry.AddressList;
            //return addr[1].ToString();
            var ipAddress = string.Empty;
            if (System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
            {
                ipAddress = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
            }
            else if (System.Web.HttpContext.Current.Request.UserHostAddress.Length != 0)
            {
                ipAddress = System.Web.HttpContext.Current.Request.UserHostName;
            }
            try
            {
                if (System.Web.HttpContext.Current.Request.ServerVariables["HTTP_CLIENT_IP"] != null & System.Web.HttpContext.Current.Request.ServerVariables["HTTP_CLIENT_IP"].Length != 0)
                {
                    ipAddress = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_CLIENT_IP"];
                }
            }
            catch (Exception e)
            {

            }


            return ipAddress;
        }
        public static string siparisnumarasiuret(int length)
        {
            string ts = DateTime.Now.ToString("hhmmss");
            string chars = "ST123456789ABCDEFGHJKLMNOPRSTUIVYZWX";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray()) + ts;
        }
        public static string make_user_basket(object[][] user_basket_arr)
        {
            JavaScriptSerializer ser = new JavaScriptSerializer();
            string user_basket_json = ser.Serialize(user_basket_arr);
            string user_basket = Convert.ToBase64String(Encoding.UTF8.GetBytes(user_basket_json)); return user_basket;
        }
        private void posVoid()
        {
            PosEntegration entegration = db.PosEntegrations.Find(1);
            // ####################### DÜZENLEMESİ ZORUNLU ALANLAR #######################
            //
            // API Entegrasyon Bilgileri - Mağaza paneline giriş yaparak BİLGİ sayfasından alabilirsiniz.
            string merchant_id = entegration.StoreCode;
            string merchant_key = entegration.UserName;
            string merchant_salt = entegration.Password;
            //
            // Müşterinizin sitenizde kayıtlı veya form vasıtasıyla aldığınız eposta adresi
            User user = _loginState.IsLoginUser();
            string emailstr = user.Email;
            //
            // Tahsil edilecek tutar. 9.99 için 9.99 * 100 = 999 gönderilmelidir.
            BasketClass s = new BasketClass();
            s = (BasketClass)Session["AktifSepet"];
            int payment_amountstr = (int)s.TotalAmount * 100;
            //
            // Sipariş numarası: Her işlemde benzersiz olmalıdır!! Bu bilgi bildirim sayfanıza yapılacak bildirimde geri gönderilir.
            s.BasketKey = siparisnumarasiuret(8);
            string merchant_oid = s.BasketKey;
            //
            // Müşterinizin sitenizde kayıtlı veya form aracılığıyla aldığınız ad ve soyad bilgisi
            string user_namestr = user.Name + " " + user.LastName;
            //
            // Müşterinizin sitenizde kayıtlı veya form aracılığıyla aldığınız adres bilgisi
            string user_addressstr = db.UserAddresses.Find(s.AddressId).Address;
            //
            // Müşterinizin sitenizde kayıtlı veya form aracılığıyla aldığınız telefon bilgisi
            string user_phonestr = user.Telephone;
            //
            // Başarılı ödeme sonrası müşterinizin yönlendirileceği sayfa
            // !!! Bu sayfa siparişi onaylayacağınız sayfa değildir! Yalnızca müşterinizi bilgilendireceğiniz sayfadır!
            // !!! Siparişi onaylayacağız sayfa "Bildirim URL" sayfasıdır (Bakınız: 2.ADIM Klasörü).
            string merchant_ok_url = "http://www.sakalliticaret.com/Sepetim/OdemeTamamlandi";
            //
            // Ödeme sürecinde beklenmedik bir hata oluşması durumunda müşterinizin yönlendirileceği sayfa
            // !!! Bu sayfa siparişi iptal edeceğiniz sayfa değildir! Yalnızca müşterinizi bilgilendireceğiniz sayfadır!
            // !!! Siparişi iptal edeceğiniz sayfa "Bildirim URL" sayfasıdır (Bakınız: 2.ADIM Klasörü).
            string merchant_fail_url = "http://www.sakalliticaret.com/Sepetim/OdemeHata";
            string user_basketstr = "";
            //        
            // !!! Eğer bu örnek kodu sunucuda değil local makinanızda çalıştırıyorsanız
            // buraya dış ip adresinizi (https://www.whatismyip.com/) yazmalısınız. Aksi halde geçersiz paytr_token hatası alırsınız.
            string user_ip = GetIPAddress();

            //string user_ip = "78.190.112.95";
            if (user_ip == "" || user_ip == null)
            {
                user_ip = Request.ServerVariables["REMOTE_ADDR"];
            }
            //
            // ÖRNEK $user_basket oluşturma - Ürün adedine göre object'leri çoğaltabilirsiniz
            //foreach (var item in s.Urunler)
            //{

            //}
            //object[][] user_basket = {
            //    new object[] {"Örnek ürün 1", "18.00", 1}, // 1. ürün (Ürün Ad - Birim Fiyat - Adet)
            //    new object[] {"Örnek ürün 2", "33.25", 2}, // 2. ürün (Ürün Ad - Birim Fiyat - Adet)
            //    new object[] {"Örnek ürün 3", "45.42", 1}, // 3. ürün (Ürün Ad - Birim Fiyat - Adet)
            //};
            int qArrayCount = s.Products.Count;
            //if (s.KargoBedeli) qArrayCount++;
            object[][] user_basket = new object[qArrayCount][];
            int i = 0;
            foreach (var item in s.Products)
            {
                string qStokKodu = item.Product.ID.ToString();
                decimal qFiyat = Convert.ToDecimal(item.Product.Price);
                int qAdet = Convert.ToInt32(item.Quantity);
                user_basket[i] = new object[] { qStokKodu, qFiyat, qAdet };
                i++;
            }
            //if (s.KargoBedeli)
            //{
            //    user_basket[user_basket.Length - 1] = new object[] { 0, 7, 1 };
            //}
            user_basketstr = make_user_basket(user_basket);
            /* ############################################################################################ */


            // İşlem zaman aşımı süresi - dakika cinsinden
            string timeout_limit = "30";
            //
            // Hata mesajlarının ekrana basılması için entegrasyon ve test sürecinde 1 olarak bırakın. Daha sonra 0 yapabilirsiniz.
            string debug_on = "1";
            //
            // Mağaza canlı modda iken test işlem yapmak için 1 olarak gönderilebilir.
            string test_mode = "1";
            //
            // Taksit yapılmasını istemiyorsanız, sadece tek çekim sunacaksanız 1 yapın
            string no_installment = "1";
            //
            // Sayfada görüntülenecek taksit adedini sınırlamak istiyorsanız uygun şekilde değiştirin.
            // Sıfır (0) gönderilmesi durumunda yürürlükteki en fazla izin verilen taksit geçerli olur.
            string max_installment = "0";
            //
            // Para birimi olarak TL, EUR, USD gönderilebilir. USD ve EUR kullanmak için kurumsal@paytr.com 
            // üzerinden bilgi almanız gerekmektedir. Boş gönderilirse TL geçerli olur.
            string currency = "TL";
            //
            // Türkçe için tr veya İngilizce için en gönderilebilir. Boş gönderilirse tr geçerli olur.
            string lang = "tr";


            // Gönderilecek veriler oluşturuluyor
            NameValueCollection data = new NameValueCollection();
            data["merchant_id"] = merchant_id;
            data["user_ip"] = user_ip;
            data["merchant_oid"] = merchant_oid;
            data["email"] = emailstr;
            data["payment_amount"] = payment_amountstr.ToString();
            //
            // Sepet içerği oluşturma fonksiyonu, değiştirilmeden kullanılabilir.
            JavaScriptSerializer ser = new JavaScriptSerializer();
            string user_basket_json = ser.Serialize(user_basket);
            //string user_basketstr = Convert.ToBase64String(Encoding.UTF8.GetBytes(user_basket_json));
            data["user_basket"] = user_basketstr;
            //
            // Token oluşturma fonksiyonu, değiştirilmeden kullanılmalıdır.
            string Birlestir = string.Concat(merchant_id, user_ip, merchant_oid, emailstr, payment_amountstr.ToString(), user_basketstr, no_installment, max_installment, currency, test_mode, merchant_salt);
            HMACSHA256 hmac = new HMACSHA256(Encoding.UTF8.GetBytes(merchant_key));
            byte[] b = hmac.ComputeHash(Encoding.UTF8.GetBytes(Birlestir));
            data["paytr_token"] = Convert.ToBase64String(b);
            //
            data["debug_on"] = debug_on;
            data["test_mode"] = test_mode;
            data["no_installment"] = no_installment;
            data["max_installment"] = max_installment;
            data["user_name"] = user_namestr;
            data["user_address"] = user_addressstr;
            data["user_phone"] = user_phonestr;
            data["merchant_ok_url"] = merchant_ok_url;
            data["merchant_fail_url"] = merchant_fail_url;
            data["timeout_limit"] = timeout_limit;
            data["currency"] = currency;
            data["lang"] = lang;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            using (WebClient client = new WebClient())
            {
                client.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                byte[] result = client.UploadValues("https://www.paytr.com/odeme/api/get-token", "POST", data);
                string ResultAuthTicket = Encoding.UTF8.GetString(result);
                dynamic json = JValue.Parse(ResultAuthTicket);

                if (json.status == "success")
                {
                    ViewBag.Src = "https://www.paytr.com/odeme/guvenli/" + json.token + "";
                }
                else
                {
                    Response.Write("PAYTR IFRAME failed. reason:" + json.reason + "");
                }
            }

        }
        #endregion

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

        #region Sepet İşlemleri
        public int Create(int productId)
        {
            int basketCount = 0;
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
                s = (BasketClass)Session["AktifSepet"];
                basketCount = s.Products.Count;
            }
            catch (Exception e)
            {

            }
            return basketCount;
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


        #endregion
    }
}
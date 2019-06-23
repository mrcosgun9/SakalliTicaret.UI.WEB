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
using SakalliTicaret.Helper.MailOperations;
using SakalliTicaret.Helper.ProductOperations;
using SakalliTicaret.UI.WEB.Models;

namespace SakalliTicaret.UI.WEB.Controllers
{
    public class BasketController : Controller
    {
        SakalliTicaretDb db = new SakalliTicaretDb();
        private LoginState _loginState = new LoginState();
        private Functions _functions = new Functions();
        [Route("Sepetim")]
        public ActionResult Index()
        {

            if (_loginState.IsLogin())
            {
                User user = _loginState.IsLoginUser();
                //GirisSepetControl();
                var model = db.UserAddresses.Where(x => x.UserId == user.Id).ToList();
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
                    return Redirect("Sepetim");
                }
            }

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
            var model = db.UserAddresses.Include(u => u.User).Where(x => x.UserId == user.Id).ToList();
            //var model = db.UserAddresses.Include(x => x.Users).Where(x => x.UserId == user.Id).ToList();

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

                Basket basket = db.Baskets.FirstOrDefault(x => x.BasketKey == s.BasketKey);
                s.BasketId = basket.Id;
                Session["AktifSepet"] = s;
                if (basket != null)
                {
                    basket.UserAddressId = ID;
                    db.Entry(basket).State = EntityState.Modified;
                    db.SaveChanges();
                }
                return Redirect("/Sepet/Tamamla/Odeme");
            }
            return View();
        }
        [Route("Sepet/Tamamla/Odeme")]
        public ActionResult BasketCompletePayment()
        {
            BasketClass s = (BasketClass)Session["AktifSepet"];
            if (s == null)
            {

            }
            else
            {

            }
            NotUserBasketModel basketModel = Session["NotUserBasketModel"] as NotUserBasketModel;

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

        public static string make_user_basket(object[][] user_basket_arr)
        {
            JavaScriptSerializer ser = new JavaScriptSerializer();
            string user_basket_json = ser.Serialize(user_basket_arr);
            string user_basket = Convert.ToBase64String(Encoding.UTF8.GetBytes(user_basket_json)); return user_basket;
        }
        private void posVoid()
        {
            PosEntegration entegration = db.PosEntegrations.FirstOrDefault();

            string merchant_id = entegration.StoreCode;
            string merchant_key = entegration.UserName;
            string merchant_salt = entegration.Password;
            User user = _loginState.IsLoginUser();
            NotUserBasketModel basketModel = Session["NotUserBasketModel"] as NotUserBasketModel;


            //
            // Tahsil edilecek tutar. 9.99 için 9.99 * 100 = 999 gönderilmelidir.
            Basket basket = new Basket();

            BasketClass s = new BasketClass();
            s = (BasketClass)Session["AktifSepet"];

            int payment_amountstr = (int)s.TotalAmount * 100;
            //
            // Sipariş numarası: Her işlemde benzersiz olmalıdır!! Bu bilgi bildirim sayfanıza yapılacak bildirimde geri gönderilir.

            basket = db.Baskets.FirstOrDefault(x => x.BasketKey == s.BasketKey);
            s.BasketId = basket.Id;
            Session["AktifSepet"] = s;
            s = (BasketClass)Session["AktifSepet"];

            string merchant_oid = s.BasketKey;
            //
            // Müşterinizin sitenizde kayıtlı veya form aracılığıyla aldığınız ad ve soyad bilgisi

            //
            // Müşterinizin sitenizde kayıtlı veya form aracılığıyla aldığınız adres bilgisi
            string user_addressstr = "";
            if (user == null)
            {
                user = basketModel.User;
                user_addressstr = basketModel.UserAddress.Address;
            }
            else
            {
                user_addressstr = db.UserAddresses.Find(s.AddressId).Address;
            }
            string emailstr = user.Email;
            string user_namestr = user.Name + " " + user.LastName;
            //
            // Müşterinizin sitenizde kayıtlı veya form aracılığıyla aldığınız telefon bilgisi
            string user_phonestr = user.Telephone;
            //
            // Başarılı ödeme sonrası müşterinizin yönlendirileceği sayfa
            // !!! Bu sayfa siparişi onaylayacağınız sayfa değildir! Yalnızca müşterinizi bilgilendireceğiniz sayfadır!
            // !!! Siparişi onaylayacağız sayfa "Bildirim URL" sayfasıdır (Bakınız: 2.ADIM Klasörü).
            string merchant_ok_url = "http://sakalliticaret.com/Sepetim/OdemeTamamlandi";
            //
            // Ödeme sürecinde beklenmedik bir hata oluşması durumunda müşterinizin yönlendirileceği sayfa
            // !!! Bu sayfa siparişi iptal edeceğiniz sayfa değildir! Yalnızca müşterinizi bilgilendireceğiniz sayfadır!
            // !!! Siparişi iptal edeceğiniz sayfa "Bildirim URL" sayfasıdır (Bakınız: 2.ADIM Klasörü).
            string merchant_fail_url = "http://sakalliticaret.com/Sepetim/OdemeHata";
            string user_basketstr = "";
            //        
            // !!! Eğer bu örnek kodu sunucuda değil local makinanızda çalıştırıyorsanız
            // buraya dış ip adresinizi (https://www.whatismyip.com/) yazmalısınız. Aksi halde geçersiz paytr_token hatası alırsınız.
            string user_ip = GetIPAddress();
            //string user_ip = "78.186.172.90";
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
                string qStokKodu = item.Product.Id.ToString();
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
            string debug_on = "0";
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
            string qToken = null;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            using (WebClient client = new WebClient())
            {
                client.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                byte[] result = client.UploadValues("https://www.paytr.com/odeme/api/get-token", "POST", data);
                string ResultAuthTicket = Encoding.UTF8.GetString(result);
                dynamic json = JValue.Parse(ResultAuthTicket);
                qToken = json.token;
                string durum = json.status;
                if (json.status == "success")
                {
                    Session["qToken"] = qToken;
                    ViewBag.Src = "https://www.paytr.com/odeme/guvenli/" + json.token + "";
                }
                else
                {
                    Response.Write("PAYTR IFRAME failed. reason:" + json.reason + "");
                }
            }

        }
        #endregion
        public ActionResult MiniBasketWidget()
        {
            if (HttpContext.Session["AktifSepet"] != null)
            {
                return PartialView((BasketClass)HttpContext.Session["AktifSepet"]);
            }
            return PartialView();
        }
        #region Sepet İşlemleri
        public int Create(int productId, List<int> pList, List<int> pValList)
        {
            int basketCount = 0;
            try
            {
                BasketClass.BasketItem basketItem = new BasketClass.BasketItem();
                Product product = db.Products.FirstOrDefault(x => x.Id == productId);
                basketItem.Product = product;
                basketItem.Quantity = 1;
                basketItem.Tax = 0;
                List<PropertyPropertyValues> propertyPropertyValues = new List<PropertyPropertyValues>();
                if (pList != null)
                {
                    for (int i = 0; i < pList.Count; i++)
                    {
                        int pId = pList[i];
                        int pVal = pValList[i];
                        PropertyPropertyValues dbpropertyPropertyValues = db.PropertyPropertyValueses.Include(x => x.CategoryProperty).Include(x => x.CategoryPropertyValue).FirstOrDefault(x => x.CategoryPropertyId == pId && x.CategoryPropertyValueId == pVal);
                        propertyPropertyValues.Add(dbpropertyPropertyValues);
                    }
                }

                basketItem.PropertyPropertyValueses = propertyPropertyValues;
                //basketItem.CategoryPropertyList = pList;
                //basketItem.CategoryPropertyValues = pValList;
                BasketClass s = new BasketClass();
                s.SepeteEkle(basketItem);
                s = (BasketClass)Session["AktifSepet"];
                basketCount = s.Products.Count;
                if (_loginState.IsLogin())
                {
                    int id = _loginState.IsLoginUser().Id;
                    Basket userBasket = db.Baskets.FirstOrDefault(x => x.UserId == id && x.StatusId == 1);
                    OrderProduct orderProduct = db.OrderProducts.FirstOrDefault(x => x.Product.Id == product.Id && x.UserId == id && x.InTheBasket);
                    if (userBasket == null)
                    {
                        //Basket newBasket=new Basket();
                        //newBasket.UserId = id;
                        //newBasket.StatusId = 1;
                        //newBasket.CreateUserId = id;
                        _functions.GirisSepetControl();


                        orderProduct = new OrderProduct();
                        orderProduct.CreateDateTime = DateTime.Now;
                        orderProduct.CreateUserId = _loginState.IsLoginUser().Id;
                        orderProduct.BasketId = s.BasketId;
                        orderProduct.InTheBasket = true;

                        orderProduct.ProductId = product.Id;
                        orderProduct.UserId = _loginState.IsLoginUser().Id;
                        orderProduct.Quantity = s.Products.FirstOrDefault(x => x.Product.Id == productId).Quantity;
                        orderProduct.Amount = (double)s.Products.FirstOrDefault(x => x.Product.Id == product.Id).Total;
                        orderProduct.BasketId = s.BasketId;
                        db.OrderProducts.Add(orderProduct);
                        db.SaveChanges();
                    }
                    else
                    {
                        if (orderProduct != null)
                        {
                            orderProduct.Amount = (double)s.Products.FirstOrDefault(x => x.Product.Id == product.Id).Total;
                            orderProduct.Quantity = orderProduct.Quantity + 1;
                            db.Entry(orderProduct).State = EntityState.Modified;
                        }
                        else
                        {
                            orderProduct = new OrderProduct();
                            orderProduct.CreateDateTime = DateTime.Now;
                            orderProduct.CreateUserId = _loginState.IsLoginUser().Id;
                            orderProduct.BasketId = userBasket.Id;
                            orderProduct.InTheBasket = true;

                            orderProduct.ProductId = product.Id;
                            orderProduct.UserId = _loginState.IsLoginUser().Id;
                            orderProduct.Quantity = s.Products.FirstOrDefault(x => x.Product.Id == productId).Quantity;
                            orderProduct.Amount = (double)s.Products.FirstOrDefault(x => x.Product.Id == product.Id).Total;
                            db.OrderProducts.Add(orderProduct);
                        }
                        db.SaveChanges();
                    }
                    Basket basket = db.Baskets.FirstOrDefault(x => x.BasketKey == s.BasketKey);
                    s = (BasketClass)Session["AktifSepet"];
                    basket.Amount = s.TotalAmount;
                    db.Entry(basket).State = EntityState.Modified;
                    db.SaveChanges();
                }
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
            if (_loginState.IsLogin())
            {
                Basket basket = db.Baskets.FirstOrDefault(x => x.BasketKey == s.BasketKey);
                List<OrderProduct> detays = db.OrderProducts.Where(x => x.BasketId == basket.Id).ToList();
                foreach (var item in detays)
                {
                    db.OrderProducts.Remove(item);
                    db.SaveChanges();
                }

            }
            Session.Remove("AktifSepet");
            return Redirect("/Sepetim");
        }
        [Route("Sepetim/Sil/{id}")]
        public ActionResult SepetItemSil(int id)
        {
            BasketClass s = (BasketClass)Session["AktifSepet"];
            try
            {

                if (_loginState.IsLogin())
                {
                    Basket basket = db.Baskets.FirstOrDefault(x => x.BasketKey == s.BasketKey);
                    OrderProduct detays = db.OrderProducts.FirstOrDefault(x => x.ProductId == id && x.BasketId == s.BasketId && x.InTheBasket);
                    if (detays != null)
                    {
                        db.OrderProducts.Remove(detays);
                        OrderProductProperty productProperty =
                            db.OrderProductProperties.FirstOrDefault(x => x.OrderProductId == detays.Id);
                        if (productProperty != null)
                        {
                            db.OrderProductProperties.Remove(productProperty);

                        }
                    }

                    db.SaveChanges();
                    s = (BasketClass)Session["AktifSepet"];
                    basket.Amount = s.TotalAmount;
                    db.Entry(basket).State = EntityState.Modified;
                    db.SaveChanges();
                }
                s.BasketItemRemove(id);

            }
            catch (Exception e)
            {
                s.AllClear();
            }
            return Redirect("/Sepetim");
        }


        #endregion
        [Route("Sepet/Tamamla/Odeme/Sonuc")]
        public ActionResult BasketPaymentResult()
        {
            PosEntegration entegration = db.PosEntegrations.Find(2);
            BasketTransactions basketTransactions = new BasketTransactions();
            NotUserBasketModel basketModel = Session["NotUserBasketModel"] as NotUserBasketModel;
            Basket basket = new Basket();
            string merchant_id = entegration.StoreCode;
            string merchant_key = entegration.UserName;
            string merchant_salt = entegration.Password;
            string merchant_oid = Request.Form["merchant_oid"];
            string status = Request.Form["status"];
            string total_amount = Request.Form["total_amount"];
            string hash = Request.Form["hash"];
            string installment_count = Request.Form["installment_count"];
            string Birlestir = string.Concat(merchant_oid, merchant_salt, status, total_amount);
            HMACSHA256 hmac = new HMACSHA256(Encoding.UTF8.GetBytes(merchant_key));
            byte[] b = hmac.ComputeHash(Encoding.UTF8.GetBytes(Birlestir));
            string token = null;
            token = Convert.ToBase64String(b);
            //if (hash != token)
            //{
            //    Response.Write("PAYTR notification failed: bad hash");
            //    return;
            //}

            if (status == "success")
            {
                try
                {
                    basketTransactions.Status = status;
                    basketTransactions.CreateDateTime = DateTime.Now;
                    if (basketModel != null|| basketModel.User!=null)
                    {
                        if (basketModel.User != null || basketModel.BasketClass != null || basketModel.UserAddress != null)
                        {
                            basketTransactions.UserId = basketModel.User.Id;
                            basketTransactions.BasketKey = basketModel.BasketClass.BasketKey;
                            basketTransactions.BasketId = basketModel.BasketClass.BasketId;
                            basketTransactions.Description = "Ödeme İşlemi Başarıyla Gerçekleştirildi.";
                            basketTransactions.CreateUserId = basketModel.User.Id;
                            basket = db.Baskets.FirstOrDefault(x => x.Id == basketModel.BasketClass.BasketId);
                            basket.StatusId = 2;
                            db.Entry(basket).State = EntityState.Modified;
                            List<OrderProduct> orderProducts =
                                db.OrderProducts.Where(x => x.BasketId == basket.Id).ToList();
                            foreach (var item in orderProducts)
                            {
                                ProductOperations productOperations = new ProductOperations();
                                productOperations.ProductOperationsCreate(item.ProductId, 3, "Satış No:" + item.Basket.BasketKey + " - Adet:" + item.Quantity, basket.UserId);
                                item.InTheBasket = false;
                                db.Entry(item).State = EntityState.Modified;
                            }
                            db.SaveChanges();
                            Session.Remove("NotUserBasketModel");
                        }
                    }
                    else
                    {
                        BasketClass s = new BasketClass();
                        s = (BasketClass)Session["AktifSepet"];
                        basketTransactions.UserId = s.UserId;
                        basketTransactions.BasketKey = s.BasketKey;
                        basketTransactions.BasketId = s.BasketId;
                        basketTransactions.Description = "Ödeme İşlemi Başarıyla Gerçekleştirildi.";
                        basketTransactions.CreateUserId = _loginState.IsLoginUser().Id;
                        basket = db.Baskets.FirstOrDefault(x => x.Id == s.BasketId);
                        basket.StatusId = 2;
                        db.Entry(basket).State = EntityState.Modified;
                        List<OrderProduct> orderProducts =
                            db.OrderProducts.Where(x => x.BasketId == basket.Id).ToList();
                        foreach (var item in orderProducts)
                        {
                            ProductOperations productOperations = new ProductOperations();
                            productOperations.ProductOperationsCreate(item.ProductId, 3, "Satış No:" + item.Basket.BasketKey + " - Adet:" + item.Quantity, basket.UserId);
                            item.InTheBasket = false;
                            db.Entry(item).State = EntityState.Modified;
                        }
                        db.SaveChanges();
                    }

                    Session.Remove("AktifSepet");
                }
                catch (Exception e)
                {
                    SendMail sendMail = new SendMail();
                    sendMail.MailSender("Ödeme Yapıldı Sepet Kapatılamadı", "Sayfadan gelen hata:<br><br>"+e.InnerException+"<br><br>"+e.Source+"<br><br>"+e.Message, "mrcosgun9@gmail.com");

                }


                Response.Write("OK");
            }
      
            return View();
        }
        [Route("Sepetim/OdemeTamamlandi")]
        public ActionResult PaymentSuccessful()
        {
            return View();
        }
        [Route("Sepetim/OdemeHata")]
        public ActionResult PaymentError()
        {
            return View();
        }
    }
}
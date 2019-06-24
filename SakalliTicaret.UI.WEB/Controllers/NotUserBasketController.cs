using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SakalliTicaret.Core.Model;
using SakalliTicaret.Core.Model.Entity;
using SakalliTicaret.UI.WEB.App_Class;
using SakalliTicaret.UI.WEB.Controllers.Base;
using SakalliTicaret.UI.WEB.Models;

namespace SakalliTicaret.UI.WEB.Controllers
{
    public class NotUserBasketController : BaseControler
    {
        private SakalliTicaretDb db = new SakalliTicaretDb();
        Functions _functions = new Functions();
        // GET: NotUserBasket
        [Route("UyeOlmadanTamamla")]
        public ActionResult Index()
        {
            NotUserBasketModel notUserBasketModel = new NotUserBasketModel();
            notUserBasketModel = GetUser();
            BasketClass s = new BasketClass();
            s = (BasketClass)Session["AktifSepet"];
            notUserBasketModel.BasketClass = s;

            if (notUserBasketModel.BasketClass == null)
            {
                return Redirect("/Anasayfa");
            }
            return View();
        }
        [HttpPost]
        [Route("UyeOlmadanTamamla")]
        public ActionResult Index(User user, string BtnPrevious, string BtnNext)
        {
            if (BtnNext != null)
            {
                User userControl = db.Users.FirstOrDefault(x => x.Email == user.Email && x.CreateMethod != UserCreateMethod.NotUserCreate);
                if (userControl == null)
                {
                    if (ModelState.IsValid)
                    {
                        NotUserBasketModel getUser = GetUser();
                        userControl = db.Users.FirstOrDefault(x => x.Email == user.Email && x.Telephone==x.Telephone);
                        if (userControl != null)
                        {
                            user.Id = userControl.Id;
                        }
                        
                        user.CreateMethod = UserCreateMethod.NotUserCreate;
                        user.CreateDateTime = DateTime.Now;
                        getUser.User = user;
                        return View("NotUserBasketAddress");
                        //return View("CricketerStatistics");
                    }
                }
                else
                {
                    ViewBag.ResultType = "danger";
                    ViewBag.ResultMessage = "Mail Adresiyle Üyelik İşlemi Gerçekleştirilmiş.Giriş Sayfasına Yönelendiriliyorsunuz.";
                    Response.AppendHeader("Refresh", "5;url=http://sakalliticaret.com/Kullanici/Giris");
                }
            }
            return View();
        }

        public ActionResult NotUserBasketAddress()
        {
            NotUserBasketModel notUserBasketModel = GetUser();
            if (notUserBasketModel.User == null)
            {
                return Redirect("/Anasayfa");
            }
            return View();
        }
        [HttpPost]
        public ActionResult NotUserBasketAddress(UserAddress userAddress, string BtnPrevious, string BtnNext)
        {
            NotUserBasketModel notUserBasketModel = GetUser();
            if (BtnPrevious != null)
            {

                return View("Index", notUserBasketModel);
            }
            if (BtnNext != null)
            {
                BasketClass s = new BasketClass();
                s = (BasketClass)Session["AktifSepet"];
                userAddress.CreateDateTime = DateTime.Now;
                notUserBasketModel.UserAddress = userAddress;
                notUserBasketModel.BasketClass = s;
                return View("NotUserBasketDetail", notUserBasketModel);
            }
            return View();
        }

        public ActionResult NotUserBasketDetail()
        {
            return View();
        }
        [HttpPost]
        public ActionResult NotUserBasketDetail(string BtnPrevious, string BtnNext)
        {
            NotUserBasketModel notUserBasketModel = GetUser();
            if (BtnPrevious != null)
            {
                return View("NotUserBasketAddress", notUserBasketModel);
            }
            if (BtnNext != null)
            {
                User user = notUserBasketModel.User;
                user.CreateMethod = UserCreateMethod.NotUserCreate;
                if (user.Id==0)
                {
                    db.Users.Add(user);
                }
                else
                {
                    db.Entry(user).State = EntityState.Modified;
                }
                UserAddress address = notUserBasketModel.UserAddress;
                address.UserId = user.Id;
                db.UserAddresses.Add(address);
                Basket basket = new Basket();
                basket.CreateDateTime = DateTime.Now;
                basket.CreateUserId = user.Id;
                basket.BasketKey = notUserBasketModel.BasketClass.BasketKey;
                basket.StatusId = 1;
                basket.UserAddressId = address.Id;
                basket.UserId = user.Id;
                basket.Quantity = notUserBasketModel.BasketClass.BasketItems.Count;
                basket.Amount = notUserBasketModel.BasketClass.TotalAmount;
                db.Baskets.Add(basket);

                foreach (var item in notUserBasketModel.BasketClass.BasketItems)
                {
                    OrderProduct orderProduct = new OrderProduct();
                    orderProduct.UserId = user.Id;
                    orderProduct.BasketId = basket.Id;
                    orderProduct.InTheBasket = true;
                    orderProduct.ProductId = item.Product.Id;
                    orderProduct.Amount = (double)item.Total;
                    orderProduct.Quantity = item.Quantity;
                    orderProduct.CreateDateTime = DateTime.Now;
                    orderProduct.CreateUserId = user.Id;
                    db.OrderProducts.Add(orderProduct);
                }

                db.SaveChanges();

                return Redirect("/Sepet/Tamamla/Odeme");
            }
            return View();
        }
        private NotUserBasketModel GetUser()
        {
            if (Session["NotUserBasketModel"] == null)
            {
                Session["NotUserBasketModel"] = new NotUserBasketModel();
            }
            return (NotUserBasketModel)Session["NotUserBasketModel"];
        }
    }
}
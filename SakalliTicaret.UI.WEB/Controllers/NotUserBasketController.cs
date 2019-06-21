using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SakalliTicaret.Core.Model;
using SakalliTicaret.Core.Model.Entity;
using SakalliTicaret.UI.WEB.App_Class;
using SakalliTicaret.UI.WEB.Models;

namespace SakalliTicaret.UI.WEB.Controllers
{
    public class NotUserBasketController : Controller
    {
        private SakalliTicaretDb db = new SakalliTicaretDb();
        Functions _functions = new Functions();
        // GET: NotUserBasket
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]

        public ActionResult Index(User user, string BtnPrevious, string BtnNext)
        {
            if (BtnNext != null)
            {
                User userControl = db.Users.FirstOrDefault(x => x.Email == user.Email);
                if (userControl == null)
                {
                    if (ModelState.IsValid)
                    {
                        NotUserBasketModel getUser = GetUser();
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
            if (notUserBasketModel.User==null)
            {
                return View("Index");
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
                userAddress.CreateDateTime=DateTime.Now;
                notUserBasketModel.UserAddress = userAddress;
                return View("NotUserBasketDetail", notUserBasketModel);
            }
            return View();
        }

        public ActionResult NotUserBasketDetail()
        {
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SakalliTicaret.UI.WEB.Areas.Panel.Controllers
{
    public class LoginController : Controller
    {
        // GET: Panel/Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(string email,string password)
        {
            if (new LoginState().IsLoginSucces(email, password, true))
            {
                return Redirect("/Panel/Default/");
            }
          
            return View();
        }

        public ActionResult LogOut()
        {
            Session["AdminLoginUser"] = null;
            return Redirect("/Panel/Default/");
        }
    }
}
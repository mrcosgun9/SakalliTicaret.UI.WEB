using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SakalliTicaret.UI.WEB.Controllers
{
    public class ErrorPageController : Controller
    {
        public ActionResult PageError()
        {
            Response.TrySkipIisCustomErrors = true;
            return View();
        }
        [Route("SayfaBulunamadi")]
        public ActionResult Page404(string aspxerrorpath)
        {
            if (!string.IsNullOrEmpty(aspxerrorpath))
                ViewBag.Kaynak = aspxerrorpath;
            Response.StatusCode = 404;
            Response.StatusDescription = "Üzgünüz Aradığınız Sayfa Bulunamadı.<br />Anasayfaya Dönebilir ya da Arama Bölümünden Ürün Arayabilirsiniz.";
            Response.TrySkipIisCustomErrors = true;
            return View("PageError");
        }
        [Route("Hata403")]
        public ActionResult Page403(string aspxerrorpath)
        {
            //Response.StatusCode = 403;
            //Response.TrySkipIisCustomErrors = true;
            //return View("PageError");
            if (!string.IsNullOrEmpty(aspxerrorpath))
                ViewBag.Kaynak = aspxerrorpath;
            Response.StatusCode = 403;
            Response.StatusDescription = "...";
            Response.TrySkipIisCustomErrors = true;
            return View("PageError");
        }
        [Route("Hata500")]
        public ActionResult Page500(string aspxerrorpath)
        {
            if (!string.IsNullOrEmpty(aspxerrorpath))
                ViewBag.Kaynak = aspxerrorpath;
            Response.StatusCode = 500;
            Response.StatusDescription = "Üzgünüz Bir Hata Oluştu.<br />Anasayfaya Dönebilirsiniz.";
            Response.TrySkipIisCustomErrors = true;
            return View("PageError");
        }
    }
}
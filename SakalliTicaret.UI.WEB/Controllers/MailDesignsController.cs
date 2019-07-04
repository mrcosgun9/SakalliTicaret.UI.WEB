using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SakalliTicaret.Core.Model;
using SakalliTicaret.Core.Model.Entity;
using SakalliTicaret.UI.WEB.App_Class;

namespace SakalliTicaret.UI.WEB.Controllers
{
    public class MailDesignsController : Controller
    {
        SakalliTicaretDb db = new SakalliTicaretDb();
        // GET: MailDesigns
        [Route("OdemeOnay/{basketKey}")]
        public ActionResult PaymentSuccessMail(string basketKey)
        {
            BasketClass basketClass = (BasketClass) HttpContext.Session["AktifSepet"];
            User user = db.Users.FirstOrDefault(x => x.Id == basketClass.UserId);
            UserAddress userAddress = db.UserAddresses.FirstOrDefault(x => x.Id == basketClass.AddressId);
            basketClass.User = user;
            basketClass.UserAddress=userAddress;
            return View(basketClass);
        }
    }
}
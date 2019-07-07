using System;
using System.Collections.Generic;
using System.Data.Entity;
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
            //Basket basket = db.Baskets.FirstOrDefault(x => x.BasketKey == basketKey);
            var basket = db.Baskets.Include(x => x.User).Include(x => x.UserAddress).FirstOrDefault(x => x.BasketKey == basketKey);

            //BasketClass basketClass = (BasketClass)Session["AktifSepet"];
            //basket. = db.Users.FirstOrDefault(x => x.Id == basket.UserId);
            //basket.UserAddress = db.UserAddresses.FirstOrDefault(x => x.Id == basket.UserAddressId);
            basket.OrderProducts = db.OrderProducts.Include(x=>x.Product).Where(x => x.BasketId == basket.Id).ToList();

            return View(basket);
        }


    }
}
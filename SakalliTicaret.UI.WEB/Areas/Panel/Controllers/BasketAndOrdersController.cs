using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SakalliTicaret.Core.Model;
using SakalliTicaret.Core.Model.Entity;

namespace SakalliTicaret.UI.WEB.Areas.Panel.Controllers
{
    public class BasketAndOrdersController : AdminControlerBase
    {

        // GET: Panel/BasketAndOrders
        public ActionResult Index()
        {
            var basket = db.Baskets.Include(x => x.User).Include(x => x.Status).OrderByDescending(x => x.Id).ToList();
            return View(basket);
        }
        public ActionResult Detail(int id)
        {
            var basket = db.OrderProducts.Include(x => x.User).Include(x=>x.Product).OrderByDescending(x => x.Id).ToList();
            return View(basket);
        }
    }
}
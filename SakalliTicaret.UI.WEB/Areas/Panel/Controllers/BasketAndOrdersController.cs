using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SakalliTicaret.Core.Model;
using SakalliTicaret.Core.Model.Entity;
using SakalliTicaret.UI.WEB.Areas.Panel.Models;

namespace SakalliTicaret.UI.WEB.Areas.Panel.Controllers
{
    public class BasketAndOrdersController : AdminControlerBase
    {

        // GET: Panel/BasketAndOrders
        public ActionResult Index()
        {
            ViewBag.StatusList = db.Statuses.ToList();
            var basket = db.Baskets.Include(x => x.User).Include(x => x.Status).OrderByDescending(x => x.Id).ToList();
            return View(basket);
        }
        public ActionResult Detail(int id)
        {
            BasketDetailModel basketDetailModel = new BasketDetailModel();

            basketDetailModel.OrderProducts = db.OrderProducts.Include(x => x.User).Include(x => x.Product).OrderByDescending(x => x.Id).ToList();

            basketDetailModel.Basket = db.Baskets.Include(x => x.UserAddress).FirstOrDefault(x => x.Id == id);

            return View(basketDetailModel);
        }

        public ActionResult StatusEdit(int Id)
        {
            Basket basket = db.Baskets.Include(x => x.Status).FirstOrDefault(x => x.Id == Id);
            if (basket.StatusId == 3)
            {

            }
            else if (basket.StatusId == 4)
            {

            }
            basket.StatusId = basket.Status.NextStatus;
            db.Entry(basket).State = EntityState.Modified;
            db.SaveChanges();
            return Redirect("/Panel/BasketAndOrders/Index");
        }
    }
}
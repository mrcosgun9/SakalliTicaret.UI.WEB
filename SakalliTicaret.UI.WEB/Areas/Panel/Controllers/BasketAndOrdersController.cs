using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SakalliTicaret.Core.Model;
using SakalliTicaret.Core.Model.Entity;
using SakalliTicaret.Helper.MailOperations;
using SakalliTicaret.UI.WEB.Areas.Panel.Models;

namespace SakalliTicaret.UI.WEB.Areas.Panel.Controllers
{
    public class BasketAndOrdersController : AdminControlerBase
    {
        SendMail sendMail = new SendMail();
        // GET: Panel/BasketAndOrders
        public ActionResult Index(int? id)
        {
            if (id==null)
            {
                id = 2;
            }
            var basket = db.Baskets.Include(x => x.User).Include(x => x.Status).OrderByDescending(x => x.Id).Where(x=>x.StatusId==id).ToList();
            return View(basket);
        }
        public ActionResult Detail(int id)
        {
            BasketDetailModel basketDetailModel = new BasketDetailModel();

            basketDetailModel.OrderProducts = db.OrderProducts.Include(x => x.User).Include(x => x.Product).OrderByDescending(x => x.Id).Where(x => x.BasketId == id).ToList();

            basketDetailModel.Basket = db.Baskets.Include(x => x.UserAddress).FirstOrDefault(x => x.Id == id);

            return View(basketDetailModel);
        }

        public ActionResult StatusEdit(int Id)
        {
            Basket basket = db.Baskets.Include(x=>x.User).Include(x => x.Status).FirstOrDefault(x => x.Id == Id);
            string MailSubject = "";
           
            if (basket.StatusId == 2)
            {
               Product product=new Product();
                foreach (var item in basket.OrderProducts)
                {
                    product = db.Products.FirstOrDefault(x => x.Id == item.ProductId);
                    product.Stock = product.Stock - item.Quantity;
                    db.Entry(product).State = EntityState.Modified;
                }
                
            }
            if (basket.StatusId == 3)
            {
                
               
                return Redirect("/Panel/BasketAndOrders/BaketCargoEdit/" + basket.Id);
            }
            basket.StatusId = basket.Status.NextStatus;
            db.Entry(basket).State = EntityState.Modified;
            db.SaveChanges();
           
            //sendMail.MailSender(MailSubject, MailSayfasiGet(basket.BasketKey),basket.User.Email);
            
            return Redirect("/Panel/BasketAndOrders/Index");
        }

        public string MailSayfasiGet(string basketKey)
        {
            string adres = Request.Url.Authority + "/OdemeOnay/" + basketKey;
            if (adres.StartsWith("localhost"))
                adres = "Http://" + adres;
            WebRequest req = WebRequest.Create(adres);
            WebResponse res = req.GetResponse();
            System.IO.StreamReader data = new System.IO.StreamReader(res.GetResponseStream(), System.Text.Encoding.UTF8);
            string mail_body = data.ReadToEnd();
            return mail_body;
        }
        public ActionResult BaketCargoEdit(int id)
        {
            ViewBag.CargoCompany = db.CargoCompanies.ToList();
            ViewBag.BasketId = id;
            BasketCargo cargoCompany = db.BasketCargoes.FirstOrDefault(x => x.BasketId == id);
            return View(cargoCompany);
        }
        [HttpPost]
        public ActionResult BaketCargoEdit(BasketCargo basketCargo)
        {
            Basket basket = db.Baskets.Include(x=>x.User).Include(x => x.Status).FirstOrDefault(x => x.Id == basketCargo.BasketId);
            basketCargo.CreateUserId = AdminLoginUserId;
            basketCargo.CreateDateTime = DateTime.Now;

            db.BasketCargoes.Add(basketCargo);

            basket.StatusId = basket.Status.NextStatus;
            db.Entry(basket).State = EntityState.Modified;
            db.SaveChanges();
         
            return Redirect("/Panel/BasketAndOrders/Index");
        }
    }
}
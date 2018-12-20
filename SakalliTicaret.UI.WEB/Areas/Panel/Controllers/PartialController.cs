using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SakalliTicaret.Core.Model;

namespace SakalliTicaret.UI.WEB.Areas.Panel.Controllers
{
    public class PartialController : AdminControlerBase
    {
        SakalliTicaretDb db=new SakalliTicaretDb();
        // GET: Panel/Partial
        public ActionResult Menu()
        {
            return View();
        }

        public ActionResult UserMenu()
        {
            return View();
        }

        public ActionResult ContactEdit()
        {
            return View();
        }
        public ActionResult IndexInfo()
        {
            IndexInfoValues infoValues=new IndexInfoValues();
            infoValues.ProductCount = db.Products.ToList().Count;
            infoValues.SaleCount = db.Baskets.ToList().Count;
            infoValues.UserCount = db.Users.Where(x => x.IsAdmin == false).ToList().Count;
            //infoValues.SalePriceCount=db.Baskets.Where()
            ViewBag.infoValue = infoValues;
            return View();
        }
       
    }
    public class IndexInfoValues{
        public int ProductCount { get; set; }
        public int SaleCount { get; set; }
        public int UserCount { get; set; }
        public int SalePriceCount { get; set; }

    }
}
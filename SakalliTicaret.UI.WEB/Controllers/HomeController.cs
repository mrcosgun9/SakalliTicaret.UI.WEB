using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using PagedList;
using SakalliTicaret.Core.Model;
using SakalliTicaret.UI.WEB.Controllers.Base;
using SakalliTicaret.UI.WEB.Models;

namespace SakalliTicaret.UI.WEB.Controllers
{
    public class HomeController : BaseControler
    {
        private SakalliTicaretDb db = new SakalliTicaretDb();
        // GET: Home
        [Route("Anasayfa")]
        [Route]
        public ActionResult Index(FilterModel filterModel)
        {
            List<PageRanking> pageRankings = new List<PageRanking>
            {
                new PageRanking {Id = 1, Adi = "Alfabetik (A-Z)"},
                new PageRanking {Id = 2, Adi = "Alfabetik (Z-A)"},
                new PageRanking {Id = 3, Adi = "Azalan Fiyat"},
                new PageRanking {Id = 4, Adi = "Artan Fiyat"},
                new PageRanking {Id = 5, Adi = "Yayın Tarihi"}

            };
            ViewBag.PageRanking = new SelectList(pageRankings, "Id", "Adi");
            int page = filterModel.Page ?? 1;
            int pageCount = filterModel.PageCount ?? 30;
            filterModel.Products = db.Products.OrderBy(x => x.Name).ToPagedList(page, pageCount);
            if (Request.IsAjaxRequest())
            {
                return PartialView("_Products", filterModel);
            }
            else
            {
                return View(filterModel);
            }

            return View();
        }
        [Route("Sayfa/{id}/{title}")]
        public ActionResult Page(int id)
        {
            var model = db.Pages.Find(id);
            return View(model);
        }
        public class PageRanking
        {
            public int Id { get; set; }

            public string Adi { get; set; }
        }
    }
}
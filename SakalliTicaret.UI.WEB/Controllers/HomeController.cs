using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using PagedList;
using SakalliTicaret.Core.Model;
using SakalliTicaret.Core.Model.Entity;
using SakalliTicaret.UI.WEB.Controllers.Base;
using SakalliTicaret.UI.WEB.Models;

namespace SakalliTicaret.UI.WEB.Controllers
{
    public class HomeController : BaseControler
    {
        private SakalliTicaretDb db = new SakalliTicaretDb();
        // GET: Home
        [Route("Anasayfa/{Page?}")]
        [Route]
        public ActionResult Index(FilterModel filterModel)
        {
        
            List<PageRanking> pageRankings = new List<PageRanking>
            {
                new PageRanking {Id = 1, Name = "Alfabetik (A-Z)"},
                new PageRanking {Id = 2, Name = "Alfabetik (Z-A)"},
                new PageRanking {Id = 3, Name = "Azalan Fiyat"},
                new PageRanking {Id = 4, Name = "Artan Fiyat"},

            };
            ViewBag.PageRanking = new SelectList(pageRankings, "Id", "Name");
            int page = filterModel.Page ?? 1;
            int pageCount = filterModel.PageCount ?? 30;
            switch (filterModel.PageRanking)
            {
                case 1:
                    filterModel.Products = db.Products.OrderBy(x => x.Name).ToPagedList(page, pageCount);
                    break;
                case 2:
                    filterModel.Products = db.Products.OrderByDescending(x => x.Name).ToPagedList(page, pageCount);
                    break;
                case 3:
                    filterModel.Products = db.Products.OrderBy(x => x.Price).ToPagedList(page, pageCount);
                    break;
                case 4:
                    filterModel.Products = db.Products.OrderByDescending(x => x.Price).ToPagedList(page, pageCount);
                    break;

                default:
                    filterModel.Products = db.Products.OrderByDescending(x=>x.Name).ToPagedList(page, pageCount);
                    break;
            }
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

            public string Name { get; set; }
        }
    }
}
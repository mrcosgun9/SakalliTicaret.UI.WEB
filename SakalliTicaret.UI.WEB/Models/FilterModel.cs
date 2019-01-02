using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;
using SakalliTicaret.Core.Model.Entity;

namespace SakalliTicaret.UI.WEB.Models
{
    public class FilterModel
    {
        public int? Category { get; set; }
        public int? PageRanking { get; set; }
        public int? Page { get; set; }
        public int? PageCount { get; set; }
        public string Search { get; set; }
        public int searchCategory { get; set; }
        public IPagedList<Product> Products { get; set; }
    }
}
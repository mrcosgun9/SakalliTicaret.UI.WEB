using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SakalliTicaret.Core.Model.Entity;

namespace SakalliTicaret.UI.WEB.Areas.Panel.Models
{
    public class BasketDetailModel
    {
        public Basket Basket { get; set; }
        public IEnumerable<OrderProduct> OrderProducts { get; set; }
    }
}
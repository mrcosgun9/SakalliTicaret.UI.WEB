using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SakalliTicaret.Core.Model.Entity;

namespace SakalliTicaret.UI.WEB.Models
{
    public class ProductDetailModel
    {
        public Product Product { get; set; }
        public List<CategoryProperty> CategoryProperties { get; set; }
        public List<CategoryPropertyValue> CategoryPropertyValues { get; set; }

    }
}
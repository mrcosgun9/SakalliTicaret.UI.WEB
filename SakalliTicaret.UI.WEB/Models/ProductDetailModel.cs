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
        public List<PropertyPropertyValues> PropertyPropertyValueses { get; set; }
        public List<ProductProperty> ProductProperties { get; set; }
        public List<Product> FeaturedProduct { get; set; }
        public List<ProductImages> ProductImageses { get; set; }
    }
}
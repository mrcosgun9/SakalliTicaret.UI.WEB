using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SakalliTicaret.Core.Model.Entity;

namespace SakalliTicaret.UI.WEB.Areas.Panel.Models
{
    public class ProductPropertiesModel
    {
        public List<PropertyPropertyValues> PropertyPropertyValueses { get; set; }
        public List<ProductProperty> ProductProperties  { get; set; }
    }
}
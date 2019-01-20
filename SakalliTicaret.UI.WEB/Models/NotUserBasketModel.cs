using SakalliTicaret.Core.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SakalliTicaret.UI.WEB.App_Class;

namespace SakalliTicaret.UI.WEB.Models
{
    public class NotUserBasketModel
    {
        public User User { get; set; }
        public UserAddress UserAddress { get; set; }
        public BasketClass BasketClass { get; set; }
    }
}